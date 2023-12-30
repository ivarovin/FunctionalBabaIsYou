namespace FunctionalBabaIsYou.Tests;

public static class FunctionalCollections
{
    public static T LastOr<T>(this IEnumerable<T> elements, T alternative) 
        => elements.Any() ? elements.Last() : alternative;
}