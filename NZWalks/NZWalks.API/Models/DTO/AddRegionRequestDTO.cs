using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="The code has to be a minimum of 3 characters.")]
        [MaxLength(6, ErrorMessage = "The code has to be a maximum of 6 characters.")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "The name has to be a maximum of 100 characters.")]
        public string Name { get; set; }
        public string? regionImageUrl { get; set; }
    }
}
