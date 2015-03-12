using System;
using System.IO;
using System.Linq;

namespace FluentMigrator.NHibernate.Templates.CSharp
{
    public class CreateForeignKey : ExpressionTemplate<FluentMigrator.Expressions.CreateForeignKeyExpression>
    {
        public override void WriteTo(TextWriter tw)
        {
            var index = Expression.ForeignKey;
            tw.Write("\t\t\t\t");
            tw.Write("Create.ForeignKey(\"{0}\")", index.Name);
            tw.WriteLine();
            tw.Write("\t\t\t\t\t");
            tw.Write(".FromTable(\"{0}\")", index.ForeignTable);
            if (!String.IsNullOrEmpty(index.ForeignTableSchema))
            {
                tw.Write(".InSchema(\"{0}\")", index.ForeignTableSchema);
            }
            tw.Write(".ForeignColumns(");
            tw.Write(String.Join(", ", index.ForeignColumns.Select(x => "\"" + x + "\"")));
            tw.Write(")");
            tw.WriteLine();
            tw.Write("\t\t\t\t\t");
            tw.Write(".ToTable(\"{0}\")", index.PrimaryTable);
            if (!String.IsNullOrEmpty(index.PrimaryTableSchema))
            {
                tw.Write(".InSchema(\"{0}\")", index.PrimaryTableSchema);
            }
            tw.Write(".PrimaryColumns(");
            tw.Write(String.Join(", ", index.ForeignColumns.Select(x => "\"" + x + "\"")));
            tw.Write(")"); 
            tw.Write(";");
            tw.WriteLine();
        }
    }
}