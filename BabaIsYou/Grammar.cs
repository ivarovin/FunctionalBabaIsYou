using FunctionalBabaIsYou.Tests;

namespace FunctionalBabaIsYou;

public static class Grammar
{
    public static string WhatIs(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string definition)
        => new SubjectSearch(blocks, definition)
            .Subject.Match
            (
                x => x.block,
                () => string.Empty
            );

    public static string DefinitionOf(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string subject)
        => new DefinitionSearch(blocks, subject)
            .Definition.Match
            (
                x => x.block,
                () => string.Empty
            );
    
    public static string DefinitionOf(this IEnumerable<(Coordinate whereIs, string block)> blocks, string subject)
        => new DefinitionSearch(blocks.ToTuples(), subject)
            .Definition.Match
            (
                x => x.block,
                () => string.Empty
            );
}