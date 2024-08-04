using System;

namespace PokemonFramework.Framework.Models.Pokemon.PokemonObject
{
    public class PokemonCrystal : IPokemonObject
    {
        public PokemonCrystal(MemoryAddress address, PokemonMemoryType pokemonMemoryType) : base(address, pokemonMemoryType)
        { }

        public override int PokemonSize => throw new NotImplementedException();

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
