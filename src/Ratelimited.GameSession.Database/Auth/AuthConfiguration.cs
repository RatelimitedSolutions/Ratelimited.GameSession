using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Database
{
    public sealed class AuthConfiguration : IEntityTypeConfiguration<Auth>
    {
        public void Configure(EntityTypeBuilder<Auth> builder)
        {
            builder.ToTable("Auths", "Auth");

            builder.HasKey(auth => auth.Id);

            builder.HasIndex(auth => auth.Login).IsUnique();

            builder.Property(auth => auth.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(auth => auth.Login).HasMaxLength(100).IsRequired();
            builder.Property(auth => auth.Password).HasMaxLength(500).IsRequired();
            builder.Property(auth => auth.Salt).HasMaxLength(500).IsRequired();
            builder.Property(auth => auth.Roles).IsRequired();


        }
    }
}
