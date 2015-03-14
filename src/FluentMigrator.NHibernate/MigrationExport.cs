using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using FluentMigrator.Expressions;
using FluentMigrator.Model;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Engine;
using NHibernate.Id;
using NHibernate.Mapping;
using NHibernate.SqlTypes;
using NHibernate.Util;
using Environment = NHibernate.Cfg.Environment;

namespace FluentMigrator.NHibernate
{

    public class MigrationExport : IDefinitionsBuilder
    {
        private readonly Configuration _cfg;
        private readonly Dialect _dialect;

        public MigrationExport(Configuration cfg, Dialect dialect)
        {
            _cfg = cfg;
            _dialect = dialect;
        }

        public IEnumerator<MigrationExpressionBase> GetEnumerator()
        {
            var mapping = _cfg.BuildMapping();
            var tables = _cfg.ClassMappings.SelectMany(m => m.TableClosureIterator)
                .GroupBy(t => new {t.Schema, t.Name})
                .Select(g => g.First())
                .Where(t => t.IsPhysicalTable)
                .ToList();
            var schemas = tables.Select(x => x.Schema).Where(s => !String.IsNullOrEmpty(s)).Distinct().ToList();
            foreach (var schema in schemas)
            {
                yield return new CreateSchemaExpression
                {
                    SchemaName = schema
                };
            }
            foreach (var table in tables)
            {
                yield return new CreateTableExpression
                {
                    SchemaName = table.Schema,
                    TableName = table.Name,
                    Columns = GetTableColumns(table, mapping),
                    TableDescription = table.Comment
                };
                foreach (var p in GetUniqueKeys(table)) yield return p;
                foreach (var p in GetIndexes(table)) yield return p;
                foreach (var p in GetFKs(table)) yield return p;
            }
            foreach (var g in GetIdGenerators()) yield return g;
        }

        private IEnumerable<MigrationExpressionBase> GetIdGenerators()
        {
            string defaultCatalog = PropertiesHelper.GetString(Environment.DefaultCatalog, _cfg.Properties, null);
            string defaultSchema = PropertiesHelper.GetString(Environment.DefaultSchema, _cfg.Properties, null);

            var generators = GetPersistentIdentifierGenerators(defaultCatalog, defaultSchema);

            IEnumerable<MigrationExpressionBase> idGenerators = GetExpressionsFor(generators);
            return idGenerators;
        }

        private IEnumerable<MigrationExpressionBase> GetExpressionsFor(List<IPersistentIdentifierGenerator> generators)
        {
            foreach (var g in generators)
            {
                var table = g as TableGenerator;
                var sequence = g as SequenceGenerator;
                if (table != null)
                {
                    var tableName = GetPrivateField<string>(table, "tableName");
                    var columnName = GetPrivateField<string>(table, "columnName");
                    var sqlType = GetPrivateField<SqlType>(table, "columnSqlType");
                    yield return new CreateTableExpression
                    {
                        TableName = tableName,
                        Columns = new List<ColumnDefinition>
                        {
                            new ColumnDefinition {Name = columnName, Type = sqlType.DbType}
                        }
                    };
                }
                else if (sequence != null)
                {
                    yield return new CreateSequenceExpression
                    {
                        Sequence = new SequenceDefinition
                        {
                            Name = sequence.SequenceName
                        }
                    };
                }
                else
                {
                    throw new NotImplementedException(String.Format("Havent implemented deconstruction for {0}", g.GetType().FullName));
                }
                
            }
        }

