using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class DeleteDefaultConstraint : ExpressionTemplate<FluentMigrator.Expressions.DeleteDefaultConstraintExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Delete.DefaultConstraint().OnTable(\"{0}\")",Expression.TableName);
            if (!String.IsNullOrEmpty(Expression.SchemaName))
            {
                tw.Write(".InSchema(\"{0}\")", Expression.SchemaName);
            }
            tw.Write(".OnColumn(\"{0}\")", Expression.ColumnName);
            tw.Write(";");
            tw.WriteLine();
        }
    }
}