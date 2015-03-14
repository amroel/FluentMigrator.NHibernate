using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class RenameColumn : ExpressionTemplate<FluentMigrator.Expressions.RenameColumnExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Rename.Column(\"{0}\").OnTable(\"{1}\")", Expression.OldName, Expression.TableName);
            if (!String.IsNullOrEmpty(Expression.SchemaName))
            {
                tw.Write(".InSchema(\"{0}\")", Expression.SchemaName);
            }
            tw.Write(".To(\"{0}\")", Expression.NewName);
            tw.Write(";");
            tw.WriteLine();
        }
    }
}