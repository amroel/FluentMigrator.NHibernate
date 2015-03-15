
namespace NhMigrator.Domain
{
	public interface MigrationEngine
	{
		void Run(Migration migration);
	}
}
