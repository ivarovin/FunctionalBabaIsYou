namespace BabaIsYou;

public static class Grammar
{
    public static string WhatIs(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string definition)
    {
        if (!blocks.Any(x => x.block == definition))
            throw new ArgumentException($"Definition {definition} is not defined");

        return blocks.First(x => x.whereIs.x == blocks.WhereIs(definition).x - 2).block;
    }
    
    public static string DefinitionOf(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string subject)
    {
        if (!blocks.Any(x => x.block == subject))
            throw new ArgumentException($"Subject {subject} is not defined");

        return blocks.ContainsDefinitionFor(subject)
            ? blocks.First(block => block.IsToTheRightOf(blocks.WhereIsLinkingVerbFor(subject))).block
            : string.Empty;
    }

    static bool ContainsDefinitionFor(this IEnumerable<((int x, int y), string)> blocks, string what)
        => blocks.Any() && blocks.ExistsLinkingVerbFor(what) && blocks.ExistsDefinitionFor(what);

    static bool ExistsDefinitionFor(this IEnumerable<((int x, int y), string)> blocks, string subject)
        => blocks.Any(block => block.IsToTheRightOf(blocks.WhereIsLinkingVerbFor(subject)));

    static bool IsToTheRightOf(this ((int x, int y) whereIs, string block) block, (int x, int y) where)
        => block.whereIs.x == where.x + 1 && block.AtSameHeightThan(where);

    static (int x, int y) WhereIsLinkingVerbFor(this IEnumerable<((int x, int y) whereIs, string block)> blocks,
        string what)
        => blocks.First(x => x.block == "is" && x.IsToTheRightOf(blocks.WhereIs(what))).whereIs;

    static (int x, int y) WhereIs(this IEnumerable<((int x, int y), string what)> blocks, string what)
        => blocks.First(x => x.what == what).Item1;

    static bool ExistsLinkingVerbFor(this IEnumerable<((int x, int y), string what)> blocks, string subject)
        => blocks.Any(block => block.what == "is" && block.IsToTheRightOf(blocks.WhereIs(subject)));

    static bool AtSameHeightThan(this ((int x, int y) whereIs, string what) block, (int x, int y) subject)
        => block.whereIs.y == subject.y;
}