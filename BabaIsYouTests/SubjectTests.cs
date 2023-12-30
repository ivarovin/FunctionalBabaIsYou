using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class SubjectTests
{
    [Test]
    public void Subject_CanMean_MultipleThings()
    {
        BabaIsYou.AndRock().DefinitionOf(Baba.AtSomewhere()).Means(You).Should().BeTrue();
        BabaIsYou.AndRock().DefinitionOf(Baba.AtSomewhere()).Means(Rock).Should().BeTrue();
    }
}