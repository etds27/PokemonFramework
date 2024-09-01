using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Utility.Coordinate
{
    public struct Coordinate(int x, int y) : IEquatable<Coordinate>
    {
        public int X = x; 
        public int Y = y;

        public readonly bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
