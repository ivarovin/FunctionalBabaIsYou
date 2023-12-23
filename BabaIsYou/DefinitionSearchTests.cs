using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

//NUEVO OBJETIVO
//1. Se acaba el mundo cuando no hay más tús
//2. Empujar actores definidos como PUSH

//PARA ALCANZARLO
//1. Un sujeto puede tener más de una definición
//2. Debemos poder empujar objetos definidos como PUSH
//3. Debemos morir cuando nos solapamos con un objeto DEFEAT

public class DefinitionSearchTests
{
    [Test]
    public void Definition_IsAttached_ToSubject()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight() }
            .DefinitionOf(Baba)
            .Should().Be(You);
    }

    [Test]
    public void Definition_CannotExist_WithoutLinkingVerb()
    {
        new[] { Baba.AtOrigin(), You.AtMiddle() }
            .DefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void LinkingVerb_ShouldBe_BetweenSubjectAndDefinition()
    {
        new[] { ToBe.AtOrigin(), Baba.AtMiddle(), You.AtRight() }
            .DefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Definition_CannotExist_BeforeSubject()
    {
        new[] { You.AtOrigin(), ToBe.AtMiddle(), Baba.AtRight() }
            .DefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Definition_CanBeAssigned_AtAnyPlace()
    {
        BabaIsYou.MoveToRight(10).DefinitionOf(Baba).Should().Be(You);
    }

    [Test]
    public void Definition_CannotExist_IfRequestedSubject_IsNotPresent()
    {
        new[] { ToBe.AtMiddle(), You.AtRight().MoveToRight(1) }
            .DefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Definition_CannotExist_IfSeparated_FromLinkingVerb()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight().MoveToRight(1) }
            .DefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Definition_CannotExist_IfSubject_IsSeparated()
    {
        new[] { Baba.AtOrigin().MoveToLeft(1), ToBe.AtMiddle(), You.AtRight() }
            .DefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void Attach_SeveralDefinitions_ToDifferentSubjects()
    {
        BabaIsYou.Concat(RockIsPush.MoveToRight(5)).DefinitionOf(Baba).Should().Be(You);
        BabaIsYou.Concat(RockIsPush.MoveToRight(5)).DefinitionOf(Rock).Should().Be(Push);
    }

    [Test]
    public void Definition_CannotExist_WithoutDefinition_AfterLinkingVerb()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle() }
            .DefinitionOf(Baba)
            .Should().Be(Baba);
    }

    [Test]
    public void AttachDefinition_ToSubject_InOtherHeight()
    {
        BabaIsYou.Down(10).DefinitionOf(Baba).Should().Be(You);
    }

    [Test]
    public void AllBlocks_MustBeAtSameHeight_ToDefineSubject()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle().MoveDown(), You.AtRight() }
            .DefinitionOf(Baba)
            .Should().NotBe(You);

        new[] { Baba.AtOrigin().MoveDown(), ToBe.AtMiddle(), You.AtRight() }
            .DefinitionOf(Baba)
            .Should().NotBe(You);

        new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight().MoveDown() }
            .DefinitionOf(Baba)
            .Should().NotBe(You);
    }

    [Test]
    public void Attach_SeveralDefinitions_ToDifferentSubjects_AtDifferentHeights()
    {
        BabaIsYou.Concat(RockIsPush.Down()).DefinitionOf(Baba).Should().Be(You);
        BabaIsYou.Concat(RockIsPush.Down()).DefinitionOf(Rock).Should().Be(Push);
    }

    [Test]
    public void You_DefinesYourself_IfThereIsNoOtherDefinition()
    {
        new[] { Rock.AtOrigin(), ToBe.AtMiddle() }
            .DefinitionOf(Rock)
            .Should().Be(Rock);
    }

    [Test]
    public void Find_DefinitionOf_PlacedBlock()
    {
        BabaIsYou.DefinitionOf(Baba.AtMiddle()).whereIs.Should().Be(Middle);
        BabaIsYou.DefinitionOf(Baba.AtMiddle()).whatDepicts.Should().Be(You);
    }
}

public class MultipleDefinitionsTests
{
    [Test]
    public void Find_AllDefinitions()
    {
        BabaIsYou.AllDefinitionsOf(Baba.AtOrigin()).Should().HaveCount(1).And.Contain(You);
    }
}