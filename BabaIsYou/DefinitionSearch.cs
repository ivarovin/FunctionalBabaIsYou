using LanguageExt;

namespace BabaIsYou;

public class DefinitionSearch
{
    readonly IEnumerable<((int x, int y), string block)> blocks;
    readonly string subject;

    (int x, int y) WhereIsSubject => blocks.WhereIs(subject);

    Option<((int x, int y), string block)> LinkingVerb
    {
        get
        {
            if (!blocks.Any(x => x.block == "is" && x.IsToTheRightOf(WhereIsSubject)))
                return Option<((int x, int y), string block)>.None;

            return blocks.FirstOrDefault(x => x.block == "is" && x.IsToTheRightOf(WhereIsSubject));
        }
    }

    Option<(int x, int y)> WhereIsDefinition => LinkingVerb.Map(ToTheRight);
    public Option<((int x, int y), string block)> Definition => WhereIsDefinition.Bind(Block);

    public DefinitionSearch(IEnumerable<((int x, int y), string block)> blocks, string subject)
    {
        this.blocks = blocks;
        this.subject = subject;
    }

    Option<((int x, int y), string)> Block((int x, int y) at)
    {
        if (blocks.All(x => x.Item1 != at)) return Option<((int x, int y), string)>.None;

        return blocks.FirstOrDefault(x => x.Item1 == at);
    }

    static (int, int y) ToTheRight(((int x, int y), string block) x) => (x.Item1.x + 1, x.Item1.y);
}