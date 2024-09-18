using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreeApplication.DAL.Entities;

namespace TreeApplication.DAL.EntityConfigurations
{
    internal class TreeConfiguration : IEntityTypeConfiguration<Tree>
    {
        public void Configure(EntityTypeBuilder<Tree> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Nodes)
                   .WithOne(n => n.Tree);
        }
    }
}