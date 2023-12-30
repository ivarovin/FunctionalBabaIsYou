using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class MultipleDefinitionsTests
{
    [Test]
    public void Find_OneDefinition()
    {
        BabaIsYou.AllDefinitionsOf(Baba.AtSomewhere()).Should().HaveCount(1).And.Contain(You);
    }

    [Test]
    public void Subject_CanHave_MultipleDefinitions()
    {
        BabaIsYou.AndRock().DefinitionOf(Baba.AtSomewhere()).allThatDepicts.Should().HaveCount(2);
    }
    
    [Test]
    public void Subject_CanMean_MultipleThings()
    {
        BabaIsYou.AndRock().DefinitionOf(Baba.AtSomewhere()).Means(You).Should().BeTrue();
        BabaIsYou.AndRock().DefinitionOf(Baba.AtSomewhere()).Means(Rock).Should().BeTrue();
    }

    [Test]
    public void SecondDefinition_CannotBeApplied_IfConjunction_IsAlone()
    {
        BabaIsYou.AppendConjunction().AllDefinitionsOf(Baba.AtSomewhere()).Should().HaveCount(1);
    }
    
    [Test]
    public void Chain_Three_Definitions()
    {
        BabaIsYou.AndRock().AndPush()
            .AllDefinitionsOf(Baba.AtSomewhere())
            .Should().HaveCount(3).And.Contain(You, Rock, Push);
    }
}