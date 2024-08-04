using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Models.Pokemon;
using PokemonFramework.Framework.Models.Pokemon.PokemonConfig;
using System;

namespace PokemonFramework.Framework.Models.Party.PartyObject
{
    internal class PartyGoldSilver : IPartyObject
    {

        private readonly IPokemonConfig pokemonConfig = new PokemonConfigGoldSilver();

        internal override MemoryQuery NumPokemonInPartyQuery
        {
            get
            {
                return new MemoryQuery(address: 0xDA22, size: 1, domain: MemoryDomain.WRAM);
            }
        }

        internal override MemoryAddress StartingPokemonAddress
        {
            get
            {
                return 0xDA22 + 0x8;
            }
        }

        public override bool NavigateToPokemon(int index)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
