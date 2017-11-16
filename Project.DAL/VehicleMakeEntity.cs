using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class VehicleMakeEntity : BaseEntity
    {
        #region Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        #endregion Properties
    }
}
