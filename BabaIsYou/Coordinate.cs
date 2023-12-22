namespace FunctionalBabaIsYou.Tests;

public readonly struct Coordinate
{
    public readonly int x;
    public readonly int y;

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static implicit operator (int x, int y)(Coordinate coordinate) => (coordinate.x, coordinate.y);
    public static implicit operator Coordinate((int x, int y) coordinate) => new(coordinate.x, coordinate.y);
    public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.x + b.x, a.y + b.y);
    public static Coordinate operator -(Coordinate a, Coordinate b) => new(a.x - b.x, a.y - b.y);
    public static bool operator ==(Coordinate a, Coordinate b) => a.Equals(b);
    public static bool operator !=(Coordinate a, Coordinate b) => !(a == b);
}