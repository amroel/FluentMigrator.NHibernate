using System;
using System.IO;
using FluentMigrator.Model;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateTable : ExpressionTemplate<FluentMigrator.Expressions.CreateTableExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.WriteLine("\t\t\t\tthrow new NotImplementedException(\"Working on this\");");
            return;
            tw.Write("\t\t\t\t");
            tw.Write("Create.Table(\"{0}\")", Expression.TableName);
            if (!String.IsNullOrEmpty(Expression.TableDescription))
            {
                tw.Write(".WithDescription(\"{0}\")", Expression.TableDescription);
            }
            if (!String.IsNullOrEmpty(Expression.SchemaName))
            {
                tw.Write(".InSchema(\"{0}\")", Expression.SchemaName);
            }
            foreach (ColumnDefinition c in Expression.Columns)
            {
                tw.WriteLine();
                tw.Write("\t\t\t\t\t");
                tw.Write(".WithColumn(\"{0}\")", c.Name);
            }

            tw.Write(";");
            tw.WriteLine();
        }
    }
}