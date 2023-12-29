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

    IEnumerable<PlacedBlock> MoveAll(Coordinate direction)
        => all
            .Except(You())
            .Concat(YouTowards(direction))
            .Except(DefeatedAt(direction))
            .Except(PushableByYou(direction))
            .Concat(PushableByYou(direction).Select(Move(direction)));

    IEnumerable<PlacedBlock> PushableByYou(Coordinate movingTo)
        => You().SelectMany(you => InFront(movingTo, you));

    IEnumerable<PlacedBlock> InFront(Coordinate movingTo, PlacedBlock you) 
        => PushableBlocksAhead(you, movingTo).SelectMany(OtherThanYouAt);

    bool ExistsPushableAt(Coordinate temporalPosition) =>
        OtherThanYouAt(temporalPosition).Any() && OtherThanYouAt(temporalPosition).All(IsPushable);

    IEnumerable<PlacedBlock> OtherThanYouAt(Coordinate temporalPosition) =>
        all.Where(IsAt(temporalPosition)).Where(IsNotYou);

    bool IsPushable(PlacedBlock what) => all.AllDefinitionsOf(what).Contains(PhraseBuilder.Push);
    IEnumerable<PlacedBlock> YouTowards(Coordinate towards) => You().Select(Move(towards));

    bool CanMove(PlacedBlock block, Coordinate towards)
        => !ElementsAt(LastElementAhead(block, towards)).Any(x => x.Means(PhraseBuilder.Stop));

    Coordinate LastElementAhead(PlacedBlock from, Coordinate towards)
        => PushableBlocksAhead(from, towards).Any()
            ? PushableBlocksAhead(from, towards).Last() + towards
            : from.whereIs + towards;

    IEnumerable<Coordinate> PushableBlocksAhead(PlacedBlock from, Coordinate towards)
    {
        var temporal = from.whereIs;
        var result = new List<Coordinate>();

        while (ExistsPushableAt(temporal + towards))
        {
            result.Add(temporal + towards);
            temporal += towards;
        }

        return result;
    }

    IEnumerable<PlacedBlock> You() => all.Where(IsYou);

    Func<PlacedBlock, PlacedBlock> Move(Coordinate direction)
        => from => CanMove(from, from.whereIs + direction) ? (from.whereIs + direction, from.whatDepicts) : from;

    bool IsNotYou(PlacedBlock actor) => !IsYou(actor);
    bool IsYou(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.You);
    bool IsWin(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.Win);
    bool IsDefeat(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.Defeat);
    IEnumerable<PlacedBlock> DefeatedAt(Coordinate to) => YouTowards(to).Where(IsAtDefeat);
    bool IsAtDefeat(PlacedBlock you) => ElementsAt(you).Any(IsDefeat);
    IEnumerable<PlacedBlock> ElementsAt(PlacedBlock what) => ElementsAt(what.whereIs);
    public IEnumerable<PlacedBlock> ElementsAt(Coordinate position) => all.DefinitionOf(all.At(position));
}