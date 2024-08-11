using BizHawk.Client.Common;
using PokemonFramework.EmulatorBridge.APIContainer;
using System;

namespace PokemonFramework.EmulatorBridge.EmulatorInterface
{
    public class BizHawkEmulatorClient : IEmulatorClientInterface
    {
        private ApiContainer api => BizHawkAPI.API;
        public override void AdvanceFrame()
        {
            api.EmuClient.DoFrameAdvance();
        }

        public override int GetFrameCount() => api.Emulation.FrameCount();

        public override bool IsPaused() => api.EmuClient.IsPaused();

        public override void LoadState(string statePath)
        {
            Serilog.Log.Information("Loading Save State from path: {State}", statePath);
            try
            {
                api.EmuClient.LoadState(statePath);
            } catch
            {
                Serilog.Log.Error("Unable to Load Save state");
                return;
            }
            Serilog.Log.Information("Loaded Save State from path: {State}", statePath);
            AdvanceFrame();
        }

        public override void Pause()
        {
            Serilog.Log.Information("Pausing Emulator");
            api.EmuClient.Pause();
        }

        public override void Resume()
        {
            Serilog.Log.Information("Resuming Emulator");
            api.EmuClient.Unpause();
        }

        public override void SaveState(string statePath)
        {
            Serilog.Log.Information("Saving current state to path: {State}", statePath);
            api.EmuClient.SaveState(statePath);
            Serilog.Log.Information("Saved current state to path: {State}", statePath);
        }
    }
}
