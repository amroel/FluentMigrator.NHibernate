using NHibernate.Cfg.Loquacious;

namespace TestTarget
{
	public interface TestDatabase
	{
		void CreateDatabase();
		void ConfigureDialect(IDbIntegrationConfigurationProperties dbProperteis);
		void DropDatabase();
		bool TableExists(string tableName);
		bool ColumnExists(string tableName, string columnName);
		bool SupportsSequences { get; }
		bool SequenceExists(string sequenceName);
		bool ForeignKeyExists(string tableName, string fkColumn);
	}
}
