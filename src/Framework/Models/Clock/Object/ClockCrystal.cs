using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Models.Clock.Object
{
    public class ClockCrystal : IClockObject
    {
        // TODO: This Day query is not correct
        internal override MemoryQuery DayQuery => new(address: 0x14, size: 1, domain: MemoryDomain.HRAM);

        internal override MemoryQuery HourQuery => new(address: 0x14, size: 1, domain: MemoryDomain.HRAM);

        internal override MemoryQuery MinuteQuery => new(address: 0x16, size: 1, domain: MemoryDomain.HRAM);

        internal override MemoryQuery SecondQuery => new(address: 0x18, size: 1, domain: MemoryDomain.HRAM);
    }
}
