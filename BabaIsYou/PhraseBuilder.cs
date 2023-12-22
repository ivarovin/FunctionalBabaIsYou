namespace FunctionalBabaIsYou.Tests;

public static class PhraseBuilder
{
    public static string You => "You";
    public static string ToBe => "is";
    public static string Baba => "Baba";
    public static string Rock => "Rock";
    public static string Push => "Push";
    public static Coordinate Origin => (0, 0);

    public static IEnumerable<PlacedBlock> BabaIsYou
        => new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight() };
    
    public static IEnumerable<PlacedBlock> RockIsPush
        => new[] { Rock.AtOrigin(), ToBe.AtMiddle(), Push.AtRight() };

    public static PlacedBlock At(this string what, int x, int y) => ((x, y), what);
    public static PlacedBlock AtOrigin(this string what) => what.At(0, 0);
    public static PlacedBlock AtMiddle(this string what) => what.At(1, 0);
    public static PlacedBlock AtRight(this string what) => what.At(2, 0);

    public static IEnumerable<PlacedBlock> MoveToRight(
        this IEnumerable<PlacedBlock> blocks, int howManyTimes = 1)
        => blocks.Select(x => (PlacedBlock)((x.whereIs.x + howManyTimes, x.whereIs.y), x.whatDepicts));

    public static IEnumerable<PlacedBlock> MoveDown(
        this IEnumerable<PlacedBlock> blocks, int howManyTimes = 1)
        => blocks.Select(x => (PlacedBlock)((x.whereIs.x, x.whereIs.y - howManyTimes), x.whatDepicts));

    public static PlacedBlock MoveDown(this PlacedBlock block, int howManyTimes)
        => ((block.whereIs.x, block.whereIs.y - howManyTimes), block.whatDepicts);

    public static PlacedBlock MoveToRight(this PlacedBlock block, int howManyTimes)
        => ((block.whereIs.x + howManyTimes, block.whereIs.y), block.whatDepicts);

    public static PlacedBlock MoveToLeft(this PlacedBlock block, int howManyTimes)
        => ((block.whereIs.x - howManyTimes, block.whereIs.y), block.whatDepicts);
}