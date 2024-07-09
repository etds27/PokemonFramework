using PokemonFramework.Framework.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Module
{
    /// <summary>
    /// Abstract class to hold necessary attributes for each class within the framework
    /// </summary>
    public abstract class TopLevelModule<T> : FrameworkModule
    {
        static TopLevelModule()
        {
            GameObjectMap = [];
        }

        /// <summary>
        /// Instantiates the submodule using the GameObjectMap defined in the subclass
        /// </summary>
        /// <exception cref="SubModuleNotFoundException">Thrown if the submodule/game aren't defined in the GameObjectMap</exception>
        public TopLevelModule()
        {
            if (GameObjectMap.TryGetValue(CurrentGame, out T tempObject))
            {
                ModelObject = tempObject;
            }
            else
            {
                throw new SubModuleNotFoundException();
            }
        }

        /// <summary>
        /// Provides the current game for all top level modules in the POM
        /// </summary>
        internal IGame CurrentGame => PokemonGame.GetCurrentGame();
        internal static Dictionary<IGame, T> GameObjectMap { get; set; }
        internal T ModelObject { get; set; }

        /// <summary>
        /// Exception for when the current game does not exist in the GameObjectMap and the submodule is undefined
        /// </summary>
        public class SubModuleNotFoundException : Exception {
            public SubModuleNotFoundException() { }
            public SubModuleNotFoundException(string message) : base(message) { }
            public SubModuleNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        }
    }
}
