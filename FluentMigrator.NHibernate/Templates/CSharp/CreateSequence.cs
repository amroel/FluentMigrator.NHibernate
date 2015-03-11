using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateSequence : ExpressionTemplate<FluentMigrator.Expressions.CreateSequenceExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            tw.Write("\t\t\t\t");
            tw.Write("Delete.Sequence(\"{0}\")", Expression.Sequence.Name);
            if (!String.IsNullOrEmpty(Expression.Sequence.SchemaName))
            {
                tw.Write(".InSchema(\"{0}\")", Expression.Sequence.SchemaName);
            }
            //TODO: All the other schema options that arent encoded in nhibernate
            tw.Write(";");
            tw.WriteLine();
        }
    }
}