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

        public override IReadOnlyList<byte> Read(long address, int size, MemoryDomain domain)
        {
            IReadOnlyCollection<byte> memoryRange = api.Memory.ReadByteRange(addr: address, length: size, domain: nameof(domain));
            return memoryRange.ToList();
        }

        public override void Write(long address, IReadOnlyList<byte> data, MemoryDomain domain)
        {
            api.Memory.WriteByteRange(addr: address, memoryblock: data, domain: nameof(domain));
        }
    }
}
