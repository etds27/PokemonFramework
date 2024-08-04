using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonFramework.Framework.Models.Pokemon;

namespace PokemonFramework.Framework.Models.Pokemon.PokemonConfig
{
    public class PokemonConfigCrystal : IPokemonConfig
    {
        public int PokemonSizeInParty => 48;

        public int PokemonSizeInBattle => 48;

        public int PokemonSizeInBox => 32;
    }
}
