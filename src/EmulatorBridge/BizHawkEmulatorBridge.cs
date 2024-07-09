using PokemonFramework.EmulatorBridge.EmulatorInterface;
using PokemonFramework.EmulatorBridge.GameInterface;
using PokemonFramework.EmulatorBridge.InputInterface;
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

        private BizHawkInput _inputModule = new();
        public IInputInterface Input { get => _inputModule; }

        private BizHawkEmulatorClient _clientModule = new();
        public IEmulatorClientInterface Emulator { get => _clientModule; }

        private BizHawkGame _gameModule = new();
        public IGameInterface Game { get => _gameModule; }
    }
}
