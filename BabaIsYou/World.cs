using static FunctionalBabaIsYou.Sight;

namespace FunctionalBabaIsYou.Tests;

public record World
{
    readonly IEnumerable<PlacedBlock> all;
    public bool IsOver => !You().Any() || Won;
    public bool Won => You().Any(IsAtAny(Wins));
    IEnumerable<PlacedBlock> Wins => all.Where(IsWin);
    public World(IEnumerable<PlacedBlock> all) => this.all = all;

    public World Move(Direction towards) => new
    (
        all.Except(You())
            .Concat(MoveYou(towards))
            .Except(DefeatedAt(towards))
            .Except(PushableByYou(towards))
            .Concat(PushableByYou(towards).Select(block => block.Moving(towards).Commit()))
    );

    IEnumerable<PlacedBlock> PushableByYou(Coordinate movingTo) => You().SelectMany(InFront(movingTo));
    Func<PlacedBlock, IEnumerable<PlacedBlock>> InFront(Coordinate towards) => block => PushableAhead(block.Moving(towards)).SelectMany(OtherThanYou);
    IEnumerable<PlacedBlock> OtherThanYou(Coordinate position) => all.At(position).Where(IsNotYou);
    bool IsPushable(PlacedBlock what) => all.AllDefinitionsOf(what).Contains(PhraseBuilder.Push);
    IEnumerable<PlacedBlock> MoveYou(Coordinate towards) => You().Select(who => TryMove(who.Moving(towards)));
    PlacedBlock TryMove(Movement move) => CanMove(move) ? move.Commit() : move.Who;
    bool CanMove(Movement move) => !BlocksAt(AfterLastPushable(move)).Any(IsStop);
    static bool IsStop(PlacedBlock who) => who.Means(PhraseBuilder.Stop);
    Coordinate AfterLastPushable(Movement move) => PushableAhead(move).LastOr(move.Who.whereIs) + move.Direction;
    IEnumerable<Coordinate> PushableAhead(Movement move) => all.Where(IsAhead(move)).Where(IsPushable).Map(Position);
    Coordinate Position(PlacedBlock block) => block.whereIs;
    IEnumerable<PlacedBlock> You() => all.Where(IsYou);
    bool IsNotYou(PlacedBlock actor) => !IsYou(actor);
    bool IsYou(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.You);
    bool IsWin(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.Win);
    bool IsDefeat(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.Defeat);
    IEnumerable<PlacedBlock> DefeatedAt(Coordinate to) => MoveYou(to).Where(IsAtDefeat);
    bool IsAtDefeat(PlacedBlock you) => BlocksAt(you).Any(IsDefeat);
    IEnumerable<PlacedBlock> BlocksAt(PlacedBlock what) => BlocksAt(what.whereIs);
    public IEnumerable<PlacedBlock> BlocksAt(Coordinate position) => all.DefinitionOf(all.At(position));
}