using PokemonFramework.Framework.Models.Game;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Models.Module
{
    /// <summary>
    /// Abstract class to hold necessary attributes for each class within the framework
    /// </summary>
    public abstract class TopLevelModule<T1, T2> : FrameworkModule
    {
        static TopLevelModule()
        {
            if ((GameConfigMap ?? []).TryGetValue(CurrentGame, out T2 tempObject))
            {
                if (tempObject != null)
                {
                    StaticModelInstance = tempObject;
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

        /// <summary>
        /// Provides the current game for all top level modules in the POM
        /// </summary>
        internal static IGame CurrentGame => PokemonGame.GetCurrentGame();
        internal static Dictionary<IGame, T1> GameConstructorMap { get; set; }
        internal static Dictionary<IGame, T2> GameConfigMap { get; set; }
        internal static T2 StaticModelInstance { get; set; }


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
