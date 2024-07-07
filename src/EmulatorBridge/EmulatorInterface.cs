﻿using PokemonFramework.EmulatorBridge.EmulatorInterface;
using PokemonFramework.EmulatorBridge.InputInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;

namespace PokemonFramework.EmulatorBridge
{
    /// <summary>
    /// This details all API of the bridges between the pokemon framework and the emulators used
    /// </summary>
    public interface IEmulatorInterface
    {
        public IMemoryInterface Memory
        {
            get;
        }

        public IInputInterface Input
        {
            get;
        }

        public IEmulatorClientInterface Emulator
        {
            get;
        }

    }
}