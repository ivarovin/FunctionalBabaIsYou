namespace FunctionalBabaIsYou.Tests;

public class WorldBuilder
{
    IEnumerable<PlacedBlock> actors = Array.Empty<PlacedBlock>();
    IEnumerable<PlacedBlock> blocks = Array.Empty<PlacedBlock>();

    public World Build() => new(actors, blocks);

    public WorldBuilder AndBlocks(IEnumerable<PlacedBlock> blocks)
    {
        this.blocks = this.blocks.Concat(blocks);
        return this;
    }
    
    public WorldBuilder AndBlocks(params PlacedBlock[] blocks)
    {
        this.blocks = blocks;
        return this;
    }

    public static WorldBuilder IntroduceToWorld(params PlacedBlock[] actors) => new() { actors = actors };
    public static WorldBuilder EmptyWorldWithBlocks(params PlacedBlock[] blocks) => new() { blocks = blocks };
}