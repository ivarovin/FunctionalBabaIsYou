using FunctionalBabaIsYou.Tests;

namespace FunctionalBabaIsYou;

public static class Sight
{
    public static IEnumerable<PlacedBlock> At(this IEnumerable<PlacedBlock> blocks,
        Coordinate position) => blocks.Where(IsAt(position));

    public static Func<PlacedBlock, bool> IsAt(Coordinate position) => x => x.whereIs == position;

    public static Func<PlacedBlock, bool> IsAtAny(IEnumerable<PlacedBlock> positions)
        => block => positions.Any(IsAt(block));

    public static Func<PlacedBlock, bool> IsAt(PlacedBlock block) => IsAt(block.whereIs);

    public static Func<PlacedBlock, bool> IsAhead(PlacedBlock from, Direction towards)
        => to => (to.whereIs - DistanceBetween(from, to) * towards).Equals(from.whereIs);

    static Coordinate DistanceBetween(PlacedBlock from, PlacedBlock to)
        => (Math.Abs((to.whereIs - from.whereIs).x), Math.Abs((to.whereIs - from.whereIs).y));
}