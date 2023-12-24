using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class MultipleDefinitionsTests
{
    [Test]
    public void Find_OneDefinition()
    {
        BabaIsYou.AllDefinitionsOf(Baba.AtOrigin()).Should().HaveCount(1).And.Contain(You);
    }

    [Test]
    public void Subject_CanHave_MultipleDefinitions()
    {
        BabaIsYou.AndRock().AllDefinitionsOf(Baba.AtOrigin()).Should().HaveCount(2);
    }

    [Test]
    public void SecondDefinition_CannotBeApplied_IfConjunction_IsAlone()
    {
        BabaIsYou.AppendConjunction().AllDefinitionsOf(Baba.AtOrigin()).Should().HaveCount(1);
    }
    
    [Test]
    public void Chain_Three_Definitions()
    {
        BabaIsYou.AndRock().AndPush()
            .AllDefinitionsOf(Baba.AtOrigin())
            .Should().HaveCount(3).And.Contain(You, Rock, Push);
    }
}