using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreeApplication.DAL.Entities;

namespace TreeApplication.DAL.EntityConfigurations
{
    internal class NodeConfiguration : IEntityTypeConfiguration<Node>
    {
        public void Configure(EntityTypeBuilder<Node> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(n => n.Parent)
                   .WithMany(n => n.Children)
                   .HasForeignKey(n => n.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}