using NHibernate.Cfg.Loquacious;

namespace TestTarget
{
	public abstract class Database
	{
		public abstract void CreateDatabase();
		public abstract void ConfigureDialect(IDbIntegrationConfigurationProperties dbProperteis);
		public abstract void DropDatabase();
		public abstract bool TableExists(string tableName);
		public abstract bool ColumnExists(string tableName, string columnName);
		public abstract bool SupportsSequences { get; }
		public abstract bool SequenceExists(string sequenceName);
		public abstract bool ForeignKeyExists(string tableName, string fkColumn);
	}
}
