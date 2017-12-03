using Project.Models.Common;
using Project.Models.Dto;
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
        #region Make 
        /// <summary>
        /// Creates a make if it doesn't exist in the database
        /// or updates the existing model.
        /// </summary>
        /// <param name="make">Model to create or update</param>
        /// <returns>The newly created model, updated model or null if there was an error.</returns>
        Task<ServiceStatusCode> CreateMake(IVehicleMake make);

        Task<ServiceStatusCode> UpdateMake(IVehicleMake updatedMake);

        Task<ServiceStatusCode> DeleteMake(Guid id);

        Task<MakePageDto> GetMakePageFor(MakePagePayload payload);
        #endregion

        #region Model
        /// <summary>
        /// Creates a model if it doesn't exist in the database
        /// or updates the existing model.
        /// </summary>
        /// <param name="model">Model to create or update</param>
        /// <returns>The newly created model, updated model or null if there was an error.</returns>
        Task<ServiceStatusCode> CreateModel(IVehicleModel model);

        Task<ServiceStatusCode> UpdateModel(IVehicleModel model);

        Task<ServiceStatusCode> DeleteModel(Guid id);

        /// <summary>
        /// Deletes all models that have the MakeId equal to passed Id.
        /// Also deletes the Make object from database.
        /// The implementation is an example of UnitOfWork pattern.
        /// </summary>
        /// <param name="makeId">Id of Make object to delete from database</param>
        Task<int> DeleteModelsByMake(Guid makeId);
        #endregion Model
    }
}
