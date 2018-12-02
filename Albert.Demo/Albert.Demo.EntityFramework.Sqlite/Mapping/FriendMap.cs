using Albert.Demo.Domain.Friends;
using Albert.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Demo.EntityFramework.Sqlite
{
    public class FriendMap : AlbertEntityTypeConfiguration<Friend>
    {
        public override void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("Friends");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.NickName).HasMaxLength(20);
            builder.Property(f => f.Description).HasMaxLength(500);
        }
    }
}
