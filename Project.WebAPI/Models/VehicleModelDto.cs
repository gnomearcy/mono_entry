using System;
using System.ComponentModel.DataAnnotations;

namespace Project.WebAPI.Models
{
    public class VehicleModelDto
    {
        #region Properties
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid MakeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }

        #endregion Properties
    }
}