using LanguageExt;

namespace FunctionalBabaIsYou;

internal static class MonadicQuery
{
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        => enumerable.Any(predicate) ? enumerable.First(predicate) : Option<T>.None;
    
    public static T LastOr<T>(this IEnumerable<T> elements, T alternative) 
        => elements.Any() ? elements.Last() : alternative;

    public static IEnumerable<T> Replace<T>(this IEnumerable<T> original, IEnumerable<T> toReplace,
        IEnumerable<T> replacement)
        => original.Except(toReplace).Concat(replacement);
}