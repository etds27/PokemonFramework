using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Models.Game;
using PokemonFramework.Framework.Models.Module;
using PokemonFramework.Framework.Models.Party.PartyObject;
using PokemonFramework.Framework.Models.Pokemon;
using System;
using System.Collections.Generic;

namespace PokemonFramework.Framework.Models.Party
{
    using Constructor = Func<IPartyObject>;

    public abstract class IPartyObject : FrameworkObject
    {
        internal readonly PokemonFactory _pokemonFactory = new();

        internal abstract MemoryQuery NumPokemonInPartyQuery
        {
            get;
        }

        internal abstract MemoryAddress StartingPokemonAddress
        {
            get;
        }

        /// <summary>
        /// Get the number of pokemon currently in the player's party
        /// </summary>
        /// <returns>Number of pokemon currently in the player's party</returns>
        public int GetNumberOfPokemonInParty()
        {
            return API.Memory.ReadInt(query: NumPokemonInPartyQuery);
        }

        /// <summary>
        /// Get the memory address of the pokemon at the specified index in the party
        /// </summary>
        /// <param name="index">Index of the pokemon to get the address for</param>
        /// <returns>Memory address of the pokemon</returns>
        public MemoryAddress GetPokemonAddress(int index)
        {
            return StartingPokemonAddress + index * _pokemonFactory.PokemonSizeInParty;
        }

        /// <summary>
        /// Get the Pokemon at the specified index in the user's party
        /// </summary>
        /// <param name="index">Index of the Pokemon to get</param>
        /// <returns></returns>
        public IPokemonObject GetPokemonAtIndex(int index)
        {
            MemoryAddress addr = GetPokemonAddress(index);
            return _pokemonFactory.CreateObject(address: addr, pokemonMemoryType: PokemonMemoryType.PARTY);
        }

        /// <summary>
        /// Get all of the Pokemon that are currently in the party
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IPokemonObject> GetPokemonInParty()
        {
            List<IPokemonObject> list = [];
            for (int i = 0; i < GetNumberOfPokemonInParty(); i++)
            {
                list.Add(GetPokemonAtIndex(i));
            }
            return list;
        }

        /// <summary>
        /// Open the party menu and open the pokemon's party page
        /// </summary>
        /// <param name="index">Index of the pokemon's page to views</param>
        /// <returns></returns>
        public abstract bool NavigateToPokemon(int index);
    }

    public class PartyFactory : TopLevelModule<IPartyObject, object>
    {
        internal new static Dictionary<IGame, Constructor> GameConstructorMap = new()
        {
            { PokemonGame.GOLD, () => new PartyGoldSilver() },
            { PokemonGame.SILVER, () => new PartyGoldSilver() },
            { PokemonGame.CRYSTAL, () => new PartyCrystal() }
        };

        public IPartyObject CreateObject()
        {
            if ((GameConstructorMap ?? []).TryGetValue(CurrentGame, out Constructor tempObject))
            {
                if (tempObject != null)
                {
                    return tempObject();
                }
                else
                {
                    throw new SubModuleNotImplemented();
                }
            }
            else
            {
                throw new SubModuleNotFoundException();
            }
        }
    }
}
