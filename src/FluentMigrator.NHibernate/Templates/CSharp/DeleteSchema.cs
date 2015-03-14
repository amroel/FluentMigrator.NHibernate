using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class DeleteSchema : ExpressionTemplate<FluentMigrator.Expressions.DeleteSchemaExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Delete.Schema(\"{0}\");", Expression.SchemaName);
            tw.WriteLine();
        }
    }
}