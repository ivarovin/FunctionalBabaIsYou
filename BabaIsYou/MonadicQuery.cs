using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace FunctionalBabaIsYou
{
    internal static class MonadicQuery
    {
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
            => enumerable.Any(predicate) ? enumerable.First(predicate) : Option<T>.None;

        public static Option<T> FirstSome<T>(this IEnumerable<Option<T>> enumerable)
            => enumerable.FirstOrDefault(x => x.IsSome);    
    
        public static T LastOr<T>(this IEnumerable<T> elements, T alternative) 
            => elements.Any() ? elements.Last() : alternative;

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> original, IEnumerable<T> toReplace,
            IEnumerable<T> replacement)
            => original.Except(toReplace).Concat(replacement);
    }
}