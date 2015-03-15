using System;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Migrator.Tests.Entities;
using TestTarget;
using TestTarget.Firebird;
using Xunit;
using FluentAssertions;
using NHibernate.Migrator.Engines.FluentMigrator;

namespace Engines.FluentMigrator.Tests
{
	public class TestInitialMigration : IClassFixture<Firebird>
	{
		private readonly TestDatabase _testTarget;
		private readonly Configuration _nhConfig = new Configuration();
		private readonly FluentMigratorMigrationEngine _migrator = new FluentMigratorMigrationEngine();

		public TestInitialMigration(Firebird testTarget)
		{
			_testTarget = testTarget;
			_testTarget.CreateDatabase();

			_nhConfig.DataBaseIntegration(_testTarget.ConfigureDialect);
		}

		[Fact]
		public void EntityTableShouldBeCreated()
		{
			MapEntity(idMap => idMap.Generator(Generators.Native));

			_migrator.Migrate(_nhConfig);

			_testTarget.TableExists("simple_table").Should().BeTrue("migrator should have created simple_table");
		}

		private void MapEntity(Action<IIdMapper> idMapper)
		{
			var modelMapper = new ModelMapper();
			modelMapper.Class<SimpleEntity>(map =>
			{
				map.Id(x => x.Id, idMapper);
				map.Property(x => x.IntProperty);
				map.Table("simple_table");
			});
			_nhConfig.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());
		}
	}
}
