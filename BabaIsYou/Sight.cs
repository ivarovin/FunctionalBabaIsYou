using FunctionalBabaIsYou.Tests;

namespace FunctionalBabaIsYou;

public static class Sight
{
    public static IEnumerable<(Coordinate, string)> At(this IEnumerable<(Coordinate whereIs, string block)> blocks,
        Coordinate position) => blocks.Where(IsAt(position));

    static Func<(Coordinate, string), bool> IsAt(Coordinate position) => x => x.Item1 == position;
}