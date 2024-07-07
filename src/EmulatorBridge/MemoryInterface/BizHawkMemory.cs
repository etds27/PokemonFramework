using System;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using BizHawk.Client.Common;
using System.Collections.Generic;
using System.Linq;
using PokemonFramework.EmulatorBridge.APIContainer;


namespace PokemonFramework.EmulatorBridge.MemoryInterface {
    public class BizHawkMemory : IMemoryInterface
    {
        private ApiContainer api => BizHawkAPI.API;

        public override List<byte> Read(long address, int size, MemoryDomain domain)
        {
            IReadOnlyCollection<byte> memoryRange = api.Memory.ReadByteRange(addr: address, length: size, domain: nameof(domain));
            // IReadOnlyCollection<byte> memoryRange = new List<byte> { 56, 56, 56};
            return memoryRange.ToList();
        }

        public override byte ReadByte(long address, MemoryDomain domain)
        {
            return Read(address: address, size: 1, domain: domain)[0];
        }
    }
}
