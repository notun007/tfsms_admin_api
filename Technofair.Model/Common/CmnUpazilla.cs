using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFSMS.Admin.Model.Common
{
    public class CmnUpazilla
    {
        public int Id { get; set; }
        public int CmnDistrictId { get; set; }
        public string Name { get; set; }
        public string? NameInBangla { get; set; }
        
    }
}
