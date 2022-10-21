﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Elephant.Database
{
    /// <summary>
    /// Queryable extensions.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Tracks or doesn't track the entity depending on <paramref name="isTracked"/>. True equals
        /// <see cref="QueryTrackingBehavior.TrackAll"/> and false equals <see cref="QueryTrackingBehavior.NoTracking"/>.
        /// </summary>
        public static IQueryable<T> AsTracking<T>(this IQueryable<T> source, bool isTracked)
            where T : class
        {
            return source.AsTracking(isTracked ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking);
        }

        /// <summary>
        /// Order by column name, either ascending or descending.
        /// </summary>
        public static IQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnName, ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            return OrderByColumn(source, columnName, sortDirection == ListSortDirection.Ascending);
        }

        /// <summary>
        /// Order by column name, either ascending or descending.
        /// </summary>
        public static IQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnName, bool isAscending = true)
        {
            if (string.IsNullOrEmpty(columnName))
                return source;

            ParameterExpression parameter = Expression.Parameter(source.ElementType, string.Empty);

            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { source.ElementType, property.Type },
                source.Expression,
                Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}