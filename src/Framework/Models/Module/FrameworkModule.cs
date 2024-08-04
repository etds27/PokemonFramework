using PokemonFramework.EmulatorBridge;

namespace PokemonFramework.Framework.Models.Module
{
    public abstract class FrameworkModule
    {
        /// <summary>
        /// Provides API access for all modules in the PokemonFramework
        /// </summary>
        internal IEmulatorInterface API = new BizHawkEmulatorBridge();
    }
}
