using Project.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Tests
{
    public class DatabaseFixture
    {
        public DbTestDoubles Db { private set; get; }

        public DatabaseFixture()
        {
            this.Db = new DbTestDoubles();
        }
    }
}
