namespace BabaIsYou;

public static class Grammar
{
    public static string DefinitionOf(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string what)
    {
        if (!blocks.Any(x => x.block == what))
            throw new ArgumentException($"Subject {what} is not defined");

        return blocks.ContainsDefinitionFor(what)
            ? blocks.First(block => block.whereIs.x > blocks.WhereIsLinkingVerbFor(what).x).block
            : string.Empty;
    }

    static bool ContainsDefinitionFor(this IEnumerable<((int x, int y), string block)> blocks, string what)
        => blocks.Any() && blocks.ExistsLinkingVerbFor(what) && blocks.ExistsDefinitionFor(what);

    static bool ExistsDefinitionFor(this IEnumerable<((int x, int y), string block)> blocks, string what)
        => blocks.Any(x => x.Item1.x > blocks.WhereIsLinkingVerbFor(what).x);

    static (int x, int y) WhereIsLinkingVerbFor(this IEnumerable<((int x, int y), string block)> blocks, string what)
        => blocks.First(x => x.block == "is" && x.Item1.x > blocks.WhereIs(what).x).Item1;

    static (int x, int y) WhereIs(this IEnumerable<((int x, int y), string block)> blocks, string what)
        => blocks.First(x => x.block == what).Item1;

    static bool ExistsLinkingVerbFor(this IEnumerable<((int x, int y), string block)> blocks, string what)
        => blocks.Any(x => x.block == "is" && x.Item1.x > blocks.WhereIs(what).x);
}