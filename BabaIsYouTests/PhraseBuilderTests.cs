using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar
{
    public class PhraseBuilderTests
    {
        [Test]
        public void AppendSecondDefinition()
        {
            BabaIsYou.AndRock().Should().BeEquivalentTo
            (
                new[]
                {
                    BabaSubject.At(Origin),
                    ToBe.At(Middle),
                    You.At(Right),
                    And.At((3,0)),
                    Rock.At((4,0))
                }
            );
        }
    }
}