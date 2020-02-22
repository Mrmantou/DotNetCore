using Albert.Demo.Domain.Friends;
using Albert.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Albert.Demo.EntityFramework.Sqlite.Mapping
{
    class FriendMap : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("Friends");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.NickName).HasMaxLength(20);
            builder.Property(f => f.Description).HasMaxLength(500);
        }
    }
}
