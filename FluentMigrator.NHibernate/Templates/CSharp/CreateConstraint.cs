using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateConstraint : ExpressionTemplate<FluentMigrator.Expressions.CreateConstraintExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            throw new NotImplementedException("FluentMigrator.Expressions.CreateConstraintExpression");
        }
    }
}