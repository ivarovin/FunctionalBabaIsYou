using FunctionalBabaIsYou.Tests;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using static FunctionalBabaIsYou.Tests.Direction;
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
    {
        if (Block(ToTheRight(block)).Map(IsConjunction) == false)
            return None;

        return Block(ToTheRight(block) + Right).Match
        (
            Some: x => x,
            None: () => None
        );
    }

    static Coordinate ToTheRight(PlacedBlock x) => (x.X + 1, x.Y);
    Option<PlacedBlock> Block(Coordinate at) => blocks.FirstOrNone(x => x.whereIs == at);

    public DefinitionSearch(IEnumerable<PlacedBlock> blocks, string subject)
    {
        this.blocks = blocks;
        this.subject = subject;
    }
}