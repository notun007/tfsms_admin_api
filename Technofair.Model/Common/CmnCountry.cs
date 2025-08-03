using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFSMS.Admin.Model.Common
{
    [Table(name: "CmnCountries")]
    public class CmnCountry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }=string.Empty;
        [MaxLength(50)]
        public string? ShortName { get; set; }

        public bool IsActive { get; set; } = true;




    }
}
