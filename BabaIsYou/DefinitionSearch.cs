using LanguageExt;

namespace BabaIsYou;

public class DefinitionSearch
{
    readonly IEnumerable<((int x, int y), string block)> blocks;
    readonly string subject;

    public Option<((int x, int y), string block)> Definition => WhereIsDefinition.Bind(Block);
    Option<(int x, int y)> WhereIsDefinition => LinkingVerb.Map(ToTheRight);
    Option<((int x, int y), string block)> LinkingVerb => blocks.Where(IsLinkingVerb).FirstOrNone(AtRightOfSubject);
    static bool IsLinkingVerb(((int x, int y), string block) x) => x.block == "is";

    bool AtRightOfSubject(((int x, int y), string block) block) =>
        block.Item1.x == WhereIsSubject.x + 1 && block.Item1.y == WhereIsSubject.y;

    (int x, int y) WhereIsSubject => WhereIs(subject);
    (int x, int y) WhereIs(string what) => blocks.First(x => x.block == what).Item1;
    static (int, int y) ToTheRight(((int x, int y), string block) x) => (x.Item1.x + 1, x.Item1.y);
    Option<((int x, int y), string)> Block((int x, int y) at) => blocks.FirstOrNone(x => x.Item1 == at);

    public DefinitionSearch(IEnumerable<((int x, int y), string block)> blocks, string subject)
    {
        this.blocks = blocks;
        this.subject = subject;
    }
}