using NHibernate.Cfg;
using NhMigrator.Domain;

namespace NhMigrator.Engines.FluentMigrator
{
    public class FluentMigratorMigrationEngine : MigrationEngine
    {
		public void Migrate(Configuration nhConfig)
		{
			var migrator = new Migrator(this);
			migrator.Migrate(nhConfig);
		}

		#region MigrationEngine Members

		public void Run(Migration migration)
		{
		}

		#endregion
	}
}
