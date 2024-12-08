using System.ComponentModel.DataAnnotations;

namespace SmartTravel.Shared.Entities
{
    public class RoleEntity : BaseEntity
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public required string RoleName { get; set; }
    }
}
