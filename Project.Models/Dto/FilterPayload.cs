using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Dto
{
    public class FilterPayload
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }
    }
}
