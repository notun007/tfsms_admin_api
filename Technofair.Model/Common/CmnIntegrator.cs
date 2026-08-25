using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.Common
{
    public class CmnIntegrator
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public short CmnServiceTypeId { get; set; }
        public string? Url { get; set; }
        public bool HasProductApi { get; set; }
        public bool HasNetworkApi { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
