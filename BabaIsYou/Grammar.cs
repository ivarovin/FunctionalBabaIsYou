namespace BabaIsYou;

public static class Grammar
{
    public static string DefinitionOf(this IEnumerable<((int x, int y), string block)> blocks, string what)
    {
        if (!blocks.Any(x => x.block == what))
            throw new ArgumentException($"Subject {what} is not defined");
        if (!blocks.Any())
            return string.Empty;
        if (!blocks.Any(x => x.block == "is"))
            return string.Empty;
        
        var subjectPosition = blocks.First(x => x.block == what).Item1;
        if (!blocks.Any(x => x.block == "is" && x.Item1.x > subjectPosition.x))
            return string.Empty;
        
        var linkingVerbPosition = blocks.First(x => x.block == "is" && x.Item1.x > subjectPosition.x).Item1;
        if (!blocks.Any(x => x.Item1.x > linkingVerbPosition.x))
            return string.Empty;
        var definition = blocks.First(x => x.Item1.x > linkingVerbPosition.x);
        return definition.block;
    }
}