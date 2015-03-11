using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateTable : ExpressionTemplate<FluentMigrator.Expressions.CreateTableExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            throw new NotImplementedException("FluentMigrator.Expressions.CreateTableExpression");
        }
    }
}