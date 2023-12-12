using FluentAssertions;
using static BabaIsYou.Subject;

namespace BabaIsYou;

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

public class Tests
{
    [Test]
    public void AttachDefinition_ToSubject()
    {
        new[] { ((0, 0), Baba), ((1, 0), "is"), ((2, 0), "You") }
            .DefinitionOf(Baba)
            .Should().Be("You");
    }

    [Test]
    public void Definition_CannotExist_WithoutLinkingVerb()
    {
        new[] { ((0, 0), Baba), ((1, 0), "You") }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }
    
    [Test]
    public void LinkingVerb_ShouldBe_BetweenSubjectAndDefinition()
    {
        new[] { ((0, 0), "is"), ((1, 0), Baba), ((2, 0), "You") }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }
    
    [Test]
    public void Definition_CannotExist_BeforeSubject()
    {
        new[] { ((0, 0), "You"), ((1, 0), "is"), ((2, 0), Baba) }
            .DefinitionOf(Baba)
            .Should().BeEmpty();
    }
}