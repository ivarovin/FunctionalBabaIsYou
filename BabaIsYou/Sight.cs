using FunctionalBabaIsYou.Tests;

namespace FunctionalBabaIsYou;

public static class Sight
{
    public static IEnumerable<PlacedBlock> At(this IEnumerable<PlacedBlock> blocks,
        Coordinate position) => blocks.Where(IsAt(position));

    static Func<PlacedBlock, bool> IsAt(Coordinate position) => x => x.whereIs == position;
}