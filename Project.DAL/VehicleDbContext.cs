using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Project.DAL.Mapping;
using System.Data.Entity.ModelConfiguration.Conventions;
using Project.DAL.MockData;
using System.Reflection;

namespace Project.DAL
{
    public class VehicleDbContext : DbContext, IDbContext
    {
        // In Entity framework terminology, a DbSet is a table, and an Entity is a table row
        public DbSet<VehicleMakeEntity> Make { get; set; }
        public DbSet<VehicleModelEntity> Model { get; set; }

        public VehicleDbContext() : base("name=VehicleString")
        {
            Database.SetInitializer<VehicleDbContext>(new Vehicles());
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            //builder.Configurations.Add(new VehicleMakeMap());
            //builder.Configurations.Add(new VehicleModelMap());

            // Use reflection to scan the Assembly for all types (called maps) extending from EntityTypeConfiguration class
            // and add them to the builder parameter.
            // This will allow for each new map to be automatically added to the builder.
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
            type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(builder);
        }

        public new IDbSet<T> Set<T>() where T: class
        {
            return base.Set<T>();
        }
    }
}
