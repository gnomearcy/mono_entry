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
        Task<ICollection<IVehicleModel>> GetAllModels();

        /// <summary>
        /// Gets all makes.
        /// API method will just ask this service for data.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<IVehicleMake>> GetAllMakes();

        /// <summary>
        /// Creates a model if it doesn't exist in the database
        /// or updates the existing model.
        /// </summary>
        /// <param name="model">Model to create or update</param>
        /// <returns>The newly created model, updated model or null if there was an error.</returns>
        Task<IVehicleModel> CreateUpdateModel(IVehicleModel model);

        /// <summary>
        /// Creates a make if it doesn't exist in the database
        /// or updates the existing model.
        /// </summary>
        /// <param name="make">Model to create or update</param>
        /// <returns>The newly created model, updated model or null if there was an error.</returns>
        Task<IVehicleMake> CreateUpdateMake(IVehicleMake make);

        Task<int> DeleteMake(Guid id);

        /// <summary>
        /// Deletes all models that have the MakeId equal to passed Id.
        /// Also deletes the Make object from database.
        /// The implementation is an example of UnitOfWork pattern.
        /// </summary>
        /// <param name="makeId">Id of Make object to delete from database</param>
        Task<int> DeleteModelsByMake(Guid makeId);
    }
}
