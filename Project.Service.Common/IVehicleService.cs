using Project.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: 
// 1. should we filter on the backend or just return all the results?
namespace Project.Service.Common
{
    /// <summary>
    /// Application business logic component.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Gets all models.
        /// API method will just ask this service for data.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IVehicleModel> GetAllModels();

        /// <summary>
        /// Gets all makes.
        /// API method will just ask this service for data.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IVehicleMake> GetAllMakes();

        /// <summary>
        /// Creates a model if it doesn't exist in the database
        /// or updates the existing model.
        /// </summary>
        /// <param name="model">Model to create or update</param>
        /// <returns>The newly created model, updated model or null if there was an error.</returns>
        IVehicleModel CreateUpdateModel(IVehicleModel model);

        /// <summary>
        /// Creates a make if it doesn't exist in the database
        /// or updates the existing model.
        /// </summary>
        /// <param name="make">Model to create or update</param>
        /// <returns>The newly created model, updated model or null if there was an error.</returns>
        IVehicleMake CreateUpdateMake(IVehicleMake make);

        void DeleteMake(Guid id);
    }
}
