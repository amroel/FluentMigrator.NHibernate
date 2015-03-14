using System;
using System.IO;
using FluentMigrator.Model;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateIndex : ExpressionTemplate<FluentMigrator.Expressions.CreateIndexExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            var index = Expression.Index;

            if (!index.IsUnique)
            {
                tw.Write("\t\t\t\t");
                tw.Write("Create.Index(\"{0}\").OnTable(\"{1}\")", index.Name, index.TableName);
                if (!String.IsNullOrEmpty(index.SchemaName))
                {
                    tw.Write(".InSchema(\"{0}\")", index.SchemaName);
                }
                foreach (var c in index.Columns)
                {
                    tw.WriteLine();
                    tw.Write("\t\t\t\t\t.OnColumn(\"{0}\")", c.Name);
                    tw.Write(".{0}()", c.Direction);
                }
                tw.Write(";");
                tw.WriteLine();
            }
            else
            {
                tw.Write("\t\t\t\t");
                tw.Write("Create.UniqueConstraint(\"{0}\").OnTable(\"{1}\")", index.Name, index.TableName);
                if (!String.IsNullOrEmpty(index.SchemaName))
                {
                    tw.Write(".WithSchema(\"{0}\")", index.SchemaName);
                }
                tw.WriteLine();
                tw.Write("\t\t\t\t\t.Columns(");
                bool neesComma = false;
                foreach (var c in index.Columns)
                {
                    if (neesComma)
                    {
                        tw.Write(", ");
                    }
                    tw.Write("\"{0}\"", c.Name);
                    neesComma = true;
                }
                tw.Write(");");
                tw.WriteLine();
                
            }
        }
    }
}