using System;
using System.Linq;

namespace Subscriptions.Domain.Utils
{
    public static class Util
    {
        public static Guid ToGuid(this Guid? source)
        {
            return source ?? Guid.Empty;
        }

        public static string EntityToString(this object Entity)
        {
            string[] stringArray = Entity.ToString().Split(".");
            return stringArray[stringArray.Length - 1].ToString();
        }

        public static IQueryable<TResult> TakeIfNotNull<TResult>(this IQueryable<TResult> source, int? count)
        {
            count = count == 0 ? null : count;
            return !count.HasValue ? source : source.Take(count.Value);
        }

        public static IQueryable<TResult> SkipIfNotNull<TResult>(this IQueryable<TResult> source, int? count)
        {
            count = count == 0 ? null : count;
            return !count.HasValue ? source : source.Skip(count.Value);
        }
    }

}
