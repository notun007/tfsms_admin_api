using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFSMS.Admin.Model.Common
{
    [Table(name: "CmnUnions")]
    public class CmnUnion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CmnUpazillaId { get; set; }
        public string Name { get; set; }
        public string? NameInBangla { get; set; }
        //public string? url { get; set; }
    }
}
