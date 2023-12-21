namespace BabaIsYou.Tests;

public record World
{
    readonly IEnumerable<((int x, int y), string what)> blocks;
    readonly IEnumerable<((int x, int y), string what)> actors;

    World(IEnumerable<((int x, int y), string what)> actors, IEnumerable<((int x, int y), string what)> blocks)
    {
        this.blocks = blocks;
        this.actors = actors;
    }

    public static World CreateWith(((int x, int y), string what)[] actors)
    {
        return new(actors, Array.Empty<((int x, int y), string what)>());
    }

    public static World CreateWith(((int x, int y), string what)[] actors,
        IEnumerable<((int x, int y), string what)> blocks)
    {
        return new(actors, blocks);
    }

    public (int x, int y) WhereIs(string baba)
    {
        if (!actors.Any(IsYou))
            return (0, 0);
        
        return actors.Where(IsYou).First().Item1;
    }

    public World MoveTowards((int, int) direction)
    {
        var you = actors.Where(IsYou);
        var otherThanYou = actors.Except(you);
        var newActors = otherThanYou.Concat(you.Select(Move(direction)));

        return new World(newActors, blocks);
    }

    Func<((int x, int y) whereIs, string what), ((int x, int y) whereIs, string what)> Move((int x, int y) direction)
        => block => ((block.whereIs.x + direction.x, block.whereIs.y + direction.y), block.what);

    bool IsYou(((int x, int y), string whatIs) actor) => blocks.DefinitionOf(actor.whatIs).Equals(PhraseBuilder.You);
}