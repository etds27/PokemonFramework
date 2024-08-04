using PokemonFramework.EmulatorBridge.EmulatorInterface;
using PokemonFramework.EmulatorBridge.GameInterface;
using PokemonFramework.EmulatorBridge.InputInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;

namespace PokemonFramework.EmulatorBridge
{
    public class BizHawkEmulatorBridge : IEmulatorInterface
    {
        private readonly BizHawkMemory _memoryModule = new();
        public IMemoryInterface Memory { get => _memoryModule; }

        private readonly BizHawkInput _inputModule = new();
        public IInputInterface Input { get => _inputModule; }

        private readonly BizHawkEmulatorClient _clientModule = new();
        public IEmulatorClientInterface Emulator { get => _clientModule; }

        private readonly BizHawkGame _gameModule = new();
        public IGameInterface Game { get => _gameModule; }
    }
}
