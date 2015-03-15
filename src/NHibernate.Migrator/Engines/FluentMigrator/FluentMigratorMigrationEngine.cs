using NHibernate.Cfg;
using NHibernate.Migrator.Domain;

namespace NHibernate.Migrator.Engines.FluentMigrator
{
    public class FluentMigratorMigrationEngine : MigrationEngine
    {

		public void Migrate(Configuration nhConfig)
		{
		}

		#region MigrationEngine Members

		public void Run(Migration migration)
		{
		}

		#endregion
	}
}
