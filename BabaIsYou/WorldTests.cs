using FluentAssertions;
using static BabaIsYou.Tests.PhraseBuilder;

namespace BabaIsYou.Tests;

public class WorldTests
{
    [Test]
    public void SpawnBaba_InWorld()
    {
        World.CreateWith(new[] { Baba.AtOrigin() })
            .WhereIs(Baba)
            .Should().Be((0, 0));
    }

    [Test]
    public void Move_IsNotPossible_IfYou_AreNotDefined()
    {
        World.CreateWith(new[] { Baba.AtOrigin() })
            .MoveTowards((1, 0))
            .WhereIs(Baba)
            .Should().Be((0, 0));
    }

    [Test]
    public void Move_TowardsDirection_IfYou_AreDefined()
    {
        World.CreateWith(new[] { Baba.AtOrigin() }, PhraseBuilder.BabaIsYou.MoveToRight(1))
            .MoveTowards((0, 1))
            .WhereIs(Baba)
            .Should().NotBe((0, 0));
    }
}