using System.IO;
using FluentMigrator.Expressions;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateSchema : ExpressionTemplate<CreateSchemaExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Create.Schema(\"");
            tw.Write(Expression.SchemaName);
            tw.Write("\");");
            tw.WriteLine();
        }
    }
}
