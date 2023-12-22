namespace FunctionalBabaIsYou.Tests;

public class WorldBuilder
{
    IEnumerable<(Coordinate, string)> actors = Array.Empty<(Coordinate, string)>();
    IEnumerable<(Coordinate, string)> blocks = Array.Empty<(Coordinate, string)>();

    public World Build() => World.CreateWith(actors, blocks);

    public WorldBuilder AndBlocks(IEnumerable<((int, int), string)> blocks)
    {
        this.blocks = blocks.ToCoordinates();
        return this;
    }

    public static WorldBuilder IntroduceToWorld(params (Coordinate, string)[] actors) => new() { actors = actors };
    public static WorldBuilder EmptyWorldWithBlocks(params (Coordinate, string)[] blocks) => new() { blocks = blocks };
}