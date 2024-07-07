using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace PokemonFramework.EmulatorBridge.MemoryInterface
{
	public readonly struct MemoryQuery
	{
		public readonly long Address;
		public readonly int Size;
		public readonly MemoryDomain Domain;
	}

	/// <summary>
	/// Mapping of Gameboy memory domain names to the starting point in the address space
	/// </summary>
	public enum MemoryDomain: long
	{
		ROM = 0x0000,
		VRAM = 0x8000,
		WRAM = 0xC000,
		OAM = 0xFE00,
		HRAM = 0xFF80
	}

	public abstract class IMemoryInterface
	{
		public abstract List<byte> Read(long address, int size, MemoryDomain domain);
        public abstract byte ReadByte(long address, MemoryDomain domain);

		public List<byte> Read(MemoryQuery query)
		{
			return Read(address: query.Address, size: query.Size, domain: query.Domain);
		}

		/// <summary>
		/// Takes the global memory address and adjusts it for the specified domain
		/// </summary>
		/// <param name="address">Global address within the memory space</param>
		/// <param name="domain">Domain to adjust the provided address to</param>
		/// <returns>The memory address within the domain specified</returns>
		public long GlobalAddressToDomainAddress(long address, MemoryDomain domain)
		{
            return address - ((long) domain);
		}

        /// <summary>
        /// Takes the domain memory address and adjusts it to the global address space
        /// </summary>
        /// <param name="address">Domain address within the memory space</param>
        /// <param name="domain">Domain to adjust the provided address from</param>
        /// <returns></returns>
        public long DomainAddressToGlobalAddress(long address, MemoryDomain domain)
		{
			return ((long) domain) | address;
		}
	}
}
