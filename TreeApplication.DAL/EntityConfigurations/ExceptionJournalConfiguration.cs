using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreeApplication.DAL.Entities;

namespace TreeApplication.DAL.EntityConfigurations
{
    internal class ExceptionJournalConfiguration : IEntityTypeConfiguration<ExceptionJournal>
    {
        public void Configure(EntityTypeBuilder<ExceptionJournal> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}