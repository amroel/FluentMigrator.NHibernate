using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Expressions;
using Xunit;

namespace FluentMigrator.NHibernate.Test
{
    public class Class1
    {
        [Fact]
        public void Export()
        {
            var baseType = typeof (MigrationExpressionBase);
            var expressions = baseType.Assembly.GetExportedTypes()
                .Where(x => baseType.IsAssignableFrom(x)).ToList();

            foreach (var e in expressions)
            {
                Console.WriteLine(GetClass(e));
            }




        }

     
    }
}
