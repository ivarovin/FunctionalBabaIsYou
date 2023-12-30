using FunctionalBabaIsYou.Tests;

namespace FunctionalBabaIsYou;

public readonly struct PlacedBlock
{
    const string SubjectSuffix = "Subject";
    
    public readonly Coordinate whereIs;
    public readonly string whatDepicts;
    public int Y => whereIs.Y;
    public int X => whereIs.X;

    PlacedBlock(Coordinate whereIs, string whatDepicts)
    {
        this.whereIs = whereIs;
        this.whatDepicts = whatDepicts;
    }
    
    public bool Means(string something) => whatDepicts == something;
    public Movement Moving(Direction towards) => new(this, towards);
    public PlacedBlock AsSubject() => (whereIs, whatDepicts + SubjectSuffix);

    public static implicit operator (Coordinate whereIs, string whatDepicts)(PlacedBlock placedBlock) =>
        (placedBlock.whereIs, placedBlock.whatDepicts);

    public static implicit operator PlacedBlock((Coordinate whereIs, string whatDepicts) block) =>
        new(block.whereIs, block.whatDepicts);
}