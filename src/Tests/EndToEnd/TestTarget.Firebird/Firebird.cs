using System;
using System.IO;
using System.Text;
using System.Threading;
using FirebirdSql.Data.FirebirdClient;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace TestTarget.Firebird
{
	public class Firebird : TestDatabase
	{
		private readonly string _connectionString = string.Format("User=SYSDBA;Password=masterkey;Database={0};", Path.Combine(Directory.GetCurrentDirectory(), "fbtest.fdb"));

		public void CreateDatabase()
		{
			if (File.Exists("fbtest.fdb"))
				FbConnection.DropDatabase(_connectionString);

			FbConnection.CreateDatabase(_connectionString);
		}

		public void ConfigureDialect(IDbIntegrationConfigurationProperties dbProperteis)
		{
			dbProperteis.Dialect<FirebirdDialect>();
			dbProperteis.Driver<FirebirdClientDriver>();
			dbProperteis.ConnectionString = _connectionString;
		}

		public void DropDatabase()
		{
			FbConnection.ClearAllPools();

			// Avoid "lock time-out on wait transaction" exception
			var retries = 5;
			while (true)
			{
				try
				{
					FbConnection.DropDatabase(_connectionString);
					break;
				}
				catch
				{
					if (--retries == 0)
						throw;
					else
						Thread.Sleep(100);
				}
			}
		}

		public bool TableExists(string tableName)
		{
			return IsInDatabase(cmd =>
			{
				cmd.CommandText = "select rdb$relation_name from rdb$relations where (rdb$flags is not null) and (rdb$relation_name = @table)";
				cmd.Parameters.AddWithValue("table", tableName.ToUpper());
			});
		}

		public bool ColumnExists(string tableName, string columnName)
		{
			return IsInDatabase(cmd =>
			{
				cmd.CommandText = "select rdb$field_name from rdb$relation_fields where (rdb$relation_name = @table) and (rdb$field_name = @column)";
				cmd.Parameters.AddWithValue("table", tableName.ToUpper());
				cmd.Parameters.AddWithValue("column", columnName.ToUpper());
			});
		}

		public bool SupportsSequences
		{
			get { return true; }
		}

		public bool SequenceExists(string sequenceName)
		{
			return IsInDatabase(cmd =>
			{
				cmd.CommandText = "select rdb$generator_name from rdb$generators where rdb$generator_name = @sequence";
				cmd.Parameters.AddWithValue("sequence", sequenceName.ToUpper());
			});
		}

		public bool ForeignKeyExists(string tableName, string fkColumn)
		{
			var sql = new StringBuilder()
				.AppendLine("select con.rdb$relation_name table_name, ix_seg.rdb$field_name field")
				.AppendLine("from rdb$index_segments ix_seg")
				.AppendLine("join rdb$indices ix on ix.rdb$index_name = ix_seg.rdb$index_name")
				.AppendLine("join rdb$relation_constraints con on con.rdb$index_name = ix_seg.rdb$index_name")
				.AppendLine("where con.rdb$constraint_type = 'FOREIGN KEY'")
				.Append("and con.rdb$relation_name = @table and ix_seg.rdb$field_name = @field")
				.ToString();
			return IsInDatabase(cmd =>
			{
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue("table", tableName.ToUpper());
				cmd.Parameters.AddWithValue("field", fkColumn.ToUpper());
			});
		}

		private bool IsInDatabase(Action<FbCommand> adjustCommand)
		{
			var result = false;
			using (var connection = new FbConnection(_connectionString))
			{
				connection.Open();
				using (var tx = connection.BeginTransaction())
				{
					using (var cmd = connection.CreateCommand())
					{
						cmd.Transaction = tx;
						adjustCommand(cmd);
						using (var reader = cmd.ExecuteReader())
						{
							result = reader.Read();
						}
					}
					tx.Commit();
				}
				connection.Close();
			}
			return result;
		}
	}
}
