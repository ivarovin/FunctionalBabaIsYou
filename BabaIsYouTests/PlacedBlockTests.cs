using FluentAssertions;
using static FunctionalBabaIsYou.Tests.PhraseBuilder;

namespace FunctionalBabaIsYou.Tests.Grammar;

public class PlacedBlockTests
{
    [Test]
    public void CanMean_MultipleThings()
    {
        PlacedBlock.CreateDepicting(You, Rock).Means(You).Should().BeTrue();
        PlacedBlock.CreateDepicting(You, Rock).Means(Rock).Should().BeTrue();
    }
    
    [Test]
    public void CanMean_OtherBlock_ByContaining_AllItsDefinitions()
    {
        PlacedBlock.CreateDepicting(You, Rock).Means(PlacedBlock.CreateDepicting(You)).Should().BeTrue();
        PlacedBlock.CreateDepicting(You, Rock).Means(PlacedBlock.CreateDepicting(You, Win)).Should().BeFalse();
    }

    [Test]
    public void Keep_AllDefinitions_AfterMoving()
    {
        PlacedBlock.CreateDepicting(You, Rock).Moving(Direction.Down).Commit().WhatDepicts.Should().HaveCount(2);
    }
}