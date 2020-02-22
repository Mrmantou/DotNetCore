using Albert.Demo.Domain.UrlNavs;
using Albert.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Albert.Demo.EntityFramework.Sqlite.Mapping
{
    class UrlNavMap : IEntityTypeConfiguration<UrlNav>
    {
        public void Configure(EntityTypeBuilder<UrlNav> builder)
        {
            builder.ToTable("UrlNavs");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Title).HasMaxLength(300);
            builder.Property(u => u.Classify).HasMaxLength(20);
            builder.Property(u => u.Url).HasMaxLength(100);
            builder.Property(u => u.Description).HasMaxLength(500);
        }
    }
}
