using PokemonFramework.Framework.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Tests.TestUtilities.SaveStates
{
    internal class TestStateLoader
    {
        private static readonly IGame CurrentGame = PokemonGame.GetCurrentGame();
        public static String TestCheckPartySize
        {
            get
            {
                Dictionary<IGame, String> stateMap = new() {
                    {  PokemonGame.CRYSTAL, Path.GetFullPath(@"C:\Users\etds2\Programming\PokemonLua\Tests\States\Crystal\EggTests") }
                };

                return GetPath(stateMap);
            }
        }

        private static String GetPath(Dictionary<IGame, String> stateMap)
        {
            if (stateMap.TryGetValue(CurrentGame, out String path))
            {
                return path;
            } else
            {
                throw new PokemonGame.GameNotFoundException();
            }
        }
    }
}
