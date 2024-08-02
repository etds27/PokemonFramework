using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Module;
using PokemonFramework.Framework.Pokemon;
using PokemonFramework.Framework.Pokemon.PokemonConfig;
using System;
using System.Collections.Generic;

namespace PokemonFramework.Framework.Party.PartyObject
{
    internal class PartyCrystal : FrameworkModule, IPartyObject
    {
        private readonly PokemonFactory _pokemonFactory = new();
        private readonly IPokemonConfig pokemonConfig = new PokemonCrystalConfig();

        private readonly MemoryQuery numPokemonInParty = new (
            address: 0xDCD7,
            size: 1,
            domain: MemoryDomain.WRAM
        );

        private readonly MemoryQuery startingPokemonAddress = new(
            address: 0xDCD7 + 0x8,
            size: 1,
            domain: MemoryDomain.WRAM
        );

        public int GetNumberOfEggsInParty()
        {
            return API.Memory.ReadInt(query: numPokemonInParty);
        }

        public int GetNumberOfPokemonInParty()
        {
            return API.Memory.ReadInt(query: numPokemonInParty);
        }

        public long GetPokemonAddress(int index)
        {
            return startingPokemonAddress.Address + index * _pokemonFactory.PokemonSizeInParty;
        }

        public IPokemonObject GetPokemonAtIndex(int index)
        {
            MemoryAddress addr = GetPokemonAddress(index);
            return _pokemonFactory.CreateObject(address: addr, pokemonMemoryType: PokemonMemoryType.PARTY);
        }

        public IReadOnlyList<IPokemonObject> GetPokemonInParty()
        {
            List<IPokemonObject> list = [];
            for (int i = 0; i < GetNumberOfPokemonInParty(); i++)
            {
                list.Add( GetPokemonAtIndex(i) );
            }
            return list;
        }

        public bool NavigateToPokemon(int index)
        {
            throw new NotImplementedException();
        }
    }
}
