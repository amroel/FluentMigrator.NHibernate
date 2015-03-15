
namespace NhMigrator.Domain.SchemaInspection
{
	public class MappedTable
	{
		public MappedTable(string tableName)
		{
			TableName = tableName;
		}

		public string TableName { get; private set; }
	}
}
