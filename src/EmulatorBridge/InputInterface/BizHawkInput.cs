using BizHawk.Client.Common;
using PokemonFramework.EmulatorBridge.APIContainer;
using PokemonFramework.EmulatorBridge.EmulatorInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PokemonFramework.EmulatorBridge.InputInterface
{
    public class BizHawkInput : IInputInterface
    {
        private ApiContainer API => BizHawkAPI.API;

        internal override IMemoryInterface MemoryInterface => new BizHawkMemory();

        internal override IEmulatorClientInterface EmulatorClientInterface => new BizHawkEmulatorClient();

        public override void PerformInputAction(InputAction action)
        {
            SetButtons(action.Buttons);
            Serilog.Log.Information("Joypad Button Mask {ButtonMask}", action.Buttons.ToList().Select(b => b.Name));
            for (int i = 0; i < action.Duration; i++)
            {

                EmulatorClientInterface.AdvanceFrame();
            }
            ClearButtons();

            EmulatorClientInterface.AdvanceFrames(action.WaitFrames);
        }

        private void SetButtons(IReadOnlyCollection<Button> buttons)
        {
            foreach (Button button in Button.AllButtons)
            {;
                if (buttons.Any(b => b == button))
                {
                    // buttonMask.Add(Enum.GetName(typeof(Button), button), true);
                    API.Joypad.Set(button.Name, true);
                }
                else
                {
                    // buttonMask.Add(, false);
                    API.Joypad.Set(button.Name, false);
                }
            }
        }

        private void ClearButtons() => SetButtons(buttons: []);
    }
}
