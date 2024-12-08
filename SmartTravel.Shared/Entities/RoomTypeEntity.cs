using System.ComponentModel.DataAnnotations;

namespace SmartTravel.Shared.Entities
{
    public class RoomTypeEntity : BaseEntity
    {
        [Key]
        public int RoomTypeId { get; set; }
        [Required]
        public required string RoomTypeName { get; set; }
    }
}
