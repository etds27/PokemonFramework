using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Models.Bag.Object
{
    internal class PocketCrystal : IPocketObject
    {
        public PocketCrystal(PocketType pocketType, long memoryAddress, int value) : base(pocketType, memoryAddress, value)
        {
        }

        internal override MemoryQuery CurrentCursorPosition => throw new NotImplementedException();

        internal override int MaxPocketSize => 40;

        internal override bool ContainsItem(Item.Item item) => IndexOfItem(item) != -1;
        internal override MemoryAddress GetItemLocation(Item.Item item) => CalculateItemBallAddress(IndexOfItem(item));

        internal override int IndexOfItem(Item.Item item)
        {
            // The memory layout of the Ball and Item packs are different than Key and TM/HMs
            // Each item in the Ball or Item pocket has a item identifier and a quantity
            // Each item in the Key pocket only has an identifier
            // The current TM/HMs are represented by a mask that contains the quantity
            if (PocketType == PocketType.Ball || PocketType == PocketType.Item)
            {
                for (int i = 0; i < NumberOfItemsInPocket; i++)
                {
                    MemoryAddress address = CalculateItemBallAddress(i);
                    if (API.Memory.ReadByte(address, MemoryDomain.WRAM) == item.Idenitifer) return i;
                }
            }
            else if (PocketType == PocketType.KeyItem)
            {

            }
            else if (PocketType == PocketType.TMHM)
            {
                throw new NotImplementedException();
            }
            return -1;
        }

        internal override int GetItemQuantity(Item.Item item)
        {
            if (!ContainsItem(item)) { return 0; }
            // Key items have no quantity other than 1 or 0
            if (PocketType == PocketType.KeyItem) return 1;
            if (PocketType != PocketType.TMHM) { throw new NotImplementedException(); }
            MemoryAddress quantityAddress = GetItemLocation(item) + 1;
            return API.Memory.ReadInt(quantityAddress, MemoryDomain.WRAM);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        internal override bool NavigateToItem(Item.Item item)
        {
            throw new NotImplementedException();
        }

        internal override long CalculateItemBallAddress(int index)
        {
            return StartAddress + 2 * index - 1;
        }

        internal override long CalculateKeyAddress(int index)
        {
            return StartAddress + index;
        }
    }
}
