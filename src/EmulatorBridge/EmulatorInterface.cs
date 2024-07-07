using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;

namespace PokemonFramework.EmulatorBridge
{
    /// <summary>
    /// This details all API of the bridges between the pokemon framework and the emulators used
    /// </summary>
    public interface IEmulatorInterface
    {
        public IMemoryInterface Memory
        {
            get;
        }

    }
}