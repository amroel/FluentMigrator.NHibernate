using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentMigrator.NHibernate.Test
{
    public class Example : Migration
    {
        public override void Up()
        {
            Create.Index("").OnTable("")
                .OnColumn().
            throw new NotImplementedException();
            
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
