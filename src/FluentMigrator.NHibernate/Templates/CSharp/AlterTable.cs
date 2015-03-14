using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class AlterTable : ExpressionTemplate<FluentMigrator.Expressions.AlterTableExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            throw new NotImplementedException("FluentMigrator.Expressions.AlterTableExpression");
        }
    }
}