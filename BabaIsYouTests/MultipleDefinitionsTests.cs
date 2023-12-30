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
    public void SubjectCanHave_MultipleDefinitions()
    {
        BabaIsYou.AndRock().AllDefinitionsOf(Baba.AtSomewhere()).Should().HaveCount(2);
    }

    [Test]
    public void SubjectCanHave_MultipleDefinitions_Vertically()
    {
        BabaIsYou.AndRock().Vertically().AllDefinitionsOf(Baba.AtSomewhere()).Should().HaveCount(2);
    }

    [Test]
    public void SubjectCanHave_MultipleDefinitions_PerpendicularToIt()
    {
        BabaIsYou.Vertically().Append(ToBe.AtMiddle()).Append(Rock.AtRight())
            .AllDefinitionsOf(Baba.AtSomewhere())
            .Should().HaveCount(2);
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