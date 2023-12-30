using System.Reflection.PortableExecutable;
using FluentAssertions;
using FluentAssertions.Execution;
using static FunctionalBabaIsYou.Sight;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Gameplay;

public class SightTests
{
    [Test]
    public void Identify_BlockAhead()
    {
        using var _ = new AssertionScope();
        
        IsAhead(Baba.AtOrigin(), Direction.Right)(Rock.AtMiddle()).Should().BeTrue();
        IsAhead(Baba.AtOrigin(), Direction.Left)(Rock.AtMiddle()).Should().BeFalse();
        IsAhead(Baba.AtOrigin(), Direction.Right)(Rock.AtRight()).Should().BeTrue();
        IsAhead(Baba.AtOrigin(), Direction.Right)(Rock.At(12312, 0)).Should().BeTrue();
        IsAhead(Baba.At(0, -1), Direction.Up)(Rock.At(0, 1)).Should().BeTrue();
        IsAhead(Baba.At(0, -1), Direction.Down)(Rock.At(0, 1)).Should().BeFalse();
        IsAhead(Baba.At(0, -1), Direction.Down)(Rock.At(0, -1)).Should().BeTrue();
        IsAhead(Baba.At(0, -1), Direction.Up)(Rock.At(0, 43242)).Should().BeTrue();
        IsAhead(Baba.At(0, 2), Direction.Down)(Rock.At(0, -12312)).Should().BeTrue();
        IsAhead(Baba.At(4, 0), Direction.Left)(Rock.At(-312312, 0)).Should().BeTrue();
    }
}