using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class DefinitionSearchTests
{
    [Test]
    public void Definition_IsAttached_ToSubject()
    {
        new[] { BabaSubject.AtOrigin(), ToBe.AtMiddle(), You.AtRight() }
            .FirstDefinitionOf(Baba)
            .Should().Be(You);
    }

    [Test]
    public void Definition_CannotExist_WithoutLinkingVerb()
    {
        new[] { BabaSubject.AtOrigin(), You.AtMiddle() }
            .FirstDefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void LinkingVerb_ShouldBe_BetweenSubjectAndDefinition()
    {
        new[] { ToBe.AtOrigin(), BabaSubject.AtMiddle(), You.AtRight() }
            .FirstDefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Definition_CannotExist_BeforeSubject()
    {
        new[] { You.AtOrigin(), ToBe.AtMiddle(), BabaSubject.AtRight() }
            .FirstDefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Definition_CanBeAssigned_AtAnyPlace()
    {
        BabaIsYou.MoveToRight(10).FirstDefinitionOf(Baba).Should().Be(You);
    }

    [Test]
    public void Definition_CannotExist_IfRequestedSubject_IsNotPresent()
    {
        new[] { ToBe.AtMiddle(), You.AtRight().MoveToRight(1) }
            .FirstDefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Definition_CannotExist_IfSeparated_FromLinkingVerb()
    {
        new[] { BabaSubject.AtOrigin(), ToBe.AtMiddle(), You.AtRight().MoveToRight(1) }
            .FirstDefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Definition_CannotExist_IfSubject_IsSeparated()
    {
        new[] { BabaSubject.AtOrigin().MoveToLeft(1), ToBe.AtMiddle(), You.AtRight() }
            .FirstDefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Attach_SeveralDefinitions_ToDifferentSubjects_InSameWorld()
    {
        BabaIsYou.Concat(RockIsPush.MoveToRight(5)).FirstDefinitionOf(Baba).Should().Be(You);
        BabaIsYou.Concat(RockIsPush.MoveToRight(5)).FirstDefinitionOf(Rock).Should().Be(Push);
    }

    [Test]
    public void Definition_CannotExist_WithoutDefinition_AfterLinkingVerb()
    {
        new[] { BabaSubject.AtOrigin(), ToBe.AtMiddle() }.FirstDefinitionOf(Baba).Should().Be(Baba);
    }

    [Test]
    public void AttachDefinition_ToSubject_InOtherHeight()
    {
        BabaIsYou.Down(10).FirstDefinitionOf(Baba).Should().Be(You);
    }

    [Test]
    public void AllBlocks_MustBeAtSameHeight_ToDefineSubject()
    {
        new[] { BabaSubject.AtOrigin(), ToBe.AtMiddle().MoveDown(), You.AtRight() }
            .FirstDefinitionOf(Baba)
            .Should().NotBe(You);

        new[] { BabaSubject.AtOrigin().MoveDown(), ToBe.AtMiddle(), You.AtRight() }
            .FirstDefinitionOf(Baba)
            .Should().NotBe(You);

        new[] { BabaSubject.AtOrigin(), ToBe.AtMiddle(), You.AtRight().MoveDown() }
            .FirstDefinitionOf(Baba)
            .Should().NotBe(You);
    }

    [Test]
    public void Attach_SeveralDefinitions_ToDifferentSubjects_AtDifferentHeights()
    {
        BabaIsYou.Concat(RockIsPush.Down()).FirstDefinitionOf(Baba).Should().Be(You);
        BabaIsYou.Concat(RockIsPush.Down()).FirstDefinitionOf(Rock).Should().Be(Push);
    }

    [Test]
    public void You_DefinesYourself_IfThereIsNoOtherDefinition()
    {
        new[] { RockSubject.AtOrigin(), ToBe.AtMiddle() }
            .FirstDefinitionOf(Rock)
            .Should().Be(Rock);
    }

    [Test]
    public void Find_DefinitionOf_PlacedBlock()
    {
        BabaIsYou.DefinitionOf(Baba.AtMiddle()).WhereIs.Should().Be(Middle);
        BabaIsYou.DefinitionOf(Baba.AtMiddle()).Means(You).Should().BeTrue();
    }

    [Test]
    public void Block_UsedAsSubject_DoesNotHave_Definition()
    {
        BabaIsYou.DefinitionOf(BabaSubject.AtOrigin()).Means(BabaSubject).Should().BeTrue();
    }

    [Test]
    public void IdentifySubject_WithinAllBlocks()
    {
        new[] { Rock.AtOrigin(), Rock.AtMiddle() }.Concat(RockIsPush.Up())
            .AllDefinitionsOf(Rock.AtOrigin())
            .Should().Contain(Push);
    }

    [Test]
    public void AttachDefinition_ToSubject_Vertically()
    {
        new[]
            {
                BabaSubject.AtOrigin(),
                ToBe.AtOrigin().MoveDown(),
                You.AtOrigin().MoveDown(2)
            }
            .DefinitionOf(Baba.AtSomewhere())
            .Means(You).Should().BeTrue();
    }
}