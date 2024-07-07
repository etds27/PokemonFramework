using BizHawk.Client.Common;
using PokemonFramework.EmulatorBridge.APIContainer;
using PokemonFramework.EmulatorBridge.EmulatorInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonFramework.EmulatorBridge.InputInterface
{
    public class BizHawkInput : IInputInterface
    {
        private ApiContainer api => BizHawkAPI.API;

        internal override IMemoryInterface MemoryInterface => new BizHawkMemory();

        internal override IEmulatorClientInterface EmulatorClientInterface => new BizHawkEmulatorClient();    

        public override void PerformInputAction(InputAction action)
        {
            Dictionary<string, bool> buttonMask = new Dictionary<string, bool>();
            foreach (Button button in Enum.GetValues(typeof(Button))) {
                if (action.Buttons.Any(b => b == button))
                {
                    buttonMask.Add(nameof(button), true);
                } else
                {
                    buttonMask.Add(nameof(button), false);
                }
            }

            for (int i = 0; i < action.Duration; i++)
            {
                api.Joypad.Set(buttons: buttonMask);
            }

            EmulatorClientInterface.AdvanceFrames(action.WaitFrames);
        }
    }
}
