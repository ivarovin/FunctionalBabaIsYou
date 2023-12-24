using static FunctionalBabaIsYou.Tests.Direction;

namespace FunctionalBabaIsYou.Tests;

public static class PhraseBuilder
{
    public static string You => "You"; //TODO: Convertir todo esto en placed blocks
    public static string Win => "Win";
    public static string ToBe => "is";
    public static string And => "and";
    public static string Baba => "Baba";
    public static string Flag => "Flag";
    public static string Rock => "Rock";
    public static string Push => "Push";
    public static Coordinate Origin => (0, 0);
    public static Coordinate Middle => (1, 0);
    public static Coordinate Right => (2, 0);

    public static IEnumerable<PlacedBlock> BabaIsYou
        => new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight() };

    public static IEnumerable<PlacedBlock> FlagIsWin
        => new[] { Flag.AtOrigin(), ToBe.AtMiddle(), Win.AtRight() };

    public static IEnumerable<PlacedBlock> BabaIsRock
        => new[] { Baba.AtOrigin(), ToBe.AtMiddle(), Rock.AtRight() };

    public static IEnumerable<PlacedBlock> RockIsPush
        => new[] { Rock.AtOrigin(), ToBe.AtMiddle(), Push.AtRight() };

    public static PlacedBlock At(this string what, int x, int y) => ((x, y), what);
    public static PlacedBlock At(this string what, Coordinate where) => (where, what);
    public static PlacedBlock AtOrigin(this string what) => what.At(0, 0);
    public static PlacedBlock AtMiddle(this string what) => what.At(1, 0);
    public static PlacedBlock AtRight(this string what) => what.At(2, 0);

    public static IEnumerable<PlacedBlock> MoveToRight(
        this IEnumerable<PlacedBlock> blocks, int howManyTimes = 1)
        => blocks.Select(x => (PlacedBlock)((x.whereIs.x + howManyTimes, x.whereIs.y), x.whatDepicts));

    public static IEnumerable<PlacedBlock> Down(
        this IEnumerable<PlacedBlock> blocks, int howManyTimes = 1)
        => blocks.Select(x => (PlacedBlock)((x.whereIs.x, x.whereIs.y - howManyTimes), x.whatDepicts));
    
    public static IEnumerable<PlacedBlock> Up(
        this IEnumerable<PlacedBlock> blocks, int howManyTimes = 1)
        => blocks.Select(x => (PlacedBlock)((x.whereIs.x, x.whereIs.y + howManyTimes), x.whatDepicts));

    public static PlacedBlock MoveDown(this PlacedBlock block, int howManyTimes = 1)
        => ((block.whereIs.x, block.whereIs.y - howManyTimes), block.whatDepicts);

    public static PlacedBlock MoveToRight(this PlacedBlock block, int howManyTimes)
        => ((block.whereIs.x + howManyTimes, block.whereIs.y), block.whatDepicts);

    public static PlacedBlock MoveToLeft(this PlacedBlock block, int howManyTimes)
        => ((block.whereIs.x - howManyTimes, block.whereIs.y), block.whatDepicts);
    
    public static IEnumerable<PlacedBlock> AndRock(this IEnumerable<PlacedBlock> blocks)
        => blocks.AppendConjunction().Append(Rock.At(blocks.Last().whereIs + Direction.Right * 2));
    
    public static IEnumerable<PlacedBlock> AppendConjunction(this IEnumerable<PlacedBlock> blocks)
        => blocks.Append(And.At(blocks.Last().whereIs + Direction.Right));
}