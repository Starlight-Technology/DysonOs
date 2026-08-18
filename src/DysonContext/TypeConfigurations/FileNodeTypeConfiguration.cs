using DysonContext.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace DysonContext.TypeConfiguration;

public class FileNodeTypeConfiguration : IEntityTypeConfiguration<FileNodeEntity>
{
    public void Configure(EntityTypeBuilder<FileNodeEntity> builder)
    {
        builder.HasKey(fn => fn.Id);

        builder.Property(fn => fn.Id)
               .ValueGeneratedOnAdd();

        builder.HasMany(fn => fn.Children)
               .WithOne(fn => fn.Parent)
               .HasForeignKey(fn => fn.ParentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fn=>fn.Owner)
               .WithMany(u => u.FileNodes)
               .HasForeignKey(fn => fn.OwnerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}