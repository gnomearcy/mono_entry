using Project.Models.Common.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Filter
{
    public class FilteringPayload : IMakeFilterParameters
    {
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
