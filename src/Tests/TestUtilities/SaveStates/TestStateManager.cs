using PokemonFramework.Framework.Models.Game;
using System;
using System.Collections.Generic;
using System.IO;

namespace PokemonFramework.Tests.TestUtilities.SaveStates
{
    internal class TestStateManager
    {
        private static string TestStateDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "src", "Tests", "TestStates", CurrentGame.ShortName);

        private static readonly IGame CurrentGame = PokemonGame.GetCurrentGame();

        public static string TestBagState => Path.Combine(TestStateDirectory, "BagTests");


        public static string TestCheckPartySize
        {
            get
            {
                Dictionary<IGame, String> stateMap = new() {
                    {  PokemonGame.CRYSTAL, Path.Combine(TestStateDirectory, "DayCareMan") }
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
