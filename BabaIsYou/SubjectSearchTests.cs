using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class SubjectSearchTests
{
    [Test]
    public void FindSubject_AttachedToDefinition()
    {
        BabaIsYou.WhatIs(You).Should().Be(Baba);
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
}