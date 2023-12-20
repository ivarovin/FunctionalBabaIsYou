using LanguageExt;

namespace BabaIsYou;

public class DefinitionSearch
{
    readonly IEnumerable<((int x, int y), string block)> blocks;
    readonly string subject;

    (int x, int y) WhereIsSubject => blocks.WhereIs(subject);
    Option<((int x, int y), string block)> LinkingVerb => blocks.Where(IsLinkingVerb).FirstOrNone(AtRightOfSubject);
    Option<(int x, int y)> WhereIsDefinition => LinkingVerb.Map(ToTheRight);
    public Option<((int x, int y), string block)> Definition => WhereIsDefinition.Bind(Block);

    public DefinitionSearch(IEnumerable<((int x, int y), string block)> blocks, string subject)
    {
        this.blocks = blocks;
        this.subject = subject;
    }

    bool AtRightOfSubject(((int x, int y), string block) block) => block.Item1.x == WhereIsSubject.x + 1 && block.Item1.y == WhereIsSubject.y;
    static bool IsLinkingVerb(((int x, int y), string block) x) => x.block == "is";
    Option<((int x, int y), string)> Block((int x, int y) at) => blocks.FirstOrNone(x => x.Item1 == at);
    static (int, int y) ToTheRight(((int x, int y), string block) x) => (x.Item1.x + 1, x.Item1.y);
}