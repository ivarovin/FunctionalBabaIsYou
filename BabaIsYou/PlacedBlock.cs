using FunctionalBabaIsYou.Tests;

namespace FunctionalBabaIsYou;

public readonly struct PlacedBlock
{
    public readonly Coordinate whereIs;
    public readonly string whatDepicts;
    public int Y => whereIs.y;
    public int X => whereIs.x;

    public PlacedBlock(Coordinate whereIs, string whatDepicts)
    {
        this.whereIs = whereIs;
        this.whatDepicts = whatDepicts;
    }
    
    public bool Means(string something) => whatDepicts == something;
    public bool Means(PlacedBlock something) => whatDepicts == something.whatDepicts;

    public static implicit operator (Coordinate whereIs, string whatDepicts)(PlacedBlock placedBlock) =>
        (placedBlock.whereIs, placedBlock.whatDepicts);

    public static implicit operator PlacedBlock((Coordinate whereIs, string whatDepicts) block) =>
        new(block.whereIs, block.whatDepicts);

    public Movement Moving(Direction towards) => new(this, towards);
}