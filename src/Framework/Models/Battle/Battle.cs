using BizHawk.Common;
using PokemonFramework.EmulatorBridge.InputInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Models.Menu;
using PokemonFramework.Framework.Models.Module;
using PokemonFramework.Framework.Utility.Coordinate;
using System;
using System.Net.NetworkInformation;

namespace PokemonFramework.Framework.Models.Battle
{
    public enum CatchStatus
    {
        PENDING,
        CAUGHT,
        MISSED
    }

    [Serializable]
    public class BattleException : Exception
    {
        public BattleException() : base() { }
        public BattleException(string message) : base(message) { }
        public BattleException(string message, Exception innerException) : base(message, innerException) { }
    }

    public abstract class IBattleObject() : FrameworkObject
    {

        internal static readonly MenuFactory menuFactory = new();

        public abstract class IBattleMenu(MemoryQuery queryX, MemoryQuery queryY) : Menu2D(queryX, queryY)
        {
            internal abstract Coordinate BattleMenuCoordinate {  get; }
            internal abstract Coordinate PokemonMenuCoordinate { get; }
            internal abstract Coordinate PackMenuCoordinate { get; }
            internal abstract Coordinate RunMenuCoordinate { get; }

            /// <summary>
            /// Get the current location of the cursor within the battle menu
            /// </summary>
            /// <returns></returns>
            public Coordinate GetCurrentMenuCoordinate() => GetCursorPosition();

            /// <summary>
            /// Move the cursor to the battle option in the battle menu
            /// </summary>
            /// <returns>If the menu coordinates in memory match the expected location</returns>
            public bool NavigateToBattleOption() => NavigateMenu(BattleMenuCoordinate);

            /// <summary>
            /// Move the cursor to the Pokemon option in the battle menu
            /// </summary>
            /// <returns>If the menu coordinates in memory match the expected location</returns>
            public bool NavigateToPokemonOption() => NavigateMenu(PokemonMenuCoordinate);

            /// <summary>
            /// Move the cursor to the Pack option in the battle menu
            /// </summary>
            /// <returns>If the menu coordinates in memory match the expected location</returns>
            public bool NavigateToPackOption() => NavigateMenu(PackMenuCoordinate);

            /// <summary>
            /// Move the cursor to the Run option in the battle menu
            /// </summary>
            /// <returns>If the menu coordinates in memory match the expected location</returns>
            public bool NavigateToRunOption() => NavigateMenu(RunMenuCoordinate);
        }

        /// <summary>
        /// Menu object representing the battle menu
        /// </summary>
        public abstract IBattleMenu Menu { get; }

        internal abstract MemoryQuery PlayerTurnCounterQuery { get; }
        internal abstract MemoryQuery EnemyTurnCounterQuery { get; }

        /// <summary>
        /// Get the current number of turns the player has taken in battle
        /// </summary>
        /// <returns></returns>
        public int PlayerTurnCounter() => API.Memory.ReadInt(PlayerTurnCounterQuery);

        /// <summary>
        /// Get the current number of turns the enemy has taken in battle
        /// </summary>
        /// <returns></returns>
        public int EnemyTurnCounter() => API.Memory.ReadInt(EnemyTurnCounterQuery);

        public abstract void RunFromPokemon();
        public abstract void OpenPack();
        
        /// <summary>
        /// Wait for the battle menu to appear while pressing B
        /// </summary>
        /// <param name="maxFrames">Maximum number of frames to wait for the battle menu to appear</param>
        public void WaitForBattleMenu(int maxFrames = 1000)
        {
            Serilog.Log.Information("Waiting for the battle menu");
            int startingFrameCount = API.Emulator.GetFrameCount();
            while (API.Emulator.GetFrameCount() - startingFrameCount > maxFrames)
            {
                if (Menu.GetCursorPosition().Equals(Menu.BattleMenuCoordinate))
                {
                    return;
                }
                API.Input.Press([Button.B], duration: 2);
            }
            int elapsedFrames = API.Emulator.GetFrameCount() - startingFrameCount;
            throw new BattleException($"Timed out while waiting for Battle Menu. Elapsed frames: {elapsedFrames}");
        }

        public abstract CatchStatus WaitForCatch();
    }

    internal class Battle
    {
    }
}
