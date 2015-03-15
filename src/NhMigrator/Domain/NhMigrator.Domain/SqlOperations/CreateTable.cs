using NhMigrator.Domain.SchemaInspection;

namespace NhMigrator.Domain.SqlOperations
{
	public class CreateTable : SqlDdlOperation
	{
		public CreateTable(MappedTable mappedTable)
		{
			MappedTable = mappedTable;
		}

		public MappedTable MappedTable { get; private set; }
	}
}
