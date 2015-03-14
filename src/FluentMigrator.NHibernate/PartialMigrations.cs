using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentMigrator.NHibernate
{
    public partial class Migrations
    {
        public IDefinitionsBuilder Definitions { get; set; }
        public string Namespace { get; set; }
        public string GetCurrentTimeStamp()
        {
            return String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
        }
    }
}
