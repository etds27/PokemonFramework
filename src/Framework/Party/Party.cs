using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Game;
using PokemonFramework.Framework.Module;
using PokemonFramework.Framework.Party.PartyObject;
using PokemonFramework.Framework.Pokemon;
using PokemonFramework.Framework.Pokemon.PokemonConfig;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonFramework.Framework.Party
{
    using Constructor = Func<IPartyObject>;

    public interface IPartyObject
    {
        /// <summary>
        /// Get the number of pokemon currently in the player's party
        /// </summary>
        /// <returns>Number of pokemon currently in the player's party</returns>
        public int GetNumberOfPokemonInParty();
        /// <summary>
        /// Get the memory address of the pokemon at the specified index in the party
        /// </summary>
        /// <param name="index">Index of the pokemon to get the address for</param>
        /// <returns>Memory address of the pokemon</returns>
        public long GetPokemonAddress(int index);
        // public Pokemon GetPokemonAtIndex(int index);
        // public IReadOnlyList<Pokemon> GetPokemonInParty();
        // public IReadOnlyList<Pokemon> GetEggsInParty();
        // public IReadOnlyList<Pokemon> GetEggMask();
        /// <summary>
        /// Get the number of eggs currently in the player's party
        /// </summary>
        /// <returns>=Number of eggs currently in the player's party</returns>
        public int GetNumberOfEggsInParty();
        /// <summary>
        /// Open the party menu and open the pokemon's party page
        /// </summary>
        /// <param name="index">Index of the pokemon's page to views</param>
        /// <returns></returns>
        public bool NavigateToPokemon(int index);
    }

    public class PartyFactory : TopLevelModule<IPartyObject, object>
    {
        internal new static Dictionary<IGame, Constructor> GameConstructorMap = new()
        {
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
