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

    public static string DefinitionOf(this IEnumerable<PlacedBlock> blocks, string subject)
        => new DefinitionSearch(blocks, subject)
            .Definition.Match
            (
                x => x.whatDepicts,
                () => string.Empty
            );
}