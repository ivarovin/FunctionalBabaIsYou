using FunctionalBabaIsYou.Tests;
using LanguageExt;
using static LanguageExt.Option<FunctionalBabaIsYou.PlacedBlock>;

namespace FunctionalBabaIsYou;

public class DefinitionSearch
{
    readonly IEnumerable<PlacedBlock> blocks;
    readonly PlacedBlock subject;
    public Option<PlacedBlock> Definition => WhereIsDefinition.Bind(Block);
    Option<Coordinate> WhereIsDefinition => LinkingVerb.Map(ToTheRight);
    Option<PlacedBlock> LinkingVerb => blocks.Where(IsLinkingVerb).FirstOrNone(AtRightOfSubject);

    public DefinitionSearch(IEnumerable<PlacedBlock> blocks, PlacedBlock subject)
    {
        this.blocks = blocks;
        this.subject = subject;
    }

    static bool IsLinkingVerb(PlacedBlock x) => x.whatDepicts == "is";
    static bool IsConjunction(PlacedBlock x) => x.whatDepicts == "and";
    bool AtRightOfSubject(PlacedBlock block) => Subject.Any(AtLeftOf(block));
    static Func<PlacedBlock, bool> AtLeftOf(PlacedBlock what) => block => block.X == what.X - 1 && block.Y == what.Y;
    IEnumerable<PlacedBlock> Subject => blocks.Where(IsSubject);
    bool IsSubject(PlacedBlock who) => who.Means(subject) && !who.Equals(subject);
    public IEnumerable<PlacedBlock> AllDefinitions() => DefinitionsAfter(Definition).Append(Definition);

    IEnumerable<PlacedBlock> DefinitionsAfter(Option<PlacedBlock> from)
        => from.Map(NextDefinition).Match
        (
            Some: definition => DefinitionsAfter(definition).Append(definition),
            None: () => None
        );

    Option<PlacedBlock> NextDefinition(PlacedBlock from) => ConjunctionAfter(from).Map(BlockAfter).IfNone(None);
    Option<PlacedBlock> ConjunctionAfter(PlacedBlock block) => BlockAfter(block).Where(IsConjunction);
    static Coordinate ToTheRight(PlacedBlock x) => (x.X + 1, x.Y);
    Option<PlacedBlock> Block(Coordinate at) => blocks.FirstOrNone(x => x.whereIs == at);
    Option<PlacedBlock> BlockAfter(PlacedBlock from) => Block(ToTheRight(from));
}