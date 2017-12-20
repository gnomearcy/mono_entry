using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.WebAPI.Models
{
    public class VehicleMakeDto
    {
        #region Properties

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }

        #endregion Properties
    }
}