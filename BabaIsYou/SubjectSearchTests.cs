using FluentAssertions;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class SubjectSearchTests
{
    [Test]
    public void FindSubject_AttachedToDefinition()
    {
        PhraseBuilder.BabaIsYou.WhatIs(PhraseBuilder.You).Should().Be(PhraseBuilder.Baba);
    }

    [Test]
    public void Subject_CannotExist_IfThereIsNo_LinkingVerb()
    {
        new[] { PhraseBuilder.Baba.AtOrigin(), PhraseBuilder.You.AtRight() }
            .WhatIs(PhraseBuilder.You)
            .Should().BeEmpty();
    }

    [Test]
    public void Subject_CannotExist_IfThereIsNo_Definition()
    {
        new[] { PhraseBuilder.Baba.AtOrigin(), PhraseBuilder.ToBe.AtMiddle() }
            .WhatIs(PhraseBuilder.You)
            .Should().BeEmpty();
    }
}