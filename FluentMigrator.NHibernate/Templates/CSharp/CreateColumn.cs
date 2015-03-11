using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateColumn : ExpressionTemplate<FluentMigrator.Expressions.CreateColumnExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            throw new NotImplementedException("FluentMigrator.Expressions.CreateColumnExpression");
        }
    }
}