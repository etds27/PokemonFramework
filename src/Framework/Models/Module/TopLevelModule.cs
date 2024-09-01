using PokemonFramework.Framework.Models.Game;
using System;
using System.Collections.Generic;

namespace PokemonFramework.Framework.Models.Module
{
    /// <summary>
    /// Abstract class to hold necessary attributes for each class within the framework
    /// </summary>
    /// <typeparam name="T1">Constructor for the object</typeparam>
    /// <typeparam name="T2">Generic config class</typeparam>
    public abstract class TopLevelModule<T1, T2> : FrameworkModule
    {
        public TopLevelModule()
        {
            if ((GameConfigMap ?? []).TryGetValue(CurrentGame, out T2 tempObject))
            {
                if (tempObject != null)
                {
                    ConfigInstance = tempObject;
                }
                else
                {
                    throw new SubModuleNotImplemented();
                }
            }
            else
            {
                Serilog.Log.Error("Unable to find config for {Game} in {GameMap}", CurrentGame.Name, GameConfigMap);
                throw new SubModuleNotFoundException();
            }
        }

        /// <summary>
        /// Provides the current game for all top level modules in the POM
        /// </summary>
        internal static IGame CurrentGame => PokemonGame.GetCurrentGame();
        internal abstract Dictionary<IGame, T1> GameConstructorMap { get; }
        internal virtual Dictionary<IGame, T2> GameConfigMap { get; }
        internal T2 ConfigInstance { get; set; }


        /// <summary>
        /// Exception for when the current game does not exist in the GameObjectMap and the submodule is undefined
        /// </summary>
        public class SubModuleNotFoundException : Exception
        {
            public SubModuleNotFoundException() { }
            public SubModuleNotFoundException(string message) : base(message) { }
            public SubModuleNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        }

        public class SubModuleNotImplemented : Exception
        {
            public SubModuleNotImplemented() { }
            public SubModuleNotImplemented(string message) : base(message) { }
            public SubModuleNotImplemented(string message, Exception innerException) : base(message, innerException) { }
        }
    }
}
