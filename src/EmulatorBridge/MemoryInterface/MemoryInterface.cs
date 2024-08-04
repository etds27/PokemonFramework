global using MemoryAddress = long;

using System;
using System.Collections.Generic;
using System.Net.Sockets;


namespace PokemonFramework.EmulatorBridge.MemoryInterface
{

    public readonly struct MemoryQuery
	{
		public MemoryQuery(long address, int size, MemoryDomain domain)
		{
			Address = address;
			Size = size;
			Domain = domain;
		}

		public readonly MemoryAddress Address;
		public readonly int Size;
		public readonly MemoryDomain Domain;
	}

	/// <summary>
	/// Mapping of Gameboy memory domain names to the starting point in the address space
	/// </summary>
	public enum MemoryDomain: MemoryAddress
    {
		ROM = 0x0000,
		VRAM = 0x8000,
		WRAM = 0xC000,
		OAM = 0xFE00,
		HRAM = 0xFF80
	}

	public abstract class IMemoryInterface
	{
        /// <summary>
        /// Reads data from the emulators memory
        /// </summary>
		/// <param name="address">Address of the data to read relative to the memory domain</param>
        /// <param name="size">Number of bytes to read from memory</param>
		/// <param name="domain">Region of memory to look in for datas</param>
        /// <returns>Array of bytes from the emulator</returns>
        public abstract IReadOnlyList<byte> Read(MemoryAddress address, int size, MemoryDomain domain);

		/// <summary>
		/// Reads a single byte from the emulators memory
		/// </summary>
		/// <param name="address">Address of the data to read relative to the memory domain</param>
		/// <param name="domain">Region of memory to look in for datas</param>
		/// <returns>Single byte of memory from the emulator</returns>
        public byte ReadByte(MemoryAddress address, MemoryDomain domain)
        {
            return Read(address: address, size: 1, domain: domain)[0];
        }

        /// <summary>
        /// Reads data from the emulators memory
        /// <param name="query">Struct containing information required to read memory data</param>
        /// <returns>Array of bytes from the emulator</returns>
        public IReadOnlyList<byte> Read(MemoryQuery query)
		{
			return Read(address: query.Address, size: query.Size, domain: query.Domain);
		}

        /// <summary>
        /// 
        /// Reads data from the emulators memory
        /// <param name="query">Struct containing information required to read memory data</param>
        /// <returns>Integer from the emulator</returns>
        public int ReadInt(MemoryQuery query)
		{
			return (int)Read(query: query)[0];
		}


        /// <summary>
        /// 
        /// Reads data from the emulators memory
        /// <param name="query">Struct containing information required to read memory data</param>
        /// <returns>Integer from the emulator</returns>
        public int ReadInt(MemoryAddress address, MemoryDomain domain)
        {
            return ReadInt(new MemoryQuery(address: address, size: 1, domain));
        }

        public abstract void Write(MemoryAddress address, IReadOnlyList<byte> data, MemoryDomain domain);
		public void WriteByte(long address, byte value, MemoryDomain domain)
		{
			Write(address: address, data: [value], domain: domain);
		}

		/// <summary>
		/// Takes the global memory address and adjusts it for the specified domain
		/// </summary>
		/// <param name="address">Global address within the memory space</param>
		/// <param name="domain">Domain to adjust the provided address to</param>
		/// <returns>The memory address within the domain specified</returns>
		public long GlobalAddressToDomainAddress(MemoryAddress address, MemoryDomain domain)
		{
            return address - ((MemoryAddress) domain);
		}

        /// <summary>
        /// Takes the domain memory address and adjusts it to the global address space
        /// </summary>
        /// <param name="address">Domain address within the memory space</param>
        /// <param name="domain">Domain to adjust the provided address from</param>
        /// <returns></returns>
        public long DomainAddressToGlobalAddress(MemoryAddress address, MemoryDomain domain)
		{
			return ((MemoryAddress) domain) | address;
		}
	}
}
