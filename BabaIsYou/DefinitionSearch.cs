using FunctionalBabaIsYou.Tests;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using static LanguageExt.Option<FunctionalBabaIsYou.PlacedBlock>;

namespace FunctionalBabaIsYou;

public class DefinitionSearch
{
    readonly IEnumerable<PlacedBlock> blocks;
    readonly string subject;

    public Option<PlacedBlock> Definition => WhereIsDefinition.Bind(Block);
    Option<Coordinate> WhereIsDefinition => LinkingVerb.Map(ToTheRight);
    Option<PlacedBlock> LinkingVerb => blocks.Where(IsLinkingVerb).FirstOrNone(AtRightOfSubject);
    static bool IsLinkingVerb(PlacedBlock x) => x.whatDepicts == "is";
    static bool IsConjunction(PlacedBlock x) => x.whatDepicts == "and";
    bool AtRightOfSubject(PlacedBlock block) => Subject.Map(AtLeftOf(block)).Match(result => result, None: () => false);
    static Func<PlacedBlock, bool> AtLeftOf(PlacedBlock what) => block => block.X == what.X - 1 && block.Y == what.Y;
    Option<PlacedBlock> Subject => blocks.FirstOrNone(x => x.whatDepicts == subject);
    public IEnumerable<PlacedBlock> AllDefinitions => All();

    IEnumerable<PlacedBlock> All()
    {
        var definition = Definition;

        while (definition.IsSome)
        {
            yield return definition.Value();
            definition = NextDefinitionFrom(definition.Value());
        }
    }

    Option<PlacedBlock> NextDefinitionFrom(PlacedBlock block)
        => ConjunctionAfter(block).Map(BlockAfter)
            .Match
            (
                Some: definition => definition,
                None: () => None
            );

    Option<PlacedBlock> ConjunctionAfter(PlacedBlock block) => BlockAfter(block).Where(IsConjunction);
    static Coordinate ToTheRight(PlacedBlock x) => (x.X + 1, x.Y);
    Option<PlacedBlock> Block(Coordinate at) => blocks.FirstOrNone(x => x.whereIs == at);
    Option<PlacedBlock> BlockAfter(PlacedBlock from) => Block(ToTheRight(from));

    public DefinitionSearch(IEnumerable<PlacedBlock> blocks, string subject)
    {
        this.blocks = blocks;
        this.subject = subject;
    }
}