using LanguageExt;
using static FunctionalBabaIsYou.Sight;

namespace FunctionalBabaIsYou.Tests;

public record World
{
    readonly IEnumerable<PlacedBlock> all;
    public bool IsOver => !You().Any() || Won;
    public bool Won => You().Any(IsAtAny(Wins));
    IEnumerable<PlacedBlock> Wins => all.Where(IsWin);
    public World(IEnumerable<PlacedBlock> all) => this.all = all;

    public World MoveTowards(Direction direction) => new
    (
        all.Except(You())
            .Concat(MoveYou(direction))
            .Except(DefeatedAt(direction))
            .Except(PushableByYou(direction))
            .Concat(PushableByYou(direction).Select(Move(direction)))
    );

    IEnumerable<PlacedBlock> PushableByYou(Coordinate movingTo) => You().SelectMany(InFront(movingTo));

    Func<PlacedBlock, IEnumerable<PlacedBlock>> InFront(Coordinate movingTo)
        => block => PushableAhead(block, movingTo).SelectMany(OtherThanYou);

    IEnumerable<PlacedBlock> OtherThanYou(Coordinate where) => all.At(where).Where(IsNotYou);
    bool IsPushable(PlacedBlock what) => all.AllDefinitionsOf(what).Contains(PhraseBuilder.Push);
    IEnumerable<PlacedBlock> MoveYou(Coordinate towards) => You().Select(who => TryMove(who, towards));
    PlacedBlock TryMove(PlacedBlock who, Coordinate towards) => CanMove(who, towards) ? Move(towards)(who) : who;
    bool CanMove(PlacedBlock block, Coordinate towards) => !BlocksAt(LastElementAhead(block, towards)).Any(IsStop);
    static bool IsStop(PlacedBlock who) => who.Means(PhraseBuilder.Stop);

    Coordinate LastElementAhead(PlacedBlock from, Coordinate towards)
        => PushableAhead(from, towards).Any()
            ? PushableAhead(from, towards).Last() + towards
            : from.whereIs + towards;

    IEnumerable<Coordinate> PushableAhead(PlacedBlock from, Coordinate towards)
        => all.Where(IsAhead(from, towards)).Where(IsPushable).Map(Position);

    Coordinate Position(PlacedBlock block) => block.whereIs;

    Func<PlacedBlock, bool> IsAhead(PlacedBlock from, Direction towards)
        => to => (to.whereIs - (to.whereIs - from.whereIs) * towards).Equals(from.whereIs);

    IEnumerable<PlacedBlock> You() => all.Where(IsYou);
    Func<PlacedBlock, PlacedBlock> Move(Coordinate direction) => from => (from.whereIs + direction, from.whatDepicts);
    bool IsNotYou(PlacedBlock actor) => !IsYou(actor);
    bool IsYou(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.You);
    bool IsWin(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.Win);
    bool IsDefeat(PlacedBlock actor) => all.DefinitionOf(actor).Means(PhraseBuilder.Defeat);
    IEnumerable<PlacedBlock> DefeatedAt(Coordinate to) => MoveYou(to).Where(IsAtDefeat);
    bool IsAtDefeat(PlacedBlock you) => BlocksAt(you).Any(IsDefeat);
    IEnumerable<PlacedBlock> BlocksAt(PlacedBlock what) => BlocksAt(what.whereIs);
    public IEnumerable<PlacedBlock> BlocksAt(Coordinate position) => all.DefinitionOf(all.At(position));
}