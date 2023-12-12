namespace BabaIsYou;

public static class Grammar
{
    public static string DefinitionOf(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string subject)
    {
        if (!blocks.Any(x => x.block == subject))
            throw new ArgumentException($"Subject {subject} is not defined");

        return blocks.ContainsDefinitionFor(subject)
            ? blocks.First(block =>
                block.whereIs.x > blocks.WhereIsLinkingVerbFor(subject).x &&
                block.AtSameHeightThan(blocks.WhereIs(subject))).block
            : string.Empty;
    }

    static bool ContainsDefinitionFor(this IEnumerable<((int x, int y), string block)> blocks, string what)
        => blocks.Any() && blocks.ExistsLinkingVerbFor(what) && blocks.ExistsDefinitionFor(what);

    static bool ExistsDefinitionFor(this IEnumerable<((int x, int y), string block)> blocks, string what)
        => blocks.Any(x => x.Item1.x > blocks.WhereIsLinkingVerbFor(what).x &&
                           x.AtSameHeightThan(blocks.WhereIsLinkingVerbFor(what)));

    static (int x, int y) WhereIsLinkingVerbFor(this IEnumerable<((int x, int y), string block)> blocks, string what)
        => blocks.First(x => x.block == "is" && x.Item1.x > blocks.WhereIs(what).x).Item1;

    static (int x, int y) WhereIs(this IEnumerable<((int x, int y), string what)> blocks, string what)
        => blocks.First(x => x.what == what).Item1;

    static bool ExistsLinkingVerbFor(this IEnumerable<((int x, int y), string what)> blocks, string subject)
        => blocks.Any(block => block.what == "is" && block.Item1.x > blocks.WhereIs(subject).x &&
                               block.AtSameHeightThan(blocks.WhereIs(subject)));

    static bool AtSameHeightThan(this ((int x, int y) whereIs, string what) block, (int x, int y) subject)
        => block.whereIs.y == subject.y;
}