using LanguageExt;

namespace FunctionalBabaIsYou;

public static class MonadicQuery
{
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        => enumerable.Any(predicate) ? enumerable.First(predicate) : Option<T>.None;
}