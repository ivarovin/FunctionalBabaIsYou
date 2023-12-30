namespace FunctionalBabaIsYou;

internal static class Grammar
{
    public static string FirstDefinitionOf(this IEnumerable<PlacedBlock> blocks, string subject)
        => new DefinitionSearch(blocks, ((2, 0), subject))
            .Definition.FirstSome().Match
            (
                x => x.allThatDepicts.First(),
                () => subject
            );

    public static PlacedBlock DefinitionOf(this IEnumerable<PlacedBlock> blocks, PlacedBlock subject)
        => new DefinitionSearch(blocks, subject)
            .Definition.FirstSome().Match
            (
                _ => (subject.whereIs, new DefinitionSearch(blocks, subject).AllDefinitions.Select(x => x.whatDepicts)),
                () => subject
            );

    public static IEnumerable<string> AllDefinitionsOf(this IEnumerable<PlacedBlock> blocks, PlacedBlock subject)
        => new DefinitionSearch(blocks, subject).AllDefinitions.Select(x => x.whatDepicts);
}