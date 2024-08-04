using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Models.Bag.Config;
using PokemonFramework.Framework.Models.Bag.Object;
using PokemonFramework.Framework.Models.Game;
using PokemonFramework.Framework.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonFramework.Framework.Models.Bag
{
    using Constructor = Func<IBagObject>;

    public abstract class IBagObject : FrameworkObject
    {
        /// <summary>
        /// Factory used by the sub class to create the expected pockets in the bag
        /// </summary>
        internal PocketFactory pocketFactory = new();

        /// <summary>
        /// Query to read the current pocket type
        /// </summary>
        internal abstract MemoryQuery CurrentPocketQuery { get; }

        /// <summary>
        /// Query to read the current selected item
        /// </summary>
        internal abstract MemoryQuery SelectedItemQuery {  get; }


        /// <summary>
        /// Collection of the pockets that the bag has for the current game
        /// </summary>
        internal abstract IReadOnlyList<IPocketObject> Pockets
        {
            get;
        }


        public abstract bool NavigateToPocket(PocketType pocket);
        
        public abstract bool UseItem(PocketType pocket, Item.Item item);
        public abstract bool UseBestBall();


        public bool NavigateToItem(PocketType pocket, Item.Item item)
        {
            IPocketObject pocketObject = GetPocket(pocket);
            if (!HasPocket(pocket)) return false;
            if (!pocketObject.ContainsItem(item)) return false;
            if (!NavigateToPocket(pocket)) return false;
            return GetPocket(pocket).NavigateToItem(item);
        }

        /// <summary>
        /// Get the current open pocket in the bag
        /// </summary>
        /// <returns>Current pocket</returns>
        internal IPocketObject GetCurrentPocket()
        {
            int currentValue = API.Memory.ReadInt(CurrentPocketQuery);
            return Pockets.First(x => x.Value == currentValue);
        }

        /// <summary>
        /// Get the current pocket type
        /// </summary>
        /// <returns>Current pocket type</returns>
        public PocketType GetCurrentPocketType() => GetCurrentPocket().PocketType;

        /// <summary>
        /// Get the specified pocket for the current bag
        /// </summary>
        /// <param name="pocket">Type of pocket to fetch</param>
        /// <returns>Pocket object of specified type</returns>
        internal IPocketObject GetPocket(PocketType pocket) => Pockets.First(x => x.PocketType == pocket);

        /// <summary>
        /// Determine if the bag has a specific pocket
        /// </summary>
        /// <param name="pocket">Type of pocket to check for</param>
        /// <returns>If the pocket is present in the bag</returns>
        public bool HasPocket(PocketType pocket) => Pockets.Any(x => x.PocketType == pocket);

        /// <summary>
        /// Current selected item in the bag
        /// </summary>
        /// <returns>Current selected item in the bag</returns>
        public Item.Item SelectedItem() => new (API.Memory.Read(SelectedItemQuery)[0]); 
    }

    public interface IBagConfig
    {

    }

    public class BagFactory : TopLevelModule<IBagObject, IBagConfig>, IBagConfig
    {
        internal new static Dictionary<IGame, IBagConfig> GameConfigMap = new()
        {
            { PokemonGame.GOLD, new BagConfigGoldSilver() },
            { PokemonGame.SILVER, new BagConfigGoldSilver() },
            { PokemonGame.CRYSTAL, new BagConfigCrystal() }
        };

        internal new static Dictionary<IGame, Constructor> GameConstructorMap = new()
        {
            { PokemonGame.GOLD, () => new BagGoldSilver() },
            { PokemonGame.SILVER, () => new BagGoldSilver() },
            { PokemonGame.CRYSTAL, () => new BagGoldSilver() }
        };

        public IBagObject CreateObject()
        {
            if ((GameConstructorMap ?? []).TryGetValue(CurrentGame, out Constructor tempObject))
            {
                if (tempObject != null)
                {
                    return tempObject();
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
