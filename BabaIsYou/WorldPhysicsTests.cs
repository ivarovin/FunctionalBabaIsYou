using FluentAssertions;
using static FunctionalBabaIsYou.Tests.Direction;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;
using static FunctionalBabaIsYou.Tests.WorldBuilder;

namespace FunctionalBabaIsYou.Tests.Gameplay;

public class WorldPhysicsTests
{
    [Test]
    public void Push_PushableBlock()
    {
        IntroduceToWorld(Baba.AtOrigin(), Rock.AtMiddle())
            .AndBlocks(BabaIsYou.Down())
            .AndBlocks(RockIsPush.Up())
            .Build()
            .MoveTowards(Direction.Right)
            .ElementsAt(Middle).Should().HaveCount(1).And.Contain(Baba.AtMiddle());
    }

    [Test]
    public void GoThroughBlock_IfIsNotPushable()
    {
        IntroduceToWorld(Baba.AtOrigin(), Rock.AtMiddle())
            .AndBlocks(BabaIsYou.Up())
            .Build()
            .MoveTowards(Direction.Right)
            .ElementsAt(Middle).Should().HaveCount(2);
    }

    [Test]
    public void PushTwoBlocks()
    {
        var sut = IntroduceToWorld(Baba.AtOrigin(), Rock.AtMiddle(), Rock.AtRight())
            .AndBlocks(BabaIsYou.Up())
            .AndBlocks(RockIsPush.Down())
            .Build()
            .MoveTowards(Direction.Right);

        sut.ElementsAt((1, 0)).Should().HaveCount(1).And.Contain(Baba.At(1, 0));
        sut.ElementsAt((2, 0)).Should().HaveCount(1).And.Contain(Push.At(2, 0));
        sut.ElementsAt((3, 0)).Should().HaveCount(1).And.Contain(Push.At(3, 0));
    }

    [Test]
    public void Push_TwoBlocks_Upwards()
    {
        var sut = IntroduceToWorld(Baba.AtOrigin(), Rock.At(0, 1), Rock.At(0, 2))
            .AndBlocks(BabaIsYou.MoveToRight())
            .AndBlocks(RockIsPush.Down())
            .Build()
            .MoveTowards(Up);

        sut.ElementsAt((0, 1)).Should().HaveCount(1).And.Contain(Baba.At(0, 1));
        sut.ElementsAt((0, 2)).Should().HaveCount(1).And.Contain(Push.At(0, 2));
        sut.ElementsAt((0, 3)).Should().HaveCount(1).And.Contain(Push.At(0, 3));
    }

    [Test]
    public void CannotMove_TowardsStopBlock()
    {
        IntroduceToWorld(Baba.AtOrigin(), Rock.AtMiddle())
            .AndBlocks(BabaIsYou.Up())
            .AndBlocks(RockIsStop.Down())
            .Build()
            .MoveTowards(Direction.Right)
            .ElementsAt(Origin).Should().HaveCount(1);
    }
}