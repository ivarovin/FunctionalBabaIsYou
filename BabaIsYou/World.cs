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
        all.Except(AllOfYou())
            .Concat(MoveYou(towards))
            .Except(DefeatedAt(towards))
            .Except(PushableByYou(towards))
            .Concat(PushableByYou(towards).Select(block => block.Moving(towards).Commit()))
    );

    IEnumerable<PlacedBlock> AllOfYou() => all.Where(IsYou);
    bool IsYou(PlacedBlock who) => all.DefinitionOf(who).Means(You);
    IEnumerable<PlacedBlock> MoveYou(Coordinate towards) => AllOfYou().Select(who => TryMove(who.Moving(towards)));
    PlacedBlock TryMove(Movement move) => CanMove(move) ? move.Commit() : move.Who;
    bool CanMove(Movement move) => !IsStop(AfterLastPushable(move));
    bool IsStop(Coordinate at) => BlocksAt(at).Any(block => block.Means(Stop));
    public IEnumerable<PlacedBlock> BlocksAt(Coordinate where) => all.DefinitionOf(all.At(where));
    Coordinate AfterLastPushable(Movement move) => PushableAhead(move).LastOr(move.Who.whereIs) + move.Direction;
    IEnumerable<Coordinate> PushableAhead(Movement move) => all.Where(IsAhead(move)).Where(IsPushable).Map(Position);
    Coordinate Position(PlacedBlock block) => block.whereIs;
    bool IsPushable(PlacedBlock what) => all.AllDefinitionsOf(what).Contains(Push);
    IEnumerable<PlacedBlock> DefeatedAt(Coordinate to) => MoveYou(to).Where(IsDefeat);
    bool IsDefeat(PlacedBlock you) => BlocksAt(you.whereIs).Any(block => block.Means(Defeat));
    IEnumerable<PlacedBlock> PushableByYou(Coordinate movingTo) => AllOfYou().SelectMany(PushableInFront(movingTo));

    Func<PlacedBlock, IEnumerable<PlacedBlock>> PushableInFront(Coordinate towards)
        => block => PushableAhead(block.Moving(towards)).SelectMany(OtherThanYou);

    IEnumerable<PlacedBlock> OtherThanYou(Coordinate at) => all.At(at).Where(IsNotYou);
    bool IsNotYou(PlacedBlock actor) => !IsYou(actor);
}