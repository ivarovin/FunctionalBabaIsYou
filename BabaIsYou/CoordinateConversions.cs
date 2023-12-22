using FunctionalBabaIsYou.Tests;

namespace FunctionalBabaIsYou;

public static class CoordinateConversions
{
    public static IEnumerable<((int x, int y) whereIs, string block)> ToTuples(
        this IEnumerable<(Coordinate whereIs, string block)> blocks)
        => blocks.Select(x => ((x.whereIs.x, x.whereIs.y), x.block));

    public static IEnumerable<(Coordinate whereIs, string block)> ToCoordinates(
        this IEnumerable<((int x, int y) whereIs, string block)> blocks)
        => blocks.Select(x => (new Coordinate(x.whereIs.x, x.whereIs.y), x.block));
}