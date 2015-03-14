using NHibernate.Cfg;
using TestTarget;
using TestTarget.Firebird;
using Xunit;

namespace Engines.FluentMigrator.Tests
{
	public class TestInitialMigration : IClassFixture<Firebird>
	{
        private readonly Database _testTarget;
		private readonly Configuration _nhConfig;

		public TestInitialMigration(Firebird testTarget)
		{
			_testTarget = testTarget;
			_testTarget.CreateDatabase();

			_nhConfig = new Configuration();
			_nhConfig.DataBaseIntegration(_testTarget.ConfigureDialect);
		}
	}
}
