using LanguageExt;
using static FunctionalBabaIsYou.Vocabulary;
using static LanguageExt.Option<FunctionalBabaIsYou.PlacedBlock>;

namespace FunctionalBabaIsYou;

internal class DefinitionSearch
{
    readonly IEnumerable<PlacedBlock> blocks;
    readonly PlacedBlock subject;
    
    public IEnumerable<PlacedBlock> AllDefinitions => JoinWithNextDefinition(Definition);
    public Option<PlacedBlock> Definition => WhereIsDefinition.Bind(Block);
    Option<Coordinate> WhereIsDefinition => LinkingVerb.Map(ToTheRight);
    Option<PlacedBlock> LinkingVerb => blocks.Where(IsLinkingVerb).FirstOrNone(AtRightOfSubject);

    public DefinitionSearch(IEnumerable<PlacedBlock> blocks, PlacedBlock subject)
    {
        this.blocks = blocks;
        this.subject = subject;
    }

    static bool IsLinkingVerb(PlacedBlock block) => block.Means(Vocabulary.LinkingVerb);
    bool AtRightOfSubject(PlacedBlock what) => Subject.Any(subject => subject.X == what.X - 1 && subject.Y == what.Y);
    IEnumerable<PlacedBlock> Subject => blocks.Where(IsSubject);
    bool IsSubject(PlacedBlock who) => who.Means(subject) && !who.Equals(subject);

    IEnumerable<PlacedBlock> JoinWithNextDefinition(Option<PlacedBlock> definition)
        => DefinitionAfter(definition).Append(definition);

    IEnumerable<PlacedBlock> DefinitionAfter(Option<PlacedBlock> definition)
        => definition.Map(NextDefinition).Match(Some: JoinWithNextDefinition, None: () => None);

    Option<PlacedBlock> NextDefinition(PlacedBlock from) => ConjunctionAfter(from).Map(BlockAfter).IfNone(None);
    Option<PlacedBlock> ConjunctionAfter(PlacedBlock block) => BlockAfter(block).Where(IsConjunction);
    Option<PlacedBlock> BlockAfter(PlacedBlock from) => Block(ToTheRight(from));
    static bool IsConjunction(PlacedBlock block) => block.Means(Conjunction);
    static Coordinate ToTheRight(PlacedBlock block) => (block.X + 1, block.Y);
    Option<PlacedBlock> Block(Coordinate at) => blocks.FirstOrNone(x => x.whereIs == at);
}