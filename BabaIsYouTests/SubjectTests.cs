using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class SubjectTests
{
    [Test]
    public void Subject_CanMean_MultipleThings()
    {
        PlacedBlock.CreateDepicting(You, Rock).Means(You).Should().BeTrue();
        PlacedBlock.CreateDepicting(You, Rock).Means(Rock).Should().BeTrue();
    }
}