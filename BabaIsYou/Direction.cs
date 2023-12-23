namespace FunctionalBabaIsYou.Tests;

public readonly struct Direction
{
    readonly int x;
    readonly int y;

    Direction(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Direction Up => new(0, 1);
    public static Direction Down => new(0, -1);
    public static Direction Left => new(-1, 0);
    public static Direction Right => new(1, 0);

    public static implicit operator Coordinate(Direction direction) => (direction.x, direction.y);
    public static implicit operator Direction(Coordinate coordinate) => new(coordinate.x, coordinate.y);
}