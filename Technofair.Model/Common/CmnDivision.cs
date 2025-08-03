using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TFSMS.Admin.Model.Common
{
    [Table(name: "CmnDivisions")]
    public class CmnDivision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? NameInBangla { get; set; }
        
        public int? CmnCountryId { get; set; }
    }
}
