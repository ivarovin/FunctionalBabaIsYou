namespace FunctionalBabaIsYou;

public readonly struct Coordinate
{
    public readonly int X;
    public readonly int Y;

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static implicit operator (int x, int y)(Coordinate coordinate) => (coordinate.X, coordinate.Y);
    public static implicit operator Coordinate((int x, int y) coordinate) => new(coordinate.x, coordinate.y);
    public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.X + b.X, a.Y + b.Y);
    public static Coordinate operator -(Coordinate a, Coordinate b) => new(a.X - b.X, a.Y - b.Y);
    public static Coordinate operator *(Coordinate a, int howMuch) => new(a.X * howMuch, a.Y * howMuch);
    public static Coordinate operator *(Coordinate a, Coordinate b) => new(a.X * b.X, a.Y * b.Y);
    public static bool operator ==(Coordinate a, Coordinate b) => a.Equals(b);
    public static bool operator !=(Coordinate a, Coordinate b) => !(a == b);
}