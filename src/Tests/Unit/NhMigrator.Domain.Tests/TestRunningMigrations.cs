using NSubstitute;
using Xunit;

namespace NhMigrator.Domain.Tests
{
    public class TestRunningMigrations
    {
		private readonly MigrationEngine _migrationEngine = Substitute.For<MigrationEngine>();
		private readonly Migrator _migrator;

		public TestRunningMigrations()
		{
			_migrator = new Migrator(_migrationEngine);
		}

		[Fact]
		public void Migrate_RunsMigrationEngine()
		{
			_migrator.Migrate(new NHibernate.Cfg.Configuration());
			_migrationEngine.Received(1).Run(Arg.Any<Migration>());
		}
    }
}
