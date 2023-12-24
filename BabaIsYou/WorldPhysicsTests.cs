using FluentAssertions;
using static FunctionalBabaIsYou.Tests.Direction;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;
using static FunctionalBabaIsYou.Tests.WorldBuilder;

namespace FunctionalBabaIsYou.Tests.Gameplay;

public class WorldPhysicsTests
{
    [Test]
    public void PushBlock_AsYouMoves_TowardsIt()
    {
        IntroduceToWorld(Baba.AtOrigin()).AndBlocks(BabaIsYou.Down()).Build()
            .MoveTowards(Down)
            .ElementsAt((0, -2))
            .Should().HaveCount(1).And.Contain(Baba.At(0, -2));
    }
}