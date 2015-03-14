using System.Linq;
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
                //Console.WriteLine(GetClass(e));
            }
        }
    }
}
