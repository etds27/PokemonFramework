using System.Collections.Generic;

namespace PokemonFramework.EmulatorBridge.InputInterface
{

    public readonly struct InputAction
    {
        public InputAction(IReadOnlyList<Button> buttons, int duration = 1, int waitFrames = 0)
        {
            Buttons = buttons;
            Duration = duration;
            WaitFrames = waitFrames;
        }
        public readonly IReadOnlyList<Button> Buttons;
        public readonly int Duration;
        public readonly int WaitFrames;
    }

    public enum Button
    {
        A,
        B,
        DOWN,
        LEFT,
        RIGHT,
        UP,
        POWER,
        START,
        SELECT
    }

    public abstract class IInputInterface
    {
        /// <summary>
        /// Perform the specified input defined by the InputAction parameter
        /// </summary>
        /// <param name="action">Button and press duration information for the input action</param>
        public abstract void PerformInputAction(InputAction action);

        /// <summary>
        /// Press a collection of buttons for a single frame
        /// </summary>
        /// <param name="buttons">Buttons to be pressed</param>
        public void Set(IReadOnlyList<Button> buttons)
        {
            PerformInputAction(action: new InputAction(buttons: buttons, duration: 1, waitFrames: 0));
        }

        /// <summary>
        /// Press a collection of buttons for a specified amount of frames
        /// </summary>
        /// <param name="buttons">Buttons to be pressed</param>
        /// <param name="duration">Duration of frames to hold the buttons down</param>
        /// <param name="waitFrames">Duration of frames to wait after releasing buttons</param>
        public void Press(IReadOnlyList<Button> buttons, int duration, int waitFrames = 0)
        {
            PerformInputAction(action: new InputAction(buttons: buttons, duration: duration, waitFrames: waitFrames));
        }

        public abstract void ButtonSequence(IReadOnlyList<InputAction> actions, int waitStart = 0, int waitEnd = 0);
    }
}
