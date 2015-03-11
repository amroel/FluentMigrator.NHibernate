using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Expressions;
using System.Linq.Expressions;

namespace FluentMigrator.NHibernate
{
    public class TemplateFromExpressionFactory : ITemplateFromExpressionFactory
    {
        private Dictionary<Type, Func<MigrationExpressionBase, ITemplate>> _templateLookup = InitTemplates();

        private static Dictionary<Type, Func<MigrationExpressionBase, ITemplate>> InitTemplates()
        {
            return typeof (TemplateFromExpressionFactory)
                .Assembly.GetExportedTypes()
                .Where(t => t.Namespace == "FluentMigrator.NHibernate.Templates.CSharp")
                .Where(IsAnExpressionTemplate)
                .ToDictionary(x => x.GetGenericArguments()[0], x => GetTemplateFactory(x));


        }
        private static Func<MigrationExpressionBase, ITemplate> GetTemplateFactory (Type type)
        {
            var prop = type.GetProperty("Expression");
            //TODO Expresionize this;
            return (MigrationExpressionBase meb) =>
            {
                var instance = Activator.CreateInstance(type);
                prop.SetValue(instance, meb);
                return (ITemplate) instance;
            };
        }
        private static bool IsAnExpressionTemplate(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Templates.ExpressionTemplate<>);
        }

        public ITemplate GetTemplate(MigrationExpressionBase expr)
        {
            var expressionType = expr.GetType();
            if (_templateLookup.ContainsKey(expressionType))
            {
                return _templateLookup[expressionType](expr);
            }
            return new BadExpressionTemplate(expressionType);
        }

        private class BadExpressionTemplate : ITemplate
        {
            private readonly Type _t;

            public BadExpressionTemplate(Type t)
            {
                _t = t;
            }

            public void WriteTo(TextWriter tw)
            {
                tw.Write("throw new NotImplementedException(\"No template implemented for {0}\");", _t.FullName);
            }
        }
    }
}
