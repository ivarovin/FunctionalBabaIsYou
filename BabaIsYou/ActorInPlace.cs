namespace FunctionalBabaIsYou.Tests;

public readonly struct ActorInPlace
{
    public readonly (int x, int y) whereIs;
    public readonly string whatDepicts;

    public ActorInPlace((int x, int y) whereIs, string whatDepicts)
    {
        this.whereIs = whereIs;
        this.whatDepicts = whatDepicts;
    }
    
    public static implicit operator ((int x, int y), string) (ActorInPlace actor) => (actor.whereIs, actor.whatDepicts);
    public static implicit operator ActorInPlace (((int x, int y), string) actor) => new(actor.Item1, actor.Item2);
}