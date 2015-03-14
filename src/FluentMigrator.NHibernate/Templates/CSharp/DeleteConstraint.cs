using System;
using System.IO;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class DeleteConstraint : ExpressionTemplate<FluentMigrator.Expressions.DeleteConstraintExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            var constratint = Expression.Constraint;
            if (constratint.IsUniqueConstraint)
            {
                tw.Write("\t\t\t\t");
                tw.Write("Delete.UniqueConstraint(\"{0}\").FromTable(\"{1}\")", constratint.ConstraintName, constratint.TableName);
                if (!String.IsNullOrEmpty(constratint.SchemaName))
                {
                    tw.Write(".InSchema(\"{0}\")", constratint.SchemaName);
                }
                tw.Write(";");
                tw.WriteLine();
            }
            else if (constratint.IsPrimaryKeyConstraint)
            {
                tw.Write("\t\t\t\t");
                tw.Write("Delete.PrimaryKey(\"{0}\").FromTable(\"{1}\")", constratint.ConstraintName, constratint.TableName);
                if (!String.IsNullOrEmpty(constratint.SchemaName))
                {
                    tw.Write(".InSchema(\"{0}\")", constratint.SchemaName);
                }
                tw.Write(";");
                tw.WriteLine();
            }
            
        }
    }
}