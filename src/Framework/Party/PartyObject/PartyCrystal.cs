using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Module;
using PokemonFramework.Framework.Pokemon;
using System;

namespace PokemonFramework.Framework.Party.PartyObject
{
    internal class PartyCrystal : FrameworkModule, IPartyObject
    {
        private readonly PokemonFactory _factory = new();

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
            return startingPokemonAddress.Address + (index - 1) * _factory.PokemonSizeInParty;
        }

        public bool NavigateToPokemon(int index)
        {
            throw new NotImplementedException();
        }
    }
}
