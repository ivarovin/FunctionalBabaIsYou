namespace FunctionalBabaIsYou;

public static class Grammar
{
    public static string DefinitionOf(this IEnumerable<PlacedBlock> blocks, string subject)
        => new DefinitionSearch(blocks, ((2, 0), subject))
            .Definition.Match
            (
                x => x.whatDepicts,
                () => subject
            );

    public static PlacedBlock DefinitionOf(this IEnumerable<PlacedBlock> blocks, PlacedBlock subject)
        => new DefinitionSearch(blocks, subject)
            .Definition.Match
            (
                _ => (subject.whereIs, new DefinitionSearch(blocks, subject).AllDefinitions.Select(x => x.whatDepicts)),
                () => subject
            );

    public static IEnumerable<string> AllDefinitionsOf(this IEnumerable<PlacedBlock> blocks, PlacedBlock subject)
        => new DefinitionSearch(blocks, subject).AllDefinitions.Select(x => x.whatDepicts);
}