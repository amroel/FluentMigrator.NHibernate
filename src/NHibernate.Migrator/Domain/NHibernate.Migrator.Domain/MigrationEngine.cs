
namespace NHibernate.Migrator.Domain
{
	public interface MigrationEngine
	{
		void Run(Migration migration);
	}
}
