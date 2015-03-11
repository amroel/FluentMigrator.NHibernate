using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateIndex : ExpressionTemplate<FluentMigrator.Expressions.CreateIndexExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            throw new NotImplementedException("FluentMigrator.Expressions.CreateIndexExpression");
        }
    }
}