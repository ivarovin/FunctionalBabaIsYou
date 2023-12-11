using FluentAssertions;

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
    public void NothingDefinesSubject_WhenNoBlockExist()
    {
        Array.Empty<((int x, int y), string subject)>()
            .DefinitionOf("Baba")
            .Should().BeEmpty();
    }
}

public static class safsafsa
{
    public static string DefinitionOf(this IEnumerable<((int x, int y), string block)> blocks, string subject)
    {
        return string.Empty;
    }
}