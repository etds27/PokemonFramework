using PokemonFramework.EmulatorBridge.EmulatorInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public InputAction(IReadOnlyList<Button> buttons, InputOptions options)
        {
            Buttons = buttons;
            Duration = options.Duration;
            WaitFrames = options.WaitFrames;
        }

        public readonly IReadOnlyList<Button> Buttons;
        public readonly int Duration;
        public readonly int WaitFrames;
    }

    public readonly struct InputOptions(int duration, int waitFrames)
    {
        public readonly int Duration = duration;
        public readonly int WaitFrames = waitFrames;
    }

    public sealed class Button()
    {
        public readonly string Name = "";

        private Button(string name) : this()
        {
            Name = name;
        }

        public static readonly Button A = new("A");
        public static readonly Button B = new("B");
        public static readonly Button DOWN = new("Down");
        public static readonly Button UP = new("Up");
        public static readonly Button LEFT = new("Left");
        public static readonly Button RIGHT = new("Right");
        public static readonly Button POWER = new("Power");
        public static readonly Button START = new("Start");
        public static readonly Button SELECT = new("Select");

        /// <summary>
        /// To be used to iterate over all allowable buttons
        /// </summary>
        public static IReadOnlyCollection<Button> AllButtons = [
            Button.A, 
            Button.B, 
            Button.DOWN, 
            Button.UP,     
            Button.LEFT, 
            Button.RIGHT, 
            Button.POWER,
            Button.START, 
            Button.SELECT
            ];
    }

    public abstract class IInputInterface
    {
        internal abstract IMemoryInterface MemoryInterface { get; }
        internal abstract IEmulatorClientInterface EmulatorClientInterface { get; } 


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

        /// <summary>
        /// Perform a sequence of actions
        /// </summary>
        /// <param name="actions">List of actions to perform in order</param>
        /// <param name="waitStart">Frames to wait until the initial sequence starts</param>
        /// <param name="waitEnd">Frames to wait after the sequence has ended</param>
        public void InputSequence(IReadOnlyList<InputAction> actions, int waitStart = 0, int waitEnd = 0)
        {
            EmulatorClientInterface.AdvanceFrames(frames: waitStart);
            foreach (var action in actions)
            {
                PerformInputAction(action: action);
            }
            EmulatorClientInterface.AdvanceFrames(frames: waitEnd);
        }

        /// <summary>
        /// Perform the action repeatedly until the memory address returns the correct information
        /// </summary>
        /// <param name="action">Action to perform repeatedly</param>
        /// <param name="memoryQuery">Memory query to observe verification data</param>
        /// <param name="expectedData">Expected data to be in memory query</param>
        /// <param name="timeout">Total time to wait for memory to change to correct value</param>
        /// <returns></returns>
        public bool PressUntil(InputAction action, MemoryQuery memoryQuery, IReadOnlyList<byte> expectedData, int timeout = 5000) {
            DateTime end = DateTime.Now.AddMilliseconds(timeout);
            while (DateTime.Now < end)
            {
                if (MemoryInterface.Read(memoryQuery) == expectedData)
                {
                    return true;
                }
                PerformInputAction(action);
            }
            return false;
        }

        /// <summary>
        /// Perform the action repeatedly until the values at the memory address have changed
        /// </summary>
        /// <param name="action">Action to perform repeatedly</param>
        /// <param name="memoryQuery">Memory query to observe verification data</param>
        /// <param name="timeout">Total time to wait for memory to change to correct value</param>
        /// <returns></returns>
        public bool PressUntilMemoryChanges(InputAction action, MemoryQuery memoryQuery, int timeout = 5000)
        {
            DateTime end = DateTime.Now.AddMilliseconds(timeout);
            byte[] startingData = MemoryInterface.Read(memoryQuery).ToArray();
            while (DateTime.Now < end)
            {
                if (MemoryInterface.Read(memoryQuery) != startingData)
                {
                    return true;
                }
                PerformInputAction(action);
            }
            return false;
        }
    }
}
