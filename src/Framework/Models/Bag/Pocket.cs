using PokemonFramework.Framework.Models.Item;
using PokemonFramework.Framework.Models.Bag.Object;
using PokemonFramework.Framework.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PokemonFramework.Framework.Models.Game;
using PokemonFramework.Framework.Models.Party;
using PokemonFramework.EmulatorBridge.MemoryInterface;


namespace PokemonFramework.Framework.Models.Bag
{
    using Constructor = Func<PocketType, MemoryAddress, int, IPocketObject>;

    internal abstract class IPocketObject : FrameworkObject
    {
        internal IPocketObject(PocketType pocketType, MemoryAddress memoryAddress, int value) : base()
        { 
            PocketType = pocketType;
            StartAddress = memoryAddress;
            Value = value;
            NumberOfItemsInPocketQuery = new(memoryAddress, 1, MemoryDomain.WRAM);
        }

        internal PocketType PocketType;
        internal MemoryAddress StartAddress;
        internal int Value;
        internal MemoryQuery NumberOfItemsInPocketQuery;
        internal int NumberOfItemsInPocket => API.Memory.ReadInt(NumberOfItemsInPocketQuery);

        internal abstract MemoryQuery CurrentCursorPosition { get; }
        internal abstract int MaxPocketSize { get; }


        /// <summary>
        /// Given an index within the pocket, determine what the corresponding Memory Address would be
        /// </summary>
        /// <param name="index">Index of item in the pocket</param>
        /// <returns>Memory address of item at index</returns>
        internal abstract MemoryAddress CalculateItemBallAddress(int index);
        /// <summary>
        /// Given an index within the pocket, determine what the corresponding Memory Address would be
        /// </summary>
        /// <param name="index">Index of item in the pocket</param>
        /// <returns>Memory address of item at index</returns>
        internal abstract MemoryAddress CalculateKeyAddress(int index);

        /// <summary>
        /// Get the memory address of a specified item
        /// </summary>
        /// <param name="item">Item to get the location for</param>
        /// <returns>Address of the item</returns>
        internal abstract MemoryAddress GetItemLocation(Item.Item item);

        /// <summary>
        /// Determine if the specified Item is in the pocket
        /// </summary>
        /// <param name="item">Item to check against</param>
        /// <returns>If the item is present</returns>
        internal abstract bool ContainsItem(Item.Item item);

        /// <summary>
        /// Get the index of the item within the pocket
        /// </summary>
        /// <param name="item">Item to get the index for</param>
        /// <returns>Index of the item</returns>
        internal abstract int IndexOfItem(Item.Item item);

        /// <summary>
        /// Get the quantity of the specified item
        /// </summary>
        /// <param name="item">Item to retrieve quantity of</param>
        /// <returns>Quantity of item</returns>
        internal abstract int GetItemQuantity(Item.Item item);

        internal abstract bool NavigateToItem(Item.Item item);
    }

    public enum PocketType
    {
        Item,
        Ball,
        KeyItem,
        TMHM
    }

    internal class PocketFactory : TopLevelModule<IPocketObject, object>
    {
        internal new static Dictionary<IGame, Constructor> GameConstructorMap = new()
        {
            { PokemonGame.GOLD, (pocketType, memoryAddress, value) => new PocketGoldSilver(pocketType, memoryAddress, value) },
            { PokemonGame.SILVER, (pocketType, memoryAddress, value) => new PocketGoldSilver(pocketType, memoryAddress, value) },
            { PokemonGame.CRYSTAL, (pocketType, memoryAddress, value) => new PocketCrystal(pocketType, memoryAddress, value) }
        };

        internal IPocketObject CreateObject(PocketType pocketType, MemoryAddress startAddress, int value)
        {
            if ((GameConstructorMap ?? []).TryGetValue(CurrentGame, out Constructor tempObject))
            {
                if (tempObject != null)
                {
                    return tempObject(pocketType, startAddress, value);
                }
                else
                {
                    throw new SubModuleNotImplemented();
                }
            }
            else
            {
                throw new SubModuleNotFoundException();
            }
        }
    }
}
