using System;
using NHibernate.Cfg;

namespace NhMigrator.Domain
{
	public class Migrator
	{
		private readonly MigrationEngine _migrationEngine;

		public Migrator(MigrationEngine migrationEngine)
		{
			_migrationEngine = migrationEngine;
		}

		public void Migrate(Configuration configuration)
		{
			_migrationEngine.Run(new Migration());
		}
	}
}
