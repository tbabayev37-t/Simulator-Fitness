using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulator16TB.Models;

namespace Simulator16TB.Configurations
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(32);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(32);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
            builder.ToTable(opt =>
            {
                opt.HasCheckConstraint("CK_Trainers_Experience", "[Experience]>2");
            });
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(1024);
            builder.HasOne(x => x.Category).WithMany(x => x.Trainers).HasForeignKey(x => x.CategoryId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
