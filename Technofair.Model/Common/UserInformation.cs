using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFSMS.Admin.Model.Common
{
    [Table(name: "UserInformation")]
    public class UserInformation : CmnBaseEntity
    {
        private string firstName = string.Empty;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        [MaxLength(50)]
        public string FirstName { get => firstName; set => firstName = value; }
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        public string? Email { get; set; }
        [Required(ErrorMessage = "MobileNo is required")]
        public string MobileNo { get; set; } = string.Empty;
        [MaxLength(17)]
        public string? NID { get; set; } = string.Empty;

        public string PresentAddress { get; set; } = string.Empty;
        public string PermanantAddress { get; set; } = string.Empty;
        [Required(ErrorMessage = "User Role is userRole")]
        public int RoleId { get; set; }
        public string? City { get; set; }

        public string? State { get; set; }

        public string? Zip { get; set; }
        [Required(ErrorMessage = "Compnay is required")]
        public string? Company { get; set; }

        public string? Country { get; set; }


    }
}
