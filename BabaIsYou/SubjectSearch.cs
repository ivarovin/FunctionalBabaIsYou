using FunctionalBabaIsYou.Tests;
using LanguageExt;

namespace FunctionalBabaIsYou;

public class SubjectSearch
{
    readonly IEnumerable<PlacedBlock> blocks;
    readonly string definition;

    public Option<PlacedBlock> Subject => WhereIsSubject.Bind(Block);
    Option<Coordinate> WhereIsSubject => LinkingVerb.Map(ToTheLeft);
    Option<PlacedBlock> LinkingVerb => blocks.Where(IsLinkingVerb).FirstOrNone(AtLeftOfDefinition);
    static bool IsLinkingVerb(PlacedBlock x) => x.whatDepicts == "is";

    bool AtLeftOfDefinition(PlacedBlock block) =>
        Definition.Map(AtRightOf(block)).Match(result => result, None: () => false);

    static Func<PlacedBlock, bool> AtRightOf(PlacedBlock what)
        => block => block.X == what.X + 1 && block.Y == what.Y;
    
    Option<PlacedBlock> Definition => blocks.FirstOrNone(x => x.whatDepicts == definition);
    static Coordinate ToTheLeft(PlacedBlock x) => (x.X - 1, x.Y);
    Option<PlacedBlock> Block(Coordinate at) => blocks.FirstOrNone(x => x.whereIs == at);

    public SubjectSearch(IEnumerable<PlacedBlock> blocks, string definition)
    {
        this.blocks = blocks;
        this.definition = definition;
    }
}