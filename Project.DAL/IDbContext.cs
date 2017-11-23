using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Project.DAL
{
    public interface IDbContext
    {
        DbSet<VehicleModelEntity> Model { get; }
        DbSet<VehicleMakeEntity> Make { get; }
        IDbSet<T> Set<T>() where T : class;

        int SaveChanges();
    }
}
