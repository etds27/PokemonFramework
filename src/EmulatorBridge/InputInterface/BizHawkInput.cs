using BizHawk.Client.Common;
using PokemonFramework.EmulatorBridge.APIContainer;
using PokemonFramework.EmulatorBridge.EmulatorInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.EmulatorBridge.InputInterface
{
    public class BizHawkInput : IInputInterface
    {
        private ApiContainer api => BizHawkAPI.API;
        private BizHawkEmulatorClient _BizHawkEmulator = new();

        public override void ButtonSequence(IReadOnlyList<InputAction> actions, int waitStart = 0, int waitEnd = 0)
        {
            _BizHawkEmulator.AdvanceFrames(frames: waitStart);
            foreach (var action in actions)
            {
                PerformInputAction(action: action);
            }
            _BizHawkEmulator.AdvanceFrames(frames: waitStart);
        }

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

            _BizHawkEmulator.AdvanceFrames(action.WaitFrames);
        }
    }
}
