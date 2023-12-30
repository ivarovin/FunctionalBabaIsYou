namespace FunctionalBabaIsYou.Tests;

public static class PhraseBuilder
{
    public static string You => "You";
    public static string Win => "Win";
    public static string ToBe => "is";
    public static string And => "and";
    public static string Baba => "Baba";
    public static string BabaSubject => "BabaSubject";
    public static string Wall => "Wall";
    public static string WallSubject => "WallSubject";
    public static string Flag => "Flag";
    public static string FlagSubject => "FlagSubject";
    public static string Rock => "Rock";
    public static string RockSubject => "RockSubject";
    public static string Push => "Push";
    public static string Stop => "Stop";
    public static string Defeat => "Defeat";
    public static Coordinate Origin => (0, 0);
    public static Coordinate Middle => (1, 0);
    public static Coordinate Right => (2, 0);

    public static IEnumerable<PlacedBlock> BabaIsYou
        => new[] { BabaSubject.AtOrigin(), ToBe.AtMiddle(), You.AtRight() };

    public static IEnumerable<PlacedBlock> FlagIsWin
        => new[] { FlagSubject.AtOrigin(), ToBe.AtMiddle(), Win.AtRight() };

    public static IEnumerable<PlacedBlock> BabaIsRock
        => new[] { BabaSubject.AtOrigin(), ToBe.AtMiddle(), Rock.AtRight() };

    public static IEnumerable<PlacedBlock> RockIsPush
        => new[] { RockSubject.AtOrigin(), ToBe.AtMiddle(), Push.AtRight() };
    
    public static IEnumerable<PlacedBlock> WallIsStop
        => new[] { WallSubject.AtOrigin(), ToBe.AtMiddle(), Stop.AtRight() };

    public static IEnumerable<PlacedBlock> RockIsDefeat
        => new[] { RockSubject.AtOrigin(), ToBe.AtMiddle(), Defeat.AtRight() };

    public static PlacedBlock At(this string what, int x, int y) => ((x, y), what);
    public static PlacedBlock At(this string what, Coordinate where) => (where, what);
    public static PlacedBlock AtOrigin(this string what) => what.At(0, 0);
    public static PlacedBlock AtMiddle(this string what) => what.At(1, 0);
    public static PlacedBlock AtRight(this string what) => what.At(2, 0);
    public static PlacedBlock AtSomewhere(this string what) => what.At(434, 123123);

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
        => blocks.AppendConjunction().AppendDefinition(Rock);
    
    public static IEnumerable<PlacedBlock> AndPush(this IEnumerable<PlacedBlock> blocks)
        => blocks.AppendConjunction().AppendDefinition(Push);
    
    public static IEnumerable<PlacedBlock> AppendDefinition(this IEnumerable<PlacedBlock> blocks, string definition)
        => blocks.Append(definition.At(blocks.Last().whereIs + Direction.Right));
    public static IEnumerable<PlacedBlock> AppendConjunction(this IEnumerable<PlacedBlock> blocks)
        => blocks.Append(And.At(blocks.Last().whereIs + Direction.Right));
}