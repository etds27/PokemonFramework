using BizHawk.Client.Common;
using PokemonFramework.EmulatorBridge.APIContainer;

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
            Serilog.Log.Information("Loading Save State from path: {State}", statePath);
            api.EmuClient.LoadState(statePath);
            Serilog.Log.Information("Loaded Save State from path: {State}", statePath);

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
            Serilog.Log.Information("Saving current state to path: {State}", statePath);
            api.EmuClient.SaveState(statePath);
            Serilog.Log.Information("Saved current state to path: {State}", statePath);
        }
    }
}
