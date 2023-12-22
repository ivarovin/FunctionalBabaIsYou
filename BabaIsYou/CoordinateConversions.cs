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
    
    public static IEnumerable<(Coordinate, string)> Deconstruct(this IEnumerable<PlacedBlock> blocks)
        => blocks.Select(x => (x.whereIs, x.whatDepicts));

    public static IEnumerable<PlacedBlock> ToPlacedBlocks(
        this IEnumerable<((int, int) whereIs, string block)> blocks)
        => blocks.Select(x => new PlacedBlock(x.whereIs, x.block));
}