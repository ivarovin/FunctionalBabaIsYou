using FluentAssertions;
using static BabaIsYou.Tests.PhraseBuilder;

namespace BabaIsYou.Tests;

//OBJETIVO INICIAL
//1. Algo se identifica como TÚ
//2. TÚ te puedes mover

//PARA ALCANZARLO
//1. TÚ puedes ser cualquier cosa
//2. Se usan frases para definir qué eres TÚ
//2.1. Las frases siempre van unidas por un "is"
//2.2. Las frases empiezan por un sujeto
//2.3. Las frases pueden acabar con un adjetivo u otro sujeto
//3. Una frase puede tener o no sentido.
//3.1 Cuando una frase tiene sentido resulta en un cambio de estado para el sujeto

public class GrammarTests
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
            .Should().BeEmpty();
    }

    [Test]
    public void LinkingVerb_ShouldBe_BetweenSubjectAndDefinition()
    {
        new[] { ToBe.AtOrigin(), Baba.AtMiddle(), You.AtRight() }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }

    [Test]
    public void Definition_CannotExist_BeforeSubject()
    {
        new[] { You.AtOrigin(), ToBe.AtMiddle(), Baba.AtRight() }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }

    [Test]
    public void Definition_CanBeAssigned_AtAnyPlace()
    {
        PhraseBuilder.BabaIsYou.MoveToRight(10).DefinitionOf(Baba).Should().Be(You);
    }

    [Test]
    public void Definition_CannotExist_IfRequestedSubject_IsNotPresent()
    {
        new[] { ToBe.AtMiddle(), You.AtRight().MoveToRight(1) }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }

    [Test]
    public void Definition_CannotExist_IfSeparated_FromLinkingVerb()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight().MoveToRight(1) }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }

    [Test]
    public void Definition_CannotExist_IfSubject_IsSeparated()
    {
        new[] { Baba.AtOrigin().MoveToLeft(1), ToBe.AtMiddle(), You.AtRight() }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }

    [Test]
    public void Attach_SeveralDefinitions_ToDifferentSubjects()
    {
        PhraseBuilder.BabaIsYou.Concat(RockIsPush.MoveToRight(5)).DefinitionOf(Baba).Should().Be(You);
        PhraseBuilder.BabaIsYou.Concat(RockIsPush.MoveToRight(5)).DefinitionOf(Rock).Should().Be(Push);
    }

    [Test]
    public void Definition_CannotExist_WithoutDefinition_AfterLinkingVerb()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle() }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }

    [Test]
    public void AttachDefinition_ToSubject_InOtherHeight()
    {
        PhraseBuilder.BabaIsYou.MoveDown(10).DefinitionOf(Baba).Should().Be(You);
    }

    [Test]
    public void AllBlocks_MustBeAtSameHeight_ToDefineSubject()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle().MoveDown(1), You.AtRight() }
            .DefinitionOf(Baba)
            .Should().BeEmpty();

        new[] { Baba.AtOrigin().MoveDown(1), ToBe.AtMiddle(), You.AtRight() }
            .DefinitionOf(Baba)
            .Should().BeEmpty();

        new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight().MoveDown(1) }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }

    [Test]
    public void Attach_SeveralDefinitions_ToDifferentSubjects_AtDifferentHeights()
    {
        PhraseBuilder.BabaIsYou.Concat(RockIsPush.MoveDown(1)).DefinitionOf(Baba).Should().Be(You);
        PhraseBuilder.BabaIsYou.Concat(RockIsPush.MoveDown(1)).DefinitionOf(Rock).Should().Be(Push);
    }

    [Test]
    public void FindSubject_AttachedToDefinition()
    {
        PhraseBuilder.BabaIsYou.WhatIs(You).Should().Be(Baba);
    }

    [Test]
    public void Subject_CannotExist_IfThereIsNo_LinkingVerb()
    {
        new[] { Baba.AtOrigin(), You.AtRight() }
            .WhatIs(You)
            .Should().BeEmpty();
    }

    [Test]
    public void Subject_CannotExist_IfThereIsNo_Definition()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle() }
            .WhatIs(You)
            .Should().BeEmpty();
    }

    [Test]
    public void SpawnBaba_InWorld()
    {
        World.CreateWith(new[] { Baba.AtOrigin() })
            .WhereIs(Baba)
            .Should().Be((0, 0));
    }

    [Test]
    public void Move_IsNotPossible_IfYou_AreNotDefined()
    {
        World.CreateWith(new[] { Baba.AtOrigin() })
            .MoveTowards((1, 0))
            .WhereIs(Baba)
            .Should().Be((0, 0));
    }

    [Test]
    public void Move_TowardsDirection_IfYou_AreDefined()
    {
        World.CreateWith(new[] { Baba.AtOrigin() }, PhraseBuilder.BabaIsYou.MoveToRight(1))
            .MoveTowards((0, 1))
            .WhereIs(Baba)
            .Should().NotBe((0, 0));
    }
}