﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace BizFlow.Infrastructure.Extensions;

public static class ModelBuilderExtension
{
    public static IEnumerable<IMutableEntityType> EntityTypes(this ModelBuilder builder)
    {
        return builder.Model.GetEntityTypes();
    }

    public static IEnumerable<IMutableProperty> Properties(this ModelBuilder builder)
    {
        return builder.EntityTypes().SelectMany(entityType => entityType.GetProperties());
    }

    public static IEnumerable<IMutableProperty> Properties<T>(this ModelBuilder builder)
    {
        return builder.EntityTypes().SelectMany(entityType => entityType.GetProperties().Where(x => x.ClrType == typeof(T)));
    }

    public static void Configure(this IEnumerable<IMutableEntityType> entityTypes, Action<IMutableEntityType> convention)
    {
        foreach (var entityType in entityTypes)
        {
            convention(entityType);
        }
    }

    public static void Configure(this IEnumerable<IMutableProperty> propertyTypes, Action<IMutableProperty> convention)
    {
        foreach (var propertyType in propertyTypes)
        {
            convention(propertyType);
        }
    }


    public static void DateTimeConvention(this ModelBuilder modelBuilder)
    {
        modelBuilder.Model.GetEntityTypes()
           .SelectMany(t => t.GetProperties())
           .Where(p => p.ClrType == typeof(decimal)
                    || p.ClrType == typeof(decimal?))
           .ToList()
           .ForEach(p =>
           {
               p.SetColumnType("datetime2");
           });
    }

    public static void DecimalConvention(this ModelBuilder modelBuilder)
    {
        modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal)
                     || p.ClrType == typeof(decimal?))
            .ToList()
            .ForEach(p =>
            {
                if (p.GetPrecision() is null)
                    p.SetPrecision(18);
                if (p.GetScale() is null)
                    p.SetScale(4);
            });
    }

    public static void RelationConvetion(this ModelBuilder modelBuilder)
    {
        modelBuilder.EntityTypes()
           .SelectMany(e => e.GetForeignKeys())
           //.Where(e => !e.IsOwnership && e.DeleteBehavior == DeleteBehavior.Cascade)
           .ToList()
           .ForEach(relationship => relationship.DeleteBehavior = DeleteBehavior.Restrict);
    }

    public static void PluralzseTableNameConventions(this ModelBuilder modelBuilder, bool pluralize = true)
    {
        if (!pluralize)
            modelBuilder.EntityTypes().Configure(e => e.SetTableName(e.DisplayName()));
    }
}
