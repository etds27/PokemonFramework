using PokemonFramework.EmulatorBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Module
{
    internal abstract class FrameworkModule
    {
        /// <summary>
        /// Provides API access for all modules in the PokemonFramework
        /// </summary>
        internal IEmulatorInterface API = new BizHawkEmulatorBridge();
    }
}
