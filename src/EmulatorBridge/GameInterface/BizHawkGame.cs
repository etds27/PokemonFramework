using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using PokemonFramework.EmulatorBridge.APIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.EmulatorBridge.GameInterface
{
    public class BizHawkGame : IGameInterface
    {
        private ApiContainer api => BizHawkAPI.API;

        public override string GetRomName()
        {
            return api.Emulation.GetGameInfo()!.Name;
        }

        public override string GetRomHash()
        {
            return api.Emulation.GetGameInfo()!.Hash;
        }
    }
}
