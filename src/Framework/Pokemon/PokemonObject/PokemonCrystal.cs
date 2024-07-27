using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Pokemon.PokemonObject
{
    public class PokemonCrystal : IPokemonObject
    {
        public PokemonCrystal(MemoryQuery query, PokemonMemoryType pokemonMemoryType) : base(query, pokemonMemoryType)
        {}

        public override int PokemonSize => throw new NotImplementedException();

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
