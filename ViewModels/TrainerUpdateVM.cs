using System.ComponentModel.DataAnnotations;
using Simulator16TB.Models.Common;

namespace Simulator16TB.ViewModels
{
    public class TrainerUpdateVM:BaseEntity
    {
        [Required, MaxLength(256), MinLength(3)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(3)]
        public string Surname { get; set; } = string.Empty;
        [Required, MaxLength(256)]
        public string Description { get; set; } = string.Empty;
        [Required, Range(2, int.MaxValue)]
        public int Experience { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
    }
}
