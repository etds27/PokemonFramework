using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.EmulatorBridge
{
    public class BizHawkEmulatorBridge : IEmulatorInterface
    {
        private BizHawkMemory _memoryModule = new();
        public IMemoryInterface Memory { get => _memoryModule; }
    }
}
