using Snippet.Micro.Common.Extensions;
using System.Linq.Expressions;

namespace Snippet.Micro.Common.Extensions
{
    public static class QueryableExtension
    {
        /// <summary>
        /// 拼接条件(直接拼接）
        /// </summary>
        public static IQueryable<T> AndIf<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate) where T : class
            => queryable.Where(predicate);

        /// <summary>
        /// 拼接条件(直接拼接）
        /// </summary>
        public static IQueryable<T> AndIf<T>(this IQueryable<T> queryable, bool condition, Expression<Func<T, bool>> predicate) where T : class
            => condition ? queryable.Where(predicate) : queryable;

        /// <summary>
        /// 拼接条件（字符串不为空）
        /// </summary>
        public static IQueryable<T> AndIfExist<T>(this IQueryable<T> queryable, string value,
            Expression<Func<T, bool>> predicate) where T : class
            => string.IsNullOrEmpty(value) ? queryable : queryable.Where(predicate);

        /// <summary>
        /// 拼接条件（对象不为空）
        /// </summary>
        public static IQueryable<T> AndIfExist<T, TData>(this IQueryable<T> queryable, TData value,
            Expression<Func<T, bool>> predicate) where T : class
            => value is null ? queryable : queryable.Where(predicate);

        /// <summary>
        /// 拼接多个排序条件
        /// </summary>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, params (Expression<Func<T, object>>, SortType)[] keySelectors) where T : class
        {
            foreach (var keySelector in keySelectors)
            {
                queryable = queryable is not IOrderedQueryable<T> ?
                    keySelector.Item2 == SortType.Ascending ?
                        queryable.OrderBy(keySelector.Item1) :
                        queryable.OrderByDescending(keySelector.Item1) :
                    keySelector.Item2 == SortType.Descending ?
                        (queryable as IOrderedQueryable<T>).ThenBy(keySelector.Item1) :
                        (queryable as IOrderedQueryable<T>).ThenByDescending(keySelector.Item1);
            }
            return queryable;
        }

        /// <summary>
        /// 拼接多个排序条件
        /// </summary>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, params (string, SortType)[] keySelectors) where T : class
        {
            foreach (var keySelector in keySelectors)
            {
                // 反射生成表达式
                var propertyInfo = typeof(T).GetProperty(keySelector.Item1);
                var param = Expression.Parameter(typeof(T), "s");
                var propertyExpression = Expression.Property(param, propertyInfo);
                var orderExpression = Expression.Lambda<Func<T, object>>(propertyExpression, param);

                queryable = queryable is not IOrderedQueryable<T> ?
                    keySelector.Item2 == SortType.Ascending ?
                        queryable.OrderBy(orderExpression) :
                        queryable.OrderByDescending(orderExpression) :
                    keySelector.Item2 == SortType.Descending ?
                        (queryable as IOrderedQueryable<T>).ThenBy(orderExpression) :
                        (queryable as IOrderedQueryable<T>).ThenByDescending(orderExpression);
            }
            return queryable;
        }

        /// <summary>
        /// 取得分页
        /// </summary>
        public static IQueryable<T> QueryPage<T>(this IQueryable<T> queryable, int page, int size)
        {
            return queryable.Skip((page - 1) * size).Take(size);
        }
    }

    public enum SortType
    {
        Ascending,
        Descending
    }
}