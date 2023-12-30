using FluentAssertions;
using static FunctionalBabaIsYou.Sight;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Gameplay;

public class SightTests
{
    [Test]
    public void Identify_BlockAhead()
    {
        IsAhead(Baba.AtOrigin(), Direction.Right)
            (Rock.AtMiddle())
            .Should().BeTrue();
    }
}