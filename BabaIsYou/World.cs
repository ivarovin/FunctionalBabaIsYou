namespace FunctionalBabaIsYou.Tests;

public record World
{
    readonly IEnumerable<(Coordinate, string what)> blocks;
    readonly IEnumerable<(Coordinate, string what)> actors;

    World(IEnumerable<(Coordinate, string what)> actors, IEnumerable<(Coordinate, string what)> blocks)
    {
        if (actors.Any(x => blocks.Any(y => x.Item1 == y.Item1)))
            throw new ArgumentException("Actors and blocks cannot be at the same place");

        this.blocks = blocks;
        this.actors = actors;
    }

    public static World CreateWith(IEnumerable<(Coordinate, string what)> actors,
        IEnumerable<(Coordinate, string what)> blocks)
    {
        return new(actors, blocks);
    }

    public Coordinate WhereIs(string actor) => actors.First(x => x.what == actor).Item1;

    public World MoveTowards(Coordinate direction)
    {
        if (Math.Abs(direction.x) > 1 || Math.Abs(direction.y) > 1)
            throw new ArgumentException("Direction must be an unit vector");

        var newActors = You().Select(Move(direction));
        var blocksToMove = newActors.SelectMany(OverlappedBlocks);
        var newBlocks = blocks.Except(blocksToMove).Concat(blocksToMove.Select(Move(direction)));

        return new World(actors.Except(You()).Concat(newActors), newBlocks);
    }

    IEnumerable<(Coordinate, string what)> OverlappedBlocks((Coordinate, string what) actor) 
        => blocks.Where(block => block.Item1 == actor.Item1);

    IEnumerable<(Coordinate, string what)> You() => actors.Where(IsYou);

    Func<(Coordinate whereIs, string what), (Coordinate whereIs, string what)> Move(Coordinate direction)
        => block => ((block.whereIs.x + direction.x, block.whereIs.y + direction.y), block.what);

    bool IsYou((Coordinate, string whatIs) actor) => blocks.DefinitionOf(actor.whatIs).Equals(PhraseBuilder.You);

    public IEnumerable<(Coordinate, string)> ElementsAt(Coordinate position)
        => blocks.Where(x => x.Item1 == position).Concat(actors.Where(x => x.Item1 == position));
}