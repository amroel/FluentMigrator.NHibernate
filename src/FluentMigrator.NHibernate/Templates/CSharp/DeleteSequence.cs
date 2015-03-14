using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class DeleteSequence : ExpressionTemplate<FluentMigrator.Expressions.DeleteSequenceExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Delete.Sequence(\"{0}\")", Expression.SequenceName);
            if (!String.IsNullOrEmpty(Expression.SchemaName))
            {
                tw.Write(".InSchema(\"{0}\")", Expression.SchemaName);
            }
            tw.Write(";");
            tw.WriteLine();
        }
    }
}