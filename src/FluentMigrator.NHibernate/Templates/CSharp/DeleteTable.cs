using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class DeleteTable : ExpressionTemplate<FluentMigrator.Expressions.DeleteTableExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Delete.Table(\"{0}\")", Expression.TableName);
            if (!String.IsNullOrEmpty(Expression.SchemaName))
            {
                tw.Write(".InSchema(\"{0}\")", Expression.SchemaName);
            }
            tw.Write(";");
            tw.WriteLine();
        }
    }
}