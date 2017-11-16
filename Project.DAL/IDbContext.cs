using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Project.DAL
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : BaseEntity;

        int SaveChanges();
    }
}
