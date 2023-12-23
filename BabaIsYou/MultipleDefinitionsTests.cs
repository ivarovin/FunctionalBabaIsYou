using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class MultipleDefinitionsTests
{
    [Test]
    public void Find_AllDefinitions()
    {
        BabaIsYou.AllDefinitionsOf(Baba.AtOrigin()).Should().HaveCount(1).And.Contain(You);
    }

    [Test]
    public void Subject_CanHave_MultipleDefinitions()
    {
        BabaIsYou.AndRock().AllDefinitionsOf(Baba.AtOrigin()).Should().HaveCount(2);
    }
}