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
