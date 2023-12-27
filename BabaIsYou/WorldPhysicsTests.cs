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
}