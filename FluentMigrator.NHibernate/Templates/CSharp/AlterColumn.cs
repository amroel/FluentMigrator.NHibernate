using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class AlterColumn : ExpressionTemplate<FluentMigrator.Expressions.AlterColumnExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            throw new NotImplementedException("FluentMigrator.Expressions.AlterColumnExpression");
        }
    }
}