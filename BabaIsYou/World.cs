namespace FunctionalBabaIsYou.Tests;

public record World
{
    readonly IEnumerable<((int x, int y), string what)> blocks;
    readonly IEnumerable<((int x, int y), string what)> actors;

    World(IEnumerable<((int x, int y), string what)> actors, IEnumerable<((int x, int y), string what)> blocks)
    {
        if (actors.Any(x => blocks.Any(y => x.Item1 == y.Item1)))
            throw new ArgumentException("Actors and blocks cannot be at the same place");

        this.blocks = blocks;
        this.actors = actors;
    }

    public static World CreateWith(IEnumerable<((int x, int y), string what)> actors,
        IEnumerable<((int x, int y), string what)> blocks)
    {
        return new(actors, blocks);
    }

    public (int x, int y) WhereIs(string actor) => actors.First(x => x.what == actor).Item1;

    public World MoveTowards((int x, int y) direction)
    {
        if (Math.Abs(direction.x) > 1 || Math.Abs(direction.y) > 1)
            throw new ArgumentException("Direction must be an unit vector");

        var newActors = You().Select(Move(direction));
        var blocksToMove = newActors.SelectMany(x => blocks.Where(y => y.Item1 == x.Item1));
        var newBlocks = blocks.Except(blocksToMove).Concat(blocksToMove.Select(Move(direction)));

        return new World(actors.Except(You()).Concat(newActors), newBlocks);
    }

    IEnumerable<((int x, int y), string what)> You() => actors.Where(IsYou);

    Func<((int x, int y) whereIs, string what), ((int x, int y) whereIs, string what)> Move((int x, int y) direction)
        => block => ((block.whereIs.x + direction.x, block.whereIs.y + direction.y), block.what);

    bool IsYou(((int x, int y), string whatIs) actor) => blocks.DefinitionOf(actor.whatIs).Equals(PhraseBuilder.You);

    public IEnumerable<((int, int), string)> ElementsAt((int, int) position)
        => blocks.Where(x => x.Item1 == position).Concat(actors.Where(x => x.Item1 == position));
}