using System;
using System.Collections.Generic;

namespace Astar
{
    public static class IsWakablePredicateFactory
    {
        public static Predicate<Point> CreateIsWakablePredicateFrom(HashSet<Point> walkablePoints) => 
            walkablePoints.Contains;
    }
}