using System.Collections.Generic;
using NhMigrator.Domain.SqlOperations;

namespace NhMigrator.Domain
{
	public class Migration
	{
		public IEnumerable<SqlDdlOperation> SqlOperations { get; private set; }
	}
}
