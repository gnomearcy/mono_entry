using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Mapping
{
    public class VehicleMakeMap : EntityTypeConfiguration<VehicleMakeEntity>
    {
        public VehicleMakeMap()
        {
            // Primary key
            HasKey(t => t.Id);

            // Properties
            Property(c => c.Id).IsRequired();
            Property(c => c.Name).IsRequired();
            Property(c => c.Abrv).IsRequired();

            // Table
            ToTable("Makes");
        }
    }
}
