using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;
using static FunctionalBabaIsYou.Tests.WorldBuilder;

namespace FunctionalBabaIsYou.Tests;

public class WorldTests
{
    [Test]
    public void SpawnBaba_InWorld()
    {
        World.CreateWith(new[] { Baba.AtOrigin() })
            .WhereIs(Baba)
            .Should().Be(Origin);
    }

    [Test]
    public void Move_IsNotPossible_IfYou_AreNotDefined()
    {
        IntroduceToWorld(Baba.AtOrigin()).Build()
            .MoveTowards((1, 0))
            .WhereIs(Baba)
            .Should().Be(Origin);
    }

    [Test]
    public void Move_TowardsDirection_IfYou_AreDefined()
    {
        IntroduceToWorld(Baba.AtOrigin()).AndBlocks(BabaIsYou.MoveToRight()).Build()
            .MoveTowards((0, 1))
            .WhereIs(Baba)
            .Should().NotBe(Origin);
    }

    [Test]
    public void OnlyYou_Moves()
    {
        IntroduceToWorld(Baba.AtOrigin(), Rock.AtOrigin()).AndBlocks(BabaIsYou.MoveToRight()).Build()
            .MoveTowards((0, 1))
            .WhereIs(Rock)
            .Should().Be(Origin);
    }
}