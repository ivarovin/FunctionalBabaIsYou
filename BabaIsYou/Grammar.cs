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
        
        var linkingVerbPosition = blocks.First(x => x.block == "is").Item1;
        var subjectPosition = blocks.First(x => x.block == what).Item1;
        if (subjectPosition.x > linkingVerbPosition.x)
            return string.Empty;

        return blocks.Last().block;
    }
}