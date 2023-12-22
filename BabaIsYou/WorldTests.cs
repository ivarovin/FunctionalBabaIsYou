using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;
using static FunctionalBabaIsYou.Tests.WorldBuilder;

namespace FunctionalBabaIsYou.Tests;

public class WorldTests
{
    [Test]
    public void SpawnBaba_InWorld()
    {
        IntroduceToWorld(Baba.AtOrigin()).Build()
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

    [Test]
    public void PushBlock_AsYouMoves_TowardsIt()
    {
        IntroduceToWorld(Baba.AtOrigin()).AndBlocks(BabaIsYou.MoveDown()).Build()
            .MoveTowards((0, -1))
            .ElementsAt((0, -2))
            .Should().HaveCount(1).And.Contain(Baba.At(0, -2));
    }

    [Test]
    public void Actors_CanBe_Overlapped()
    {
        IntroduceToWorld(Baba.AtOrigin(), Rock.AtOrigin()).Build()
            .ElementsAt(Origin)
            .Should().HaveCount(2);
    }

    [Test]
    public void Block_CanBeSpotted_AtCoordinate()
    {
        EmptyWorldWithBlocks(Baba.AtOrigin()).Build()
            .ElementsAt(Origin)
            .Should().HaveCount(1);
    }
    
    [Test]
    public void Get_RedefinedElement()
    {
        IntroduceToWorld(Baba.AtOrigin())
            .AndBlocks(BabaIsRock.MoveDown()).Build()
            .ElementsAt(Origin)
            .Should().Contain(Rock.AtOrigin());
    }
}