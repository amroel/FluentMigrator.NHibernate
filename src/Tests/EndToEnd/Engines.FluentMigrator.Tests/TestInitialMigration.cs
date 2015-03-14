using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTarget.Firebird;
using Xunit;

namespace Engines.FluentMigrator.Tests
{
	public class TestInitialMigration : IClassFixture<Firebird>, IClassFixture<SQLite>
	{
	}
}
