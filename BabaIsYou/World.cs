using static FunctionalBabaIsYou.Sight;

namespace FunctionalBabaIsYou.Tests;

public record World
{
    readonly IEnumerable<PlacedBlock> all;
    public bool IsOver => !You().Any() || Won;
    public bool Won => You().Any(IsAtAny(Wins));
    IEnumerable<PlacedBlock> Wins => all.Where(IsWin);
    public World(IEnumerable<PlacedBlock> all) => this.all = all;
    public World MoveTowards(Direction direction) => new(MoveAll(direction));

    IEnumerable<PlacedBlock> MoveAll(Coordinate position)
        => all
            .Except(You())
            .Concat(YouAt(position))
            .Except(DefeatedAt(position))
            .Except(PushableByYou(position))
            .Concat(PushableByYou(position).Select(Move(position)));

    IEnumerable<PlacedBlock> PushableByYou(Coordinate movingTo)
    {
        foreach (var you in YouAt(movingTo))
        {
            var temporalPosition = you.whereIs;
            var placedBlocks = all.Where(IsAt(temporalPosition));
            while (placedBlocks.Any() && placedBlocks.All(IsPushable))
            {
                foreach (var result in placedBlocks.Except(You()))
                    yield return result;
                
                temporalPosition += movingTo;
                placedBlocks = all.Where(IsAt(temporalPosition));
            }
        }
    }

    bool IsPushable(PlacedBlock what) => all.AllDefinitionsOf(what).Contains(PhraseBuilder.Push);
    IEnumerable<PlacedBlock> YouAt(Coordinate towards) => You().Select(Move(towards));
    IEnumerable<PlacedBlock> OverlappedWith(PlacedBlock block) => all.Where(x => IsAt(block)(x));
    IEnumerable<PlacedBlock> You() => all.Where(IsYou);
    Func<PlacedBlock, PlacedBlock> Move(Coordinate direction) => from => (from.whereIs + direction, from.whatDepicts);
    bool IsYou(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.You);
    bool IsWin(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.Win);
    bool IsDefeat(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.Defeat);
    IEnumerable<PlacedBlock> DefeatedAt(Coordinate to) => YouAt(to).Where(IsAtDefeat);
    bool IsAtDefeat(PlacedBlock you) => ElementsAt(you).Any(IsDefeat);
    IEnumerable<PlacedBlock> ElementsAt(PlacedBlock what) => ElementsAt(what.whereIs);
    public IEnumerable<PlacedBlock> ElementsAt(Coordinate position) => all.DefinitionOf(all.At(position));
}