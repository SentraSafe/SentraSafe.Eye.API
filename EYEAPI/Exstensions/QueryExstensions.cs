using System.Linq.Expressions;

namespace EYEAPI.Exstensions
{
    public static class QueryExstensions
    {
        /// <summary>
        /// Applies the expression if the source is not null
        /// </summary>
        public static IQueryable<TSource> WhereIfNotNull<TSource>(this IQueryable<TSource> queryable, object? source, Expression<Func<TSource, bool>> expression)
        {
            if (source == null || source is string stringSource && string.IsNullOrWhiteSpace(stringSource)) return queryable;

            return queryable.Where(expression);
        }

        /// <summary>
        /// Applies the expression if the source is not null
        /// </summary>
        public static IEnumerable<TSource> WhereIfNotNull<TSource>(this IEnumerable<TSource> enumerable, object? source, Func<TSource, bool> expression)
        {
            if (source == null || source is string stringSource && string.IsNullOrWhiteSpace(stringSource)) return enumerable;

            return enumerable.Where(expression);
        }
    }
}
