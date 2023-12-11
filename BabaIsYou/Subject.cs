namespace BabaIsYou;

public readonly struct Subject
{
    readonly string whatIs;
    Subject(string whatIs) => this.whatIs = whatIs;
    public static Subject Baba => new("Baba");
    public static implicit operator string(Subject subject) => subject.whatIs;
}