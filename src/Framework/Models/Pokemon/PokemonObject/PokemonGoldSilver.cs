using System;

namespace PokemonFramework.Framework.Models.Pokemon.PokemonObject
{
    public class PokemonGoldSilver : IPokemonObject
    {
        public PokemonGoldSilver(MemoryAddress address, PokemonMemoryType pokemonMemoryType) : base(address, pokemonMemoryType)
        { }

        public override int PokemonSize => throw new NotImplementedException();

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
