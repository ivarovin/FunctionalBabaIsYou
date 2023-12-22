using FunctionalBabaIsYou.Tests;

namespace FunctionalBabaIsYou;

public static class Grammar
{
    public static string WhatIs(this IEnumerable<PlacedBlock> blocks, string definition)
        => new SubjectSearch(blocks, definition)
            .Subject.Match
            (
                x => x.whatDepicts,
                () => string.Empty
            );

    public static string DefinitionOf(this IEnumerable<((int x, int y) whereIs, string block)> blocks, string subject)
        => new DefinitionSearch(blocks.ToPlacedBlocks(), subject)
            .Definition.Match
            (
                x => x.whatDepicts,
                () => string.Empty
            );
    
    public static string DefinitionOf(this IEnumerable<(Coordinate whereIs, string block)> blocks, string subject)
        => blocks.ToTuples().DefinitionOf(subject);
    
    public static string DefinitionOf(this IEnumerable<PlacedBlock> blocks, string subject)
        => new DefinitionSearch(blocks, subject)
            .Definition.Match
            (
                x => x.whatDepicts,
                () => string.Empty
            );
}