using LanguageExt;
using static FunctionalBabaIsYou.Vocabulary;
using static LanguageExt.Option<FunctionalBabaIsYou.PlacedBlock>;

namespace FunctionalBabaIsYou;

internal class DefinitionSearch
{
    readonly IEnumerable<PlacedBlock> blocks;
    readonly PlacedBlock subject;

    public IEnumerable<PlacedBlock> AllDefinitions => Definition.SelectMany(JoinWithNextDefinition);
    public IEnumerable<Option<PlacedBlock>> Definition => WhereIsDefinition.Map(Block);
    IEnumerable<Coordinate> WhereIsDefinition => LinkingVerb.Map(ToTheRight).Append(LinkingVerb.Map(Down));
    Option<PlacedBlock> LinkingVerb => blocks.Where(IsLinkingVerb).FirstOrNone(LinkedToSubject);

    public DefinitionSearch(IEnumerable<PlacedBlock> blocks, PlacedBlock subject)
    {
        this.blocks = blocks;
        this.subject = subject;
    }

    static bool IsLinkingVerb(PlacedBlock block) => block.Means(Vocabulary.LinkingVerb);
    bool LinkedToSubject(PlacedBlock what) => Subject.Any(AtLeft(what)) || Subject.Any(Over(what));
    Func<PlacedBlock, bool> AtLeft(PlacedBlock ofWhat) => block => block.X == ofWhat.X - 1 && block.Y == ofWhat.Y;
    Func<PlacedBlock, bool> Over(PlacedBlock ofWhat) => block => block.X == ofWhat.X && block.Y == ofWhat.Y + 1;
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
    static Coordinate Down(PlacedBlock block) => (block.X, block.Y - 1);
    Option<PlacedBlock> Block(Coordinate at) => blocks.FirstOrNone(x => x.whereIs == at);
}