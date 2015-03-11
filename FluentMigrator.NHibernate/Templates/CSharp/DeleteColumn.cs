using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class DeleteColumn : ExpressionTemplate<FluentMigrator.Expressions.DeleteColumnExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            foreach (var c in Expression.ColumnNames)
            {
                tw.Write("\t\t\t\t");
                tw.Write("Delete.Column(\"{0}\").FromTable(\"{1}\")", c, Expression.TableName);
                if (!String.IsNullOrEmpty(Expression.SchemaName))
                {
                    tw.Write(".InSchema(\"{0}\")", Expression.SchemaName);
                }
                tw.Write(";");
                tw.WriteLine();
            }

        }
    }
}