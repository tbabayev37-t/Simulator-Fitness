using Simulator16TB.Models.Common;

namespace Simulator16TB.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Trainer> Trainers { get; set; } = [];
    }
}
