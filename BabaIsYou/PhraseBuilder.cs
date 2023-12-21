namespace BabaIsYou.Tests;

public static class PhraseBuilder
{
    public static string You => "You";
    public static string ToBe => "is";
    public static string Baba => "Baba";
    public static string Rock => "Rock";
    public static string Push => "Push";

    public static IEnumerable<((int x, int y), string what)> BabaIsYou
        => new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight() };

    public static ((int x, int y), string what) At(this string what, int x, int y) => ((x, y), what);
    public static ((int x, int y), string what) AtOrigin(this string what) => what.At(0, 0);
    public static ((int x, int y), string what) AtMiddle(this string what) => what.At(1, 0);
    public static ((int x, int y), string what) AtRight(this string what) => what.At(2, 0);

    public static IEnumerable<((int x, int y), string what)> MoveToRight(
        this IEnumerable<((int x, int y), string what)> blocks, int howManyTimes)
        => blocks.Select(x => ((x.Item1.x + howManyTimes, x.Item1.y), x.Item2));

    public static IEnumerable<((int x, int y), string what)> MoveDown(
        this IEnumerable<((int x, int y), string what)> blocks, int howManyTimes)
        => blocks.Select(x => ((x.Item1.x, x.Item1.y - howManyTimes), x.Item2));

    public static ((int x, int y), string what) MoveDown(this ((int x, int y), string what) block, int howManyTimes)
        => ((block.Item1.x, block.Item1.y - howManyTimes), block.Item2);

    public static ((int x, int y), string what) MoveToRight(this ((int x, int y), string what) block, int howManyTimes)
        => ((block.Item1.x + howManyTimes, block.Item1.y), block.Item2);

    public static ((int x, int y), string what) MoveToLeft(this ((int x, int y), string what) block, int howManyTimes)
        => ((block.Item1.x - howManyTimes, block.Item1.y), block.Item2);
}