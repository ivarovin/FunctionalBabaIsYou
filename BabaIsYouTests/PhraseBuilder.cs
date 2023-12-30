namespace FunctionalBabaIsYou.Tests;

public static class PhraseBuilder
{
    public static string You => Vocabulary.You;
    public static string Win => Vocabulary.Win;
    public static string ToBe => Vocabulary.LinkingVerb;
    public static string And => Vocabulary.Conjunction;
    public static string Baba => "Baba";
    public static string BabaSubject => "BabaSubject";
    public static string Wall => "Wall";
    public static string Flag => "Flag";
    public static string Rock => "Rock";
    public static string RockSubject => "RockSubject";
    public static string Push => Vocabulary.Push;
    public static string Stop => Vocabulary.Stop;
    public static string Defeat => Vocabulary.Defeat;
    public static Coordinate Origin => (0, 0);
    public static Coordinate Middle => (1, 0);
    public static Coordinate Right => (2, 0);

    public static IEnumerable<PlacedBlock> BabaIsYou
        => new[] { Baba.AtOrigin().AsSubject(), ToBe.AtMiddle(), You.AtRight() };

    public static IEnumerable<PlacedBlock> FlagIsWin
        => new[] { Flag.AtOrigin().AsSubject(), ToBe.AtMiddle(), Win.AtRight() };

    public static IEnumerable<PlacedBlock> BabaIsRock
        => new[] { Baba.AtOrigin().AsSubject(), ToBe.AtMiddle(), Rock.AtRight() };

    public static IEnumerable<PlacedBlock> RockIsPush
        => new[] { Rock.AtOrigin().AsSubject(), ToBe.AtMiddle(), Push.AtRight() };
    
    public static IEnumerable<PlacedBlock> WallIsStop
        => new[] { Wall.AtOrigin().AsSubject(), ToBe.AtMiddle(), Stop.AtRight() };

    public static IEnumerable<PlacedBlock> RockIsDefeat
        => new[] { Rock.AtOrigin().AsSubject(), ToBe.AtMiddle(), Defeat.AtRight() };

    public static PlacedBlock At(this string what, int x, int y) => ((x, y), what);
    public static PlacedBlock At(this string what, Coordinate where) => (where, what);
    public static PlacedBlock AtOrigin(this string what) => what.At(0, 0);
    public static PlacedBlock AtMiddle(this string what) => what.At(1, 0);
    public static PlacedBlock AtRight(this string what) => what.At(2, 0);
    public static PlacedBlock AtSomewhere(this string what) => what.At(434, 123123);

    public static IEnumerable<PlacedBlock> MoveToRight(
        this IEnumerable<PlacedBlock> blocks, int howManyTimes = 1)
        => blocks.Select(x => (PlacedBlock)((x.whereIs.X + howManyTimes, x.whereIs.Y), x.whatDepicts));

    public static IEnumerable<PlacedBlock> Down(
        this IEnumerable<PlacedBlock> blocks, int howManyTimes = 1)
        => blocks.Select(x => (PlacedBlock)((x.whereIs.X, x.whereIs.Y - howManyTimes), x.whatDepicts));
    
    public static IEnumerable<PlacedBlock> Up(
        this IEnumerable<PlacedBlock> blocks, int howManyTimes = 1)
        => blocks.Select(x => (PlacedBlock)((x.whereIs.X, x.whereIs.Y + howManyTimes), x.whatDepicts));

    public static PlacedBlock MoveDown(this PlacedBlock block, int howManyTimes = 1)
        => ((block.whereIs.X, block.whereIs.Y - howManyTimes), block.whatDepicts);

    public static PlacedBlock MoveToRight(this PlacedBlock block, int howManyTimes)
        => ((block.whereIs.X + howManyTimes, block.whereIs.Y), block.whatDepicts);

    public static PlacedBlock MoveToLeft(this PlacedBlock block, int howManyTimes)
        => ((block.whereIs.X - howManyTimes, block.whereIs.Y), block.whatDepicts);
    
    public static IEnumerable<PlacedBlock> AndRock(this IEnumerable<PlacedBlock> blocks)
        => blocks.AppendConjunction().AppendDefinition(Rock);
    
    public static IEnumerable<PlacedBlock> AndPush(this IEnumerable<PlacedBlock> blocks)
        => blocks.AppendConjunction().AppendDefinition(Push);
    
    public static IEnumerable<PlacedBlock> AppendDefinition(this IEnumerable<PlacedBlock> blocks, string definition)
        => blocks.Append(definition.At(blocks.Last().whereIs + Direction.Right));
    public static IEnumerable<PlacedBlock> AppendConjunction(this IEnumerable<PlacedBlock> blocks)
        => blocks.Append(And.At(blocks.Last().whereIs + Direction.Right));
}