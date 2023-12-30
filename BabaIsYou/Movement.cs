namespace FunctionalBabaIsYou;

internal readonly struct Movement
{
    public readonly PlacedBlock Who;
    public readonly Coordinate Direction;

    public Movement(PlacedBlock who, Coordinate direction)
    {
        this.Who = who;
        this.Direction = direction;
    }

    public PlacedBlock Commit() => (Who.whereIs + Direction, Who.allThatDepicts);
}