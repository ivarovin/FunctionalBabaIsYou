using FluentAssertions;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class MultipleDefinitionsTests
{
    [Test]
    public void Find_AllDefinitions()
    {
        PhraseBuilder.BabaIsYou.AllDefinitionsOf(PhraseBuilder.Baba.AtOrigin()).Should().HaveCount(1).And.Contain(PhraseBuilder.You);
    }
}