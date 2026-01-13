using Simulator16TB.Models.Common;

namespace Simulator16TB.ViewModels
{
    public class TranierGetVM:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Experience { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

    }
}
