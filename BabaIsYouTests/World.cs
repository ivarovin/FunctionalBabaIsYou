using static FunctionalBabaIsYou.Sight;

namespace FunctionalBabaIsYou.Tests;

public record World
{
    readonly IEnumerable<PlacedBlock> all;
    public bool IsOver => !You().Any() || Won;
    public bool Won => You().Any(IsAtAny(Wins));
    IEnumerable<PlacedBlock> Wins => all.Where(IsWin);
    public World(IEnumerable<PlacedBlock> all) => this.all = all;
    bool IsWin(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.Win);

    public World Move(Direction towards) => new
    (
        all.Except(You())
            .Concat(MoveYou(towards))
            .Except(DefeatedAt(towards))
            .Except(PushableByYou(towards))
            .Concat(PushableByYou(towards).Select(block => block.Moving(towards).Commit()))
    );

    IEnumerable<PlacedBlock> You() => all.Where(IsYou);
    bool IsYou(PlacedBlock who) => all.DefinitionOf(who).Means(PhraseBuilder.You);
    IEnumerable<PlacedBlock> MoveYou(Coordinate towards) => You().Select(who => TryMove(who.Moving(towards)));
    PlacedBlock TryMove(Movement move) => CanMove(move) ? move.Commit() : move.Who;
    bool CanMove(Movement move) => !IsStop(AfterLastPushable(move));
    bool IsStop(Coordinate at) => BlocksAt(at).Any(block => block.Means(PhraseBuilder.Stop));
    public IEnumerable<PlacedBlock> BlocksAt(Coordinate where) => all.DefinitionOf(all.At(where));
    Coordinate AfterLastPushable(Movement move) => PushableAhead(move).LastOr(move.Who.whereIs) + move.Direction;
    IEnumerable<Coordinate> PushableAhead(Movement move) => all.Where(IsAhead(move)).Where(IsPushable).Map(Position);
    Coordinate Position(PlacedBlock block) => block.whereIs;
    bool IsPushable(PlacedBlock what) => all.AllDefinitionsOf(what).Contains(PhraseBuilder.Push);
    IEnumerable<PlacedBlock> DefeatedAt(Coordinate to) => MoveYou(to).Where(IsDefeat);
    bool IsDefeat(PlacedBlock you) => BlocksAt(you.whereIs).Any(block => block.Means(PhraseBuilder.Defeat));
    IEnumerable<PlacedBlock> PushableByYou(Coordinate movingTo) => You().SelectMany(InFront(movingTo));

    Func<PlacedBlock, IEnumerable<PlacedBlock>> InFront(Coordinate towards)
        => block => PushableAhead(block.Moving(towards)).SelectMany(OtherThanYou);

    IEnumerable<PlacedBlock> OtherThanYou(Coordinate at) => all.At(at).Where(IsNotYou);
    bool IsNotYou(PlacedBlock actor) => !IsYou(actor);
}