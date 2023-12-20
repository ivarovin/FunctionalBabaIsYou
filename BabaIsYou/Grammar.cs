using LanguageExt;

namespace BabaIsYou;

public static class Grammar
{
    public static string WhatIs(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string definition)
    {
        if (!blocks.Any(x => x.block == definition))
            throw new ArgumentException($"Definition {definition} is not defined");

        return blocks.ContainsSubjectFor(definition)
            ? blocks.First(x => x.whereIs.x == blocks.WhereIs(definition).x - 2).block
            : string.Empty;
    }

    public static string DefinitionOf(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string subject)
    {
        if (!blocks.Any(x => x.block == subject))
            throw new ArgumentException($"Subject {subject} is not defined");

        return new DefinitionSearch(blocks, subject).Definition.Match(x => x.block, () => string.Empty);
    }

    public static Option<T> FirstOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        => enumerable.Any(predicate) ? enumerable.First(predicate) : Option<T>.None;

    static bool ContainsSubjectFor(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string definition)
        => blocks.Any() && blocks.ExistsLinkingVerbForDefinition(definition) && blocks.ExistsSubjectFor(definition);

    static bool ExistsSubjectFor(this IEnumerable<((int x, int y), string)> blocks, string definition)
        => blocks.Any(block => block.IsToTheLeftOf(blocks.WhereIsLinkingVerbForDefinition(definition)));

    public static bool IsToTheRightOf(this ((int x, int y) whereIs, string block) block, (int x, int y) where)
        => block.whereIs.x == where.x + 1 && block.AtSameHeightThan(where);

    static bool IsToTheLeftOf(this ((int x, int y) whereIs, string block) block, (int x, int y) where)
        => block.whereIs.x == where.x - 1 && block.AtSameHeightThan(where);

    static (int x, int y) WhereIsLinkingVerbForDefinition(
        this IEnumerable<((int x, int y) whereIs, string block)> blocks,
        string what)
        => blocks.First(x => x.block == "is" && x.IsToTheLeftOf(blocks.WhereIs(what))).whereIs;

    public static (int x, int y) WhereIs(this IEnumerable<((int x, int y), string what)> blocks, string what)
        => blocks.First(x => x.what == what).Item1;

    static bool ExistsLinkingVerbForDefinition(this IEnumerable<((int x, int y), string what)> blocks,
        string definition)
        => blocks.Any(block => block.what == "is" && block.IsToTheLeftOf(blocks.WhereIs(definition)));

    static bool AtSameHeightThan(this ((int x, int y) whereIs, string what) block, (int x, int y) subject)
        => block.whereIs.y == subject.y;
}