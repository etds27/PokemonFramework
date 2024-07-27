using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PokemonFramework.EmulatorBridge.EmulatorInterface
{
    public abstract class IEmulatorClientInterface
    {
        public abstract void AdvanceFrame();

        public void AdvanceFrames(int frames = 1)
        {
            for (int i = 0; i < frames; i++)
            {
                AdvanceFrame();
            }
        }

        public abstract void Pause();

        /// <summary>
        /// Pauses the emulator for a specified amount of time
        /// </summary>
        /// <param name="duration">Duration to pause the emulator for in milliseconds</param>
        public void Pause(int duration)
        {
            Pause();
            Thread.Sleep(duration);
            Resume();
        }

        public abstract void Resume();

        /// <summary>
        /// Unpauses the emulator for a specified amount of time
        /// </summary>
        /// <param name="duration">Duration to unpause the emulator for in milliseconds</param>
        public void Resume(int duration)
        {
            Resume();
            Thread.Sleep(duration);
            Pause();
        }

        public abstract bool IsPaused();
        public bool IsRunning()
        {
            return !IsPaused();
        }

        public abstract void LoadState(String statePath);
        public abstract void SaveState(String statePath);
    }
}
