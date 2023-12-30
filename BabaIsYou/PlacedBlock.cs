namespace FunctionalBabaIsYou;

public readonly struct PlacedBlock
{
    const string SubjectSuffix = "Subject";

    public readonly Coordinate whereIs;
    public string whatDepicts => allThatDepicts.First();
    public readonly IEnumerable<string> allThatDepicts;
    public int Y => whereIs.Y;
    public int X => whereIs.X;

    PlacedBlock(Coordinate whereIs, string whatDepicts)
    {
        this.whereIs = whereIs;
        this.allThatDepicts = new[] { whatDepicts };
    }
    
    PlacedBlock(Coordinate whereIs, IEnumerable<string> allThatDepicts)
    {
        this.whereIs = whereIs;
        this.allThatDepicts = allThatDepicts;
    }

    public bool Means(string something) => whatDepicts.Contains(something);
    public bool Means(PlacedBlock other) => whatDepicts.Contains(other.whatDepicts);
    public Movement Moving(Direction towards) => new(this, towards);
    public PlacedBlock AsSubject() => (whereIs, whatDepicts + SubjectSuffix);

    public static implicit operator (Coordinate whereIs, string whatDepicts)(PlacedBlock placedBlock) =>
        (placedBlock.whereIs, placedBlock.whatDepicts);

    public static implicit operator PlacedBlock((Coordinate whereIs, string whatDepicts) block) =>
        new(block.whereIs, block.whatDepicts);
    
    public static implicit operator PlacedBlock((Coordinate whereIs, IEnumerable<string> allThatDepicts) block) =>
        new(block.whereIs, block.allThatDepicts);

    public override bool Equals(object? other)
        => other is PlacedBlock block && whereIs.Equals(block.whereIs) &&
           allThatDepicts.SequenceEqual(block.allThatDepicts);
}