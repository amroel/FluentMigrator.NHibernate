using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentMigrator.Expressions;

namespace FluentMigrator.NHibernate.Templates.CSharp
{


    public  class Migration : ITemplate
    {
        public virtual string Namespace { get; set; }
        public virtual string Name { get; set; }
        public virtual IEnumerable<MigrationExpressionBase> Expressions { get; set; }
        public virtual ITemplateFromExpressionFactory TemplateFactory { get; set; }


        public void WriteTo(TextWriter tw)
        {
            var expressions = Expressions.ToList();
            var reverseExpressions = expressions.Select(e =>
            {
                try
                {
                    return (MigrationExpressionBase)e.Reverse();
                }
                catch (NotSupportedException)
                {
                    return null;
                }
            }).Where(x => x != null).ToList();

            tw.WriteLine("using System;");
            tw.WriteLine("using System.Collections.Generic;");
            tw.WriteLine("using System.Linq;");
            tw.WriteLine("using System.Web;");
            tw.WriteLine("using System.Linq.Expressions;");
            tw.WriteLine("using FluentMigrator;");
            tw.WriteLine();
            tw.WriteLine("namespace"); 
            tw.Write(Namespace);
            tw.WriteLine();
            tw.WriteLine("{");
            tw.Write("\t[Migration({0:yyyyMMddHHmmss})]", DateTime.Now);
            tw.WriteLine();

            tw.WriteLine("\tpublic class "+Name+" : Migration");
            tw.WriteLine("\t{");
            tw.WriteLine("\t\tpublic override void Up()");
            tw.WriteLine("\t\t\t{");
            foreach (var templ in expressions.Select(e=>TemplateFactory.GetTemplate(e)))
            {
                templ.WriteTo(tw);
            }
            tw.WriteLine("\t\t\t}");

            tw.WriteLine("\t\tpublic override void Down()");
            tw.WriteLine("\t\t\t{");

            foreach (var templ in reverseExpressions.Select(e => TemplateFactory.GetTemplate(e)))
            {
                templ.WriteTo(tw);
            }
            tw.WriteLine("\t\t\t}");


            tw.WriteLine("\t\t}");
            tw.WriteLine("\t}");
            tw.WriteLine("}");
        }
    }


}


