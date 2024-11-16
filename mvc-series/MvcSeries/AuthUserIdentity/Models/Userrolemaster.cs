using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthUserIdentity.Models
{
    [Table("Userrolemaster")]
    public class Userrolemaster
    {
        public short Id { get; set; }
        [Required]
        [StringLength(30)]
        public string? RoleName { get; set; }
        [Required]
        [StringLength(100)]
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsRemoved { get; set; }
    }
}