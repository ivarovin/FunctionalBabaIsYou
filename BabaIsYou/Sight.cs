namespace FunctionalBabaIsYou;

internal static class Sight
{
    public static IEnumerable<PlacedBlock> At(this IEnumerable<PlacedBlock> all, Coordinate where)
        => all.Where(IsAt(where));

    static Func<PlacedBlock, bool> IsAt(Coordinate position) => x => x.whereIs == position;
    public static Func<PlacedBlock, bool> IsAtAny(IEnumerable<PlacedBlock> where) => block => where.Any(IsAt(block));
    static Func<PlacedBlock, bool> IsAt(PlacedBlock block) => IsAt(block.whereIs);
    public static Func<PlacedBlock, bool> IsAhead(Movement move) => IsAhead(move.Who, move.Direction);

    public static Func<PlacedBlock, bool> IsAhead(PlacedBlock from, Direction towards)
        => to => (to.whereIs - DistanceBetween(from, to) * towards).Equals(from.whereIs);

    static Coordinate DistanceBetween(PlacedBlock from, PlacedBlock to)
        => (Math.Abs((to.whereIs - from.whereIs).X), Math.Abs((to.whereIs - from.whereIs).Y));
}