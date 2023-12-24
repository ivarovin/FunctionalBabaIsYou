using FluentAssertions;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class PhraseBuilderTests
{
    [Test]
    public void AppendSecondDefinition()
    {
        PhraseBuilder.BabaIsYou.AndRock().Should().BeEquivalentTo
        (
            new[]
            {
                PhraseBuilder.Baba.At(PhraseBuilder.Origin),
                PhraseBuilder.ToBe.At(PhraseBuilder.Middle),
                PhraseBuilder.You.At(PhraseBuilder.Right),
                PhraseBuilder.And.At(PhraseBuilder.Right * 2),
                PhraseBuilder.Rock.At(PhraseBuilder.Right * 3)
            }
        );
    }
}