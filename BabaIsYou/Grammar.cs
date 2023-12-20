namespace BabaIsYou;

public static class Grammar
{
    public static string WhatIs(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string definition)
    {
        if (!blocks.Any(x => x.block == definition))
            throw new ArgumentException($"Definition {definition} is not defined");

        return new SubjectSearch(blocks, definition).Subject.Match(x => x.block, () => string.Empty);
    }

    public static string DefinitionOf(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string subject)
    {
        if (!blocks.Any(x => x.block == subject))
            throw new ArgumentException($"Subject {subject} is not defined");

        return new DefinitionSearch(blocks, subject).Definition.Match(x => x.block, () => string.Empty);
    }

    public static (int x, int y) WhereIs(this IEnumerable<((int x, int y), string what)> blocks, string what)
        => blocks.First(x => x.what == what).Item1;
}