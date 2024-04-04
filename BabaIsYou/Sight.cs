using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalBabaIsYou
{
    internal static class Sight
    {
        public static IEnumerable<PlacedBlock> At(this IEnumerable<PlacedBlock> all, Coordinate where)
            => all.Where(IsAt(where));

        static Func<PlacedBlock, bool> IsAt(Coordinate position) => x => x.WhereIs == position;
        static Func<PlacedBlock, bool> IsAt(PlacedBlock block) => IsAt(block.WhereIs);
        public static Func<PlacedBlock, bool> IsAtAny(IEnumerable<PlacedBlock> where) => block => where.Any(IsAt(block));
        public static Func<PlacedBlock, bool> IsAhead(Movement move) => IsAhead(move.Who, move.Direction);

        public static Func<PlacedBlock, bool> IsAhead(PlacedBlock from, Direction towards)
            => to => (to.WhereIs - DistanceBetween(from, to) * towards).Equals(from.WhereIs);

        static Coordinate DistanceBetween(PlacedBlock from, PlacedBlock to)
            => (Math.Abs((to.WhereIs - from.WhereIs).X), Math.Abs((to.WhereIs - from.WhereIs).Y));
    }
}