        private T GetPrivateField<T>(object instance, string name)
        {
            var field = instance.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            return (T) (field.GetValue(instance));
        }
        private List<IPersistentIdentifierGenerator> GetPersistentIdentifierGenerators(string defaultCatalog, string defaultSchema)
        {
            var classGens = _cfg.ClassMappings.Where(pc => !pc.IsInherited)
                .Select(pc => pc.Identifier.CreateIdentifierGenerator(_dialect, defaultCatalog, defaultSchema, (RootClass) pc))
                .Cast<IPersistentIdentifierGenerator>()
                .Where(x => x != null);

            var collectionGens = _cfg.CollectionMappings.Where(c => c.IsIdentified)
                .Cast<IdentifierCollection>()
                .Select(ig => ig.Identifier.CreateIdentifierGenerator(_dialect, defaultCatalog, defaultSchema, null))
                .Cast<IPersistentIdentifierGenerator>()
                .Where(x => x != null);
            var generators = classGens.Concat(collectionGens)
                .GroupBy(g => g.GeneratorKey())
                .Select(g => g.First())
                .ToList();
            return generators;
        }

        private static IEnumerable<MigrationExpressionBase> GetUniqueKeys(Table table)
        {
            foreach (var uk in table.UniqueKeyIterator)
            {
                yield return new CreateIndexExpression
                {
                    Index = new IndexDefinition
                    {
                        SchemaName = table.Schema,
                        TableName = table.Name,
                        Name = uk.Name,
                        IsUnique = true,
                        IsClustered = false,
                        Columns = uk.Columns.Select(c => new IndexColumnDefinition
                        {
                            Name = c.Name,
                            Direction = Direction.Ascending
                        }).ToList()
                    }
                };
            }
        }

        private static IEnumerable<MigrationExpressionBase> GetIndexes(Table table)
        {
            return table.IndexIterator.Select(idx => new CreateIndexExpression
            {
                Index = new IndexDefinition
                {
                    SchemaName = table.Schema,
                    TableName = table.Name,
                    Name = idx.Name,
                    IsUnique = false,
                    IsClustered = false,
                    Columns = idx.ColumnIterator.Select(c => new IndexColumnDefinition
                    {
                        Name = c.Name,
                        Direction = Direction.Ascending
                    }).ToList()
                }
            }).Cast<MigrationExpressionBase>();
        }

        private static IEnumerable<MigrationExpressionBase> GetFKs(Table table)
        {
            return table.ForeignKeyIterator.Select(fk => new CreateForeignKeyExpression
            {
                ForeignKey = new ForeignKeyDefinition
                {
                    Name = fk.Name,
                    PrimaryTableSchema = fk.ReferencedTable.Schema,
                    PrimaryTable = fk.ReferencedTable.Name,
                    ForeignTableSchema = table.Schema,
                    ForeignTable = table.Name,
                    ForeignColumns = fk.Columns.Select(c => c.Name).ToList(),
                    PrimaryColumns = fk.ReferencedColumns.Select(c => c.Name).ToList(),
                    OnDelete = fk.CascadeDeleteEnabled ? Rule.Cascade : Rule.None,
                    OnUpdate = Rule.None
                }
            }).Cast<MigrationExpressionBase>();
        }

        private IList<ColumnDefinition> GetTableColumns(Table table, IMapping mapping)
        {
            return table.ColumnIterator.Select(c => new ColumnDefinition
            {
                TableName = table.Name,
                ColumnDescription = c.Comment,
                Name = c.Name,
                Type = GetSqlType(c, mapping),
                CustomType = c.SqlType,
                Size = Math.Max(c.Length, c.Scale),
                Precision = c.Precision,
                DefaultValue = c.DefaultValue,
                IsPrimaryKey = IsPrimaryKey(c, table),
                IsNullable = c.IsNullable,
                IsUnique = c.IsUnique,
                ModificationType = ColumnModificationType.Create
            }).ToList();
        }

        private DbType? GetSqlType(Column column, IMapping mapping)
        {
            var code = column.GetSqlTypeCode(mapping);
            if(code != null)
            {
                return code.DbType;
            }
            return null;
        }

        private bool IsPrimaryKey(Column column, Table table)
        {
            return table.PrimaryKey.ColumnIterator.Contains(column);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
