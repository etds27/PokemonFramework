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
    internal abstract class TopLevelModule : FrameworkModule
    {
        /// <summary>
        /// Provides the current game for all top level modules in the POM
        /// </summary>
        internal IGame CurrentGame => PokemonGame.GetCurrentGame();
    }
}
