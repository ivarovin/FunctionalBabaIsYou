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

    bool AtLeftOfDefinition(((int x, int y), string block) block) =>
        Definition.Map(AtRightOf(block)).Match(result => result, None: () => false);

    static Func<((int x, int y), string), bool> AtRightOf(((int x, int y), string) what)
        => block => block.Item1.x == what.Item1.x + 1 && block.Item1.y == what.Item1.y;
    
    Option<((int x, int y), string)> Definition => blocks.FirstOrNone(x => x.block == definition);
    static (int, int y) ToTheLeft(((int x, int y), string block) x) => (x.Item1.x - 1, x.Item1.y);
    Option<((int x, int y), string)> Block((int x, int y) at) => blocks.FirstOrNone(x => x.Item1 == at);

    public SubjectSearch(IEnumerable<((int x, int y), string block)> blocks, string definition)
    {
        this.blocks = blocks;
        this.definition = definition;
    }
}