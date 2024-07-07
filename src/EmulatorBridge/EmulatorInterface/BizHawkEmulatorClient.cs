using BizHawk.Client.Common;
using PokemonFramework.EmulatorBridge.APIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.EmulatorBridge.EmulatorInterface
{
    public class BizHawkEmulatorClient : IEmulatorClientInterface
    {
        private ApiContainer api => BizHawkAPI.API;
        public override void AdvanceFrame()
        {
            api.EmuClient.DoFrameAdvance();
        }

        public override void Pause()
        {
            api.EmuClient.Pause();
        }

        public override void Resume()
        {
            api.EmuClient.Unpause();
        }
    }
}
