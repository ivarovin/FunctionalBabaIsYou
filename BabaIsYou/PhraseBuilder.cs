namespace BabaIsYou.Tests;

public static class PhraseBuilder
{
    public static string You => "You";
    public static string ToBe => "is";
    public static string Baba => "Baba";
    public static ((int x, int y), string what) At(this string what, int x, int y) => ((x, y), what);
    public static ((int x, int y), string what) AtOrigin(this string what) => what.At(0, 0);
    public static ((int x, int y), string what) AtMiddle(this string what) => what.At(1, 0);
    public static ((int x, int y), string what) AtRight(this string what) => what.At(2, 0);
}