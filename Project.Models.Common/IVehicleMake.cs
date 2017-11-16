using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Common
{
    public interface IVehicleMake
    {
        #region Properties

        Guid Id { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

        #endregion Properties
    }
}
