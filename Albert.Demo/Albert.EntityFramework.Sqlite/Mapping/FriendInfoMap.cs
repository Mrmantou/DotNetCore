using Albert.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.EntityFramework.Sqlite.Mapping
{
    public class FriendInfoMap : IEntityTypeConfiguration<FriendInfo>
    {
        public void Configure(EntityTypeBuilder<FriendInfo> builder)
        {
            builder.ToTable("");
        }
    }
}
