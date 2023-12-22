namespace FunctionalBabaIsYou.Tests;

public record World
{
    readonly IEnumerable<PlacedBlock> blocks;
    readonly IEnumerable<PlacedBlock> actors;

    World(IEnumerable<PlacedBlock> actors, IEnumerable<PlacedBlock> blocks)
    {
        if (actors.Any(x => blocks.Any(y => y.whereIs == x.whereIs)))
            throw new ArgumentException("Actors and blocks cannot be at the same place");

        this.blocks = blocks;
        this.actors = actors;
    }

    public static World CreateWith(IEnumerable<PlacedBlock> actors,
        IEnumerable<PlacedBlock> blocks)
    {
        return new(actors, blocks);
    }

    public Coordinate WhereIs(string actor) => actors.First(x => x.whatDepicts == actor).whereIs;

    public World MoveTowards(Coordinate direction)
    {
        if (Math.Abs(direction.x) > 1 || Math.Abs(direction.y) > 1)
            throw new ArgumentException("Direction must be an unit vector");

        var newActors = You().Select(Move(direction));
        var blocksToMove = newActors.SelectMany(OverlappedBlocks);
        var newBlocks = blocks.Except(blocksToMove).Concat(blocksToMove.Select(Move(direction)));

        return new World(actors.Except(You()).Concat(newActors), newBlocks);
    }

    IEnumerable<PlacedBlock> OverlappedBlocks(PlacedBlock actor)
        => blocks.Where(block => block.whereIs == actor.whereIs);

    IEnumerable<PlacedBlock> You() => actors.Where(IsYou);

    Func<PlacedBlock, PlacedBlock> Move(Coordinate direction)
        => block => ((block.whereIs.x + direction.x, block.whereIs.y + direction.y), block.whatDepicts);

    bool IsYou(PlacedBlock actor) => blocks.DefinitionOf(actor.whatDepicts).Equals(PhraseBuilder.You);
    public IEnumerable<PlacedBlock> ElementsAt(Coordinate position) => blocks.At(position).Concat(actors.At(position));
}