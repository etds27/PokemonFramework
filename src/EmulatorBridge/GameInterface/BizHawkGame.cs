using BizHawk.Client.Common;
using PokemonFramework.EmulatorBridge.APIContainer;

namespace PokemonFramework.EmulatorBridge.GameInterface
{
    public class BizHawkGame : IGameInterface
    {
        private ApiContainer API => BizHawkAPI.API;

        public override string GetRomName()
        {
            return API.Emulation.GetGameInfo()!.Name;
        }

        public override string GetRomHash()
        {
            return API.Emulation.GetGameInfo()!.Hash;
        }
    }
}
