using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class RenameTable : ExpressionTemplate<FluentMigrator.Expressions.RenameTableExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Rename.Table(\"{0}\").To(\"{1}\")", Expression.OldName, Expression.NewName);
            if (!String.IsNullOrEmpty(Expression.SchemaName))
            {
                tw.Write(".InSchema(\"{0}\")",Expression.SchemaName);
            }
            tw.Write(";");
            tw.WriteLine();
        }
    }
}