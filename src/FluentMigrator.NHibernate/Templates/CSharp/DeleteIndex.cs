using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class DeleteIndex : ExpressionTemplate<FluentMigrator.Expressions.DeleteIndexExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Delete.Index(\"{0}\").OnTable(\"{1}\")", Expression.Index.Name, Expression.Index.TableName);
            if (!String.IsNullOrEmpty(Expression.Index.SchemaName))
            {
                tw.Write(".InSchema(\"{0}\")", Expression.Index.SchemaName);
            }
            tw.Write(";");
            tw.WriteLine();
        }
    }
}