using Albert.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.EntityFramework.Sqlite.Mapping
{
    public class FriendInfoMap : AlbertEntityTypeConfiguration<FriendInfo>
    {
        public override void Configure(EntityTypeBuilder<FriendInfo> builder)
        {
            builder.ToTable("FriendInfo");
            builder.HasKey(f => f.ID);
            builder.Property(f => f.NickName).HasMaxLength(20);
            builder.Property(f => f.Description).HasMaxLength(500);
        }
    }
}
