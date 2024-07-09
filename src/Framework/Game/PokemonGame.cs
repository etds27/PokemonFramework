using BizHawk.Client.Common;
using PokemonFramework.EmulatorBridge;
using PokemonFramework.EmulatorBridge.APIContainer;
using PokemonFramework.Framework.Game.Gen2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Game
{
    public class PokemonGame
    {
        /// <summary>
        /// Exception to be thrown when the current game is not recognized
        /// </summary>
        public class GameNotFoundException : Exception
        {
            public GameNotFoundException() { }
            public GameNotFoundException(string message) : base(message) { }
            public GameNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        }


        // MARK
        // Static references to the pokemon games
        public static IGame RED = new PokemonRed();
        public static IGame BLUE = new PokemonBlue();
        public static IGame GOLD = new PokemonGold();
        public static IGame SILVER = new PokemonSilver();
        public static IGame CRYSTAL = new PokemonCrystal();

        private static IEmulatorInterface API => new BizHawkEmulatorBridge();
        /// <summary>
        /// Mapping of the games ROM hash to the instance of the pokemon game within the framework
        /// </summary>
        private static readonly Dictionary<String, IGame> knownGameMap = new()
        {
            {"F2F52230B536214EF7C9924F483392993E226CFB", CRYSTAL }
        };

        /// <summary>
        /// Determines the current pokemon game that is loaded in the emulator
        /// </summary>
        /// <returns>The current pokemon game</returns>
        /// <exception cref="GameNotFoundException">Thrown if the current game has an unexpected ROM hash</exception>
        public static IGame GetCurrentGame()
        {
            string romHash = API.Game.GetRomHash();
            if (knownGameMap.TryGetValue(romHash, out IGame game))
            {
                return game;
            } else
            {
                throw new GameNotFoundException();
            }
        }
    }
}
