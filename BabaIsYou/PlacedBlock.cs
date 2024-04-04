using System.Collections.Generic;
using System.Linq;

namespace FunctionalBabaIsYou
{
    public readonly struct PlacedBlock
    {
        const string SubjectSuffix = "Subject";

        public readonly Coordinate WhereIs;
        public readonly IEnumerable<string> WhatDepicts;
        public int Y => WhereIs.Y;
        public int X => WhereIs.X;

        PlacedBlock(Coordinate whereIs, string whatDepicts)
        {
            this.WhereIs = whereIs;
            this.WhatDepicts = new[] { whatDepicts };
        }

        PlacedBlock(Coordinate whereIs, IEnumerable<string> whatDepicts)
        {
            this.WhereIs = whereIs;
            this.WhatDepicts = whatDepicts;
        }

        public bool Means(string something) => WhatDepicts.Any(block => block.Contains(something));
        public bool Means(PlacedBlock other) => other.WhatDepicts.All(Means);
        internal Movement Moving(Direction towards) => new(this, towards);
        public PlacedBlock AsSubject() => (WhereIs, WhatDepicts.First() + SubjectSuffix);

        public static implicit operator PlacedBlock((Coordinate whereIs, string whatDepicts) block) =>
            new(block.whereIs, block.whatDepicts);

        public static implicit operator PlacedBlock((Coordinate whereIs, IEnumerable<string> allThatDepicts) block) =>
            new(block.whereIs, block.allThatDepicts);

        public override bool Equals(object? other)
            => other is PlacedBlock block && WhereIs.Equals(block.WhereIs) &&
               WhatDepicts.SequenceEqual(block.WhatDepicts);

        public static PlacedBlock CreateDepicting(params string[] whatDepicts) => ((0, 0), whatDepicts);
    }
}