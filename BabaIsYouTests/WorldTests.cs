using FluentAssertions;
using static FunctionalBabaIsYou.Direction;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;
using static FunctionalBabaIsYou.Tests.WorldBuilder;

namespace FunctionalBabaIsYou.Tests.Gameplay
{
    public class WorldTests
    {
        [Test]
        public void SpawnBaba_InWorld()
        {
            IntroduceToWorld(Baba.AtOrigin()).Build()
                .BlocksAt(Origin)
                .Should().HaveCount(1);
        }

        [Test]
        public void Move_IsNotPossible_IfYou_AreNotDefined()
        {
            IntroduceToWorld(Baba.AtOrigin()).Build()
                .Move(Left)
                .BlocksAt(Origin)
                .Should().HaveCount(1);
        }

        [Test]
        public void Move_TowardsDirection_IfYou_AreDefined()
        {
            IntroduceToWorld(Baba.AtOrigin()).AndBlocks(BabaIsYou.MoveToRight()).Build()
                .Move(Up)
                .BlocksAt(Origin)
                .Should().HaveCount(0);
        }

        [Test]
        public void OnlyYou_Moves()
        {
            IntroduceToWorld(Baba.AtOrigin(), Rock.AtOrigin()).AndBlocks(BabaIsYou.MoveToRight()).Build()
                .Move(Up)
                .BlocksAt(Origin)
                .Should().Contain(Rock.AtOrigin())
                .And.NotContain(Baba.AtOrigin());
        }

        [Test]
        public void Actors_CanBe_Overlapped()
        {
            IntroduceToWorld(Baba.AtOrigin(), Rock.AtOrigin()).Build()
                .BlocksAt(Origin)
                .Should().HaveCount(2);
        }

        [Test]
        public void Block_CanBeSpotted_AtCoordinate()
        {
            EmptyWorldWithBlocks(Baba.AtOrigin()).Build()
                .BlocksAt(Origin)
                .Should().HaveCount(1);
        }

        [Test]
        public void Get_RedefinedElement()
        {
            IntroduceToWorld(Baba.AtOrigin())
                .AndBlocks(BabaIsRock.Down()).Build()
                .BlocksAt(Origin)
                .Should().Contain(Rock.AtOrigin());
        }

        [Test]
        public void Win_OnlyIfYou_AreInSamePlace_ThatWin()
        {
            IntroduceToWorld(Baba.AtOrigin(), Flag.AtOrigin())
                .AndBlocks(BabaIsYou.Down()).Build()
                .Won.Should().BeFalse();
        }

        [Test]
        public void Win_WhenAnyActor_ReachesWin()
        {
            IntroduceToWorld(Baba.AtOrigin(), Flag.AtOrigin())
                .AndBlocks(BabaIsYou.Down())
                .AndBlocks(FlagIsWin.Up()).Build()
                .Won.Should().BeTrue();
        }

        [Test]
        public void Win_IsNotPossible_IfDidNotReachFlag()
        {
            IntroduceToWorld(Baba.AtOrigin(), Flag.AtMiddle())
                .AndBlocks(BabaIsYou.Down())
                .AndBlocks(FlagIsWin.Up()).Build()
                .Won.Should().BeFalse();
        }

        [Test]
        public void WorldIsOver_WhenThereIsNoYou()
        {
            IntroduceToWorld(Baba.AtOrigin()).Build().IsOver.Should().BeTrue();
            IntroduceToWorld(Baba.AtOrigin()).AndBlocks(BabaIsYou.Down()).Build().IsOver.Should().BeFalse();
        }

        [Test]
        public void WorldIsOver_AfterWin()
        {
            IntroduceToWorld(Baba.AtOrigin(), Flag.AtOrigin())
                .AndBlocks(BabaIsYou.Down())
                .AndBlocks(FlagIsWin.Up()).Build()
                .IsOver.Should().BeTrue();
        }

        [Test]
        public void Die_WhenTouch_DefeatBlock()
        {
            IntroduceToWorld(Baba.AtOrigin(), Rock.AtOrigin())
                .AndBlocks(BabaIsYou.Down())
                .AndBlocks(RockIsDefeat.Up()).Build()
                .Move(None)
                .BlocksAt(Origin).Should().HaveCount(1);
        }

        [Test]
        public void Die_WhenTouch_DefeatBlock_AfterMoving()
        {
            IntroduceToWorld(Baba.AtOrigin(), Rock.AtMiddle())
                .AndBlocks(BabaIsYou.Down())
                .AndBlocks(RockIsDefeat.Up()).Build()
                .Move(Direction.Right)
                .BlocksAt(Middle).Should().HaveCount(1);
        }
    }
}