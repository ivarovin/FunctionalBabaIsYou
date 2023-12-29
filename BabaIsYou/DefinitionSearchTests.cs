using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

//Objetivo final
//1. Eliminar bucles y condicionales por mapeos y bindeos
//2. Extraer noción de sujeto
//3. Eliminar dependencia que hay entre código de producción y tests
//4. Asociar varias definiciones a un bloque

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
    public void Attach_SeveralDefinitions_ToDifferentSubjects_InSameWorld()
    {
        BabaIsYou.Concat(RockIsPush.MoveToRight(5)).DefinitionOf(Baba).Should().Be(You);
        BabaIsYou.Concat(RockIsPush.MoveToRight(5)).DefinitionOf(Rock).Should().Be(Push);
    }

    [Test]
    public void Definition_CannotExist_WithoutDefinition_AfterLinkingVerb()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle() }.DefinitionOf(Baba).Should().Be(Baba);
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

    [Test]
    public void Block_UsedAsSubject_DoesNotHave_Definition()
    {
        BabaIsYou.DefinitionOf(Baba.AtOrigin()).whatDepicts.Should().Be(Baba);
    }

    [Test]
    public void IdentifySubject_WithinAllBlocks()
    {
        new[] { Rock.AtOrigin(), Rock.AtMiddle() }.Concat(RockIsPush.Up())
            .AllDefinitionsOf(Rock.AtOrigin())
            .Should().Contain(Push);
    }
}