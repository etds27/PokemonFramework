﻿using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Module;
using PokemonFramework.Framework.Pokemon;
using PokemonFramework.Framework.Pokemon.PokemonConfig;
using System;
using System.Collections.Generic;

namespace PokemonFramework.Framework.Party.PartyObject
{
    internal class PartyCrystal : IPartyObject
    {
        internal override MemoryQuery NumPokemonInPartyQuery
        {
            get {
                return new MemoryQuery(address: 0xDCD7, size: 1, domain: MemoryDomain.WRAM);
            }
        }

        internal override MemoryAddress StartingPokemonAddress
        {
            get
            {
                return 0xDCD7 + 0x8;
            }
        }

        public override bool NavigateToPokemon(int index)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
