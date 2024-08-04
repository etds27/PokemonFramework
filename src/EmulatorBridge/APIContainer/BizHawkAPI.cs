using BizHawk.Client.Common;

namespace PokemonFramework.EmulatorBridge.APIContainer
{
    /// <summary>
    /// Static class to hold the container of APIs provided from the BizHawk Emulator
    /// </summary>
    internal class BizHawkAPI
    {
        public static ApiContainer? _APIContainer { get; set; }

        public static ApiContainer API => _APIContainer!;

        public static void SetAPIContainer(ApiContainer apiContainer)
        {
            _APIContainer = apiContainer;
        }
    }
}
