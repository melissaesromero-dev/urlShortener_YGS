using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortener.Web.Entities;

namespace URLShortener.Web.Data.Configurations
{
    public class UrlEntryConfiguration : IEntityTypeConfiguration<UrlEntry>
    {
        public void Configure(EntityTypeBuilder<UrlEntry> builder)
        {
            builder.ToTable("Urls");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OriginalUrl)
                .HasMaxLength(3000)
                .IsRequired();

            builder.Property(x => x.ShortUrl)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.ClickCount)
                .HasDefaultValue(0)
                .IsRequired();

            //Index ShortUrl for faster redirects
            builder.HasIndex(x => x.ShortUrl)
                .IsUnique();

        }
    }
}
