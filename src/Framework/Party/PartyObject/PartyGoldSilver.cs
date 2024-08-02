using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Module;
using PokemonFramework.Framework.Pokemon;
using PokemonFramework.Framework.Pokemon.PokemonConfig;
using System;
using System.Collections.Generic;

namespace PokemonFramework.Framework.Party.PartyObject
{
    internal class PartyGoldSilver : IPartyObject
    {
        
        private readonly IPokemonConfig pokemonConfig = new PokemonConfigGoldSilver();

        internal override MemoryQuery NumPokemonInPartyQuery
        {
            get {
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

        public override int GetNumberOfEggsInParty()
        {
            return API.Memory.ReadInt(query: NumPokemonInPartyQuery);
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
