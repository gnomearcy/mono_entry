using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class VehicleModelEntity : BaseEntity
    {
        #region Properties
        public Guid Id { get; set; }

        [ForeignKey("MakeEntity")]
        public Guid MakeId { get; set; }

        public VehicleMakeEntity MakeEntity { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        #endregion Properties
    }
}
