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
        sut.ElementsAt((2, 0)).Should().HaveCount(1).And.Contain(Rock.At(2, 0));
        sut.ElementsAt((3, 0)).Should().HaveCount(1).And.Contain(Rock.At(3, 0));
    }
}