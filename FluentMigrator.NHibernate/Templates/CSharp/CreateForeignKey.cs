using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateForeignKey : ExpressionTemplate<FluentMigrator.Expressions.CreateForeignKeyExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            throw new NotImplementedException("FluentMigrator.Expressions.CreateForeignKeyExpression");
        }
    }
}