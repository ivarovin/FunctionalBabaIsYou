using LanguageExt;

namespace BabaIsYou;

public class SubjectSearch
{
    readonly IEnumerable<((int x, int y), string block)> blocks;
    readonly string definition;

    public Option<((int x, int y), string block)> Subject => WhereIsSubject.Bind(Block);
    Option<(int x, int y)> WhereIsSubject => LinkingVerb.Map(ToTheLeft);
    Option<((int x, int y), string block)> LinkingVerb => blocks.Where(IsLinkingVerb).FirstOrNone(AtLeftOfDefinition);
    static bool IsLinkingVerb(((int x, int y), string block) x) => x.block == "is";
    bool AtLeftOfDefinition(((int x, int y), string block) block) => block.Item1.x == WhereIsDefinition.x - 1 && block.Item1.y == WhereIsDefinition.y;
    (int x, int y) WhereIsDefinition => WhereIs(definition);
    (int x, int y) WhereIs(string what) => blocks.First(x => x.block == what).Item1;
    static (int, int y) ToTheLeft(((int x, int y), string block) x) => (x.Item1.x - 1, x.Item1.y);
    Option<((int x, int y), string)> Block((int x, int y) at) => blocks.FirstOrNone(x => x.Item1 == at);

    public SubjectSearch(IEnumerable<((int x, int y), string block)> blocks, string definition)
    {
        this.blocks = blocks;
        this.definition = definition;
    }
}