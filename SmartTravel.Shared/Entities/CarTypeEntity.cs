using System.ComponentModel.DataAnnotations;

namespace SmartTravel.Shared.Entities
{
    public class CarTypeEntity : BaseEntity
    {
        [Key]
        public int CarTypeId { get; set; }
        [Required]
        public required string CarTypeName { get; set; }
    }
}
