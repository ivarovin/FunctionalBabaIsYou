namespace FunctionalBabaIsYou.Tests;

public class WorldBuilder
{
    IEnumerable<((int, int), string)> actors = Array.Empty<((int, int), string)>();
    IEnumerable<((int, int), string)> blocks = Array.Empty<((int, int), string)>();
    
    public World Build() => World.CreateWith(actors, blocks);
    
    public WorldBuilder AndBlocks(IEnumerable<((int, int), string)> blocks)
    {
        this.blocks = blocks;
        return this;
    }
    
    public static WorldBuilder IntroduceToWorld(params ((int, int), string)[] actors) => new() { actors = actors };
}