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
    public void AttachDefinition_ToSubject()
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
        new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight() }
            .MoveToRight(10)
            .DefinitionOf(Baba)
            .Should().Be(You);
    }

    [Test]
    public void Find_SeveralDefinitions_ForDifferentSubjects()
    {
        var sut = new[] { Baba.AtOrigin(), ToBe.AtMiddle(), You.AtRight() }
            .Concat(new[] { Rock.AtOrigin(), ToBe.AtMiddle(), Push.AtRight() }.MoveToRight(5));

        sut.DefinitionOf(Baba).Should().Be(You);
        sut.DefinitionOf(Rock).Should().Be(Push);
    }

    [Test]
    public void Definition_CannotExist_WithoutDefinition_AfterLinkingVerb()
    {
        new[] { Baba.AtOrigin(), ToBe.AtMiddle() }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }
}