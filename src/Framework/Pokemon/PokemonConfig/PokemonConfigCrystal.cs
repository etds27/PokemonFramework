using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Pokemon.PokemonConfig
{
    public class PokemonCrystalConfig : IPokemonConfig
    {
        public int PokemonSizeInParty => 48;

        public int PokemonSizeInBattle => 48;

        public int PokemonSizeInBox => 32;
    }
}
