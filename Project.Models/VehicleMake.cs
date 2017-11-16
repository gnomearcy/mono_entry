using Project.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    /// <summary>
    /// Class implements the interface with equivalent properties
    /// so the interface type can be used in ApiController methods as input parameter
    /// and delegated to the Service.
    /// 
    /// CustomCreationConverter is made for the interface type that maps it to this type.
    /// </summary>
    public class VehicleMake : IVehicleMake
    {
        #region Properties

        public Guid Id{ get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv{ get; set; }

        #endregion Properties
    }
}
