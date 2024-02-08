using HalaqahModel.Context;
using HalaqahModel.Models;

namespace HalaqahModel.Helpers;

public class EntityHelper(HalaqahContext context)
{
    /// <summary>
    ///     Specifies related navigations to ensure they are loaded in the entity.
    /// </summary>
    /// <param name="entity">The entity to load navigations for.</param>
    /// <param name="navigationProperties">Navigation property names to be loaded.</param>
    /// <typeparam name="T">A type inheriting from <see cref="BaseEntity" />.</typeparam>
    /// <returns>The entity with the loaded navigations.</returns>
    public T AlsoInclude<T>(T entity, params string[] navigationProperties) where T : BaseEntity
    {
        var entry = context.Entry(entity);
        foreach (var navigationProperty in navigationProperties)
        {
            entry.Navigation(navigationProperty).Load();
        }

        return entity;
    }

    /// <inheritdoc cref="AlsoInclude{T}" />
    /// <summary>
    ///     Specifies related navigations to ensure they are loaded in the entity. Any other navigations will be set to null.
    /// </summary>
    public T OnlyInclude<T>(T entity, params string[] navigationProperties) where T : BaseEntity
    {
        var entry = context.Entry(entity);
        var toLoad = entry.Navigations.Where(n => navigationProperties.Contains(n.Metadata.Name)).ToList();
        var toNull = entry.Navigations.Where(n => !navigationProperties.Contains(n.Metadata.Name));

        foreach (var navigationEntry in toLoad)
        {
            navigationEntry.Load();
        }

        foreach (var navigationEntry in toNull)
        {
            navigationEntry.CurrentValue = null;
        }

        return entity;
    }
    
    /// <summary>
    ///     Ensure all related navigations are loaded in the entity.
    /// </summary>
    /// <param name="entity">The entity to load navigations for.</param>
    /// <typeparam name="T">A type inheriting from <see cref="BaseEntity" />.</typeparam>
    /// <returns>The entity with the loaded navigations.</returns>
    public T IncludeAll<T>(T entity) where T : BaseEntity
    {
        var entry = context.Entry(entity);
        foreach (var navigationProperty in entry.Navigations)
        {
            navigationProperty.Load();
        }

        return entity;
    }
    
    /// <summary>
    ///     Clear all related navigations in the entity and set them to null.
    /// </summary>
    /// <param name="entity">The entity to clear navigations for.</param>
    /// <typeparam name="T">A type inheriting from <see cref="BaseEntity" />.</typeparam>
    /// <returns>The entity with the cleared navigations.</returns>
    public T IncludeNone<T>(T entity) where T : BaseEntity
    {
        var entry = context.Entry(entity);
        foreach (var navigationProperty in entry.Navigations)
        {
            navigationProperty.CurrentValue = null;
        }

        return entity;
    }
}