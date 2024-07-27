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

        public override bool IsPaused()
        {
            return api.EmuClient.IsPaused();
        }

        public override void LoadState(string statePath)
        {
            api.EmuClient.LoadState(statePath);
        }

        public override void Pause()
        {
            api.EmuClient.Pause();
        }

        public override void Resume()
        {
            api.EmuClient.Unpause();
        }

        public override void SaveState(string statePath)
        {
            api.EmuClient.SaveState(statePath);
        }
    }
}
