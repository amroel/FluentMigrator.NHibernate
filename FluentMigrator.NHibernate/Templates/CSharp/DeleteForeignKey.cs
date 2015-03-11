using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class DeleteForeignKey : ExpressionTemplate<FluentMigrator.Expressions.DeleteForeignKeyExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Delete.ForeignKey(\"{0}\").OnTable(\"{1}\")", Expression.ForeignKey.Name, Expression.ForeignKey.ForeignTable);
            if (!String.IsNullOrEmpty(Expression.ForeignKey.ForeignTableSchema))
            {
                tw.Write(".InSchema(\"{0}\")", Expression.ForeignKey.ForeignTableSchema);
            }
            tw.Write(";");
            tw.WriteLine();
        }
    }
}