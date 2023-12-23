using static FunctionalBabaIsYou.Sight;

namespace FunctionalBabaIsYou.Tests;

public record World
{
    readonly IEnumerable<PlacedBlock> blocks;
    readonly IEnumerable<PlacedBlock> actors;

    public World(IEnumerable<PlacedBlock> actors, IEnumerable<PlacedBlock> blocks)
    {
        if (actors.Any(x => blocks.Any(y => y.whereIs == x.whereIs)))
            throw new ArgumentException("Actors and blocks cannot be at the same place");
        if (blocks.Select(x => x.whereIs).Distinct().Count() != blocks.Count())
            throw new ArgumentException("Blocks cannot be overlapped");

        this.blocks = blocks;
        this.actors = actors;
    }

    public bool Won => You().Any(IsAtAny(Wins()));
    public bool IsOver => !You().Any() || Won;

    IEnumerable<PlacedBlock> Wins() => actors.Where(IsWin);

    public World MoveTowards(Coordinate direction)
    {
        if (Math.Abs(direction.x) > 1 || Math.Abs(direction.y) > 1)
            throw new ArgumentException("Direction must be an unit vector");

        return new World(MoveActors(direction), MoveBlocks(direction));
    }

    IEnumerable<PlacedBlock> MoveBlocks(Coordinate direction)
        => blocks.Except(OverlappedBlocksAfter(direction))
            .Concat(OverlappedBlocksAfter(direction).Select(Move(direction)));

    IEnumerable<PlacedBlock> OverlappedBlocksAfter(Coordinate movingTo)
        => MovedYou(movingTo).SelectMany(OverlappedBlocks);

    IEnumerable<PlacedBlock> MoveActors(Coordinate to) => actors.Except(You()).Concat(MovedYou(to));
    IEnumerable<PlacedBlock> MovedYou(Coordinate towards) => You().Select(Move(towards));
    IEnumerable<PlacedBlock> OverlappedBlocks(PlacedBlock block) => blocks.Where(IsAt(block));
    IEnumerable<PlacedBlock> You() => actors.Where(IsYou);
    Func<PlacedBlock, PlacedBlock> Move(Coordinate direction) => from => (from.whereIs + direction, from.whatDepicts);
    bool IsYou(PlacedBlock actor) => blocks.DefinitionOf(actor).Means(PhraseBuilder.You);
    bool IsWin(PlacedBlock actor) => blocks.DefinitionOf(actor).Means(PhraseBuilder.Win);

    public IEnumerable<PlacedBlock> ElementsAt(Coordinate position)
        => blocks.WhichAreAt(position).Concat(blocks.DefinitionOf(actors.WhichAreAt(position)));
}