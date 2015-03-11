using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class AlterSchema : ExpressionTemplate<FluentMigrator.Expressions.AlterSchemaExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            throw new NotImplementedException("FluentMigrator.Expressions.AlterSchemaExpression");
        }
    }
}