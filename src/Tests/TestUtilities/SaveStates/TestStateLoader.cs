using PokemonFramework.Framework.Models.Game;
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
    internal class TestStateManager
    {
        private static readonly string testStateDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tests", "TestStates");

        private static readonly IGame CurrentGame = PokemonGame.GetCurrentGame();
        public static string TestCheckPartySize
        {
            get
            {
                Dictionary<IGame, String> stateMap = new() {
                    {  PokemonGame.CRYSTAL, Path.Combine(testStateDirectory, "DayCareMan.State") }
                };

                return GetPath(stateMap);
            }
        }

        private static string GetPath(Dictionary<IGame, String> stateMap)
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
