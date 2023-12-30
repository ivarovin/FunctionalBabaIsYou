using static FunctionalBabaIsYou.Sight;
using static FunctionalBabaIsYou.Vocabulary;

namespace FunctionalBabaIsYou;

public record World
{
    readonly IEnumerable<PlacedBlock> all;
    public bool IsOver => !AllOfYou().Any() || Won;
    public bool Won => AllOfYou().Any(IsAtAny(Wins));
    IEnumerable<PlacedBlock> Wins => all.Where(IsWin);
    public World(IEnumerable<PlacedBlock> all) => this.all = all;
    bool IsWin(PlacedBlock actor) => all.DefinitionOf(actor).Means(Win);

    public World Move(Direction towards) => new
    (
        all.Replace(AllOfYou(), MovedYou(towards))
            .Except(DefeatedAt(towards))
            .Replace(ToBePushed(towards), Push(towards))
    );

    IEnumerable<PlacedBlock> Push(Direction towards) => ToBePushed(towards).Select(Step(towards));
    Func<PlacedBlock, PlacedBlock> Step(Direction towards) => block => block.Moving(towards).Commit();
    IEnumerable<PlacedBlock> AllOfYou() => all.Where(IsYou);
    bool IsYou(PlacedBlock who) => all.DefinitionOf(who).Means(You);
    IEnumerable<PlacedBlock> MovedYou(Coordinate towards) => AllOfYou().Select(who => TryMove(who.Moving(towards)));
    PlacedBlock TryMove(Movement move) => CanMove(move) ? move.Commit() : move.Who;
    bool CanMove(Movement move) => !IsStop(AfterLastPushable(move));
    bool IsStop(Coordinate at) => BlocksAt(at).Any(block => block.Means(Stop));
    public IEnumerable<PlacedBlock> BlocksAt(Coordinate where) => all.At(where).Select(all.DefinitionOf);
    Coordinate AfterLastPushable(Movement move) => PushableAhead(move).LastOr(move.Who.WhereIs) + move.Direction;
    IEnumerable<Coordinate> PushableAhead(Movement move) => all.Where(IsAhead(move)).Where(IsPushable).Map(Position);
    Coordinate Position(PlacedBlock block) => block.WhereIs;
    bool IsPushable(PlacedBlock what) => all.AllDefinitionsOf(what).Contains(Vocabulary.Push);
    IEnumerable<PlacedBlock> DefeatedAt(Coordinate to) => MovedYou(to).Where(IsDefeat);
    bool IsDefeat(PlacedBlock you) => BlocksAt(you.WhereIs).Any(block => block.Means(Defeat));
    IEnumerable<PlacedBlock> ToBePushed(Coordinate movingTo) => AllOfYou().SelectMany(PushableInFront(movingTo));

    Func<PlacedBlock, IEnumerable<PlacedBlock>> PushableInFront(Coordinate towards)
        => block => PushableAhead(block.Moving(towards)).SelectMany(OtherThanYou);

    IEnumerable<PlacedBlock> OtherThanYou(Coordinate at) => all.At(at).Where(IsNotYou);
    bool IsNotYou(PlacedBlock actor) => !IsYou(actor);
}