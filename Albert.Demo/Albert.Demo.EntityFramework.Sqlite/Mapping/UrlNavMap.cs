using Albert.Demo.Domain.UrlNavs;
using Albert.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Demo.EntityFramework.Sqlite.Mapping
{
    class UrlNavMap : AlbertEntityTypeConfiguration<UrlNav>
    {
        public override void Configure(EntityTypeBuilder<UrlNav> builder)
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
