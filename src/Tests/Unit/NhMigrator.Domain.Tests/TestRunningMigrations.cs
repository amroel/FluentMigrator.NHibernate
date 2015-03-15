using NSubstitute;
using Xunit;
using NHibernate.Cfg;
using System.Collections.Generic;
using FluentAssertions;
using NhMigrator.Domain.SchemaInspection;
using NhMigrator.Domain.SqlOperations;
using NHibernate.Mapping.ByCode;
using NHibernate.Dialect;

namespace NhMigrator.Domain.Tests
{
    public class TestRunningMigrations
    {
		private readonly MigrationEngine _migrationEngine = Substitute.For<MigrationEngine>();
		private readonly Configuration _nhConfig = new Configuration();
		private readonly Migrator _migrator;
		private Migration _createdMigration;

		public TestRunningMigrations()
		{
			_migrator = new Migrator(_migrationEngine);
			_migrationEngine.Run(Arg.Do<Migration>(arg => _createdMigration = arg));
		}

		[Fact]
		public void Migrate_RunsMigrationEngine()
		{
			_migrator.Migrate(_nhConfig);
			_createdMigration.Should().NotBeNull();
		}

		[Fact]
		public void Migrate_RunsMigrationEngineWithCorrectSqlOperations()
		{
			MapSimpleEntity();
			var expectedOperations = new List<SqlDdlOperation>
			{
				new CreateTable(new MappedTable("simple_table"))
			};

			_migrator.Migrate(_nhConfig);

			_createdMigration.SqlOperations.Should().BeEquivalentTo(expectedOperations);
		}

		private void MapSimpleEntity()
		{
			var modelMapper = new ModelMapper();
			modelMapper.Class<NhMigrator.Tests.Entities.SimpleEntity>(map =>
			{
				map.Id(x => x.Id, idMap => idMap.Generator(Generators.Assigned));
				map.Property(x => x.IntProperty);
				map.Table("simple_table");
			});

			_nhConfig.DataBaseIntegration(db =>
				{
					db.Dialect<GenericDialect>(); // We don't care about a concrete dialect here!
				});
			_nhConfig.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());
		}
    }
}
