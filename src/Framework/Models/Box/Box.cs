using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Models.Game;
using PokemonFramework.Framework.Models.Menu;
using PokemonFramework.Framework.Models.Module;
using PokemonFramework.Framework.Models.Pokemon;
using PokemonFramework.Framework.Utility.Coordinate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Models.Box
{
    internal class Box
    {
        [Serializable]
        public class BoxException : Exception
        {
            public BoxException() : base() { }
            public BoxException(string message) : base(message) { }
            public BoxException(string message, Exception innerException) : base(message, innerException) { }
        }

        public abstract class IBoxObject() : FrameworkObject
        {

            /// <summary>
            /// Number of bytes that make up the size of the box header
            /// </summary>
            internal abstract int BoxHeaderSize { get; }

            /// <summary>
            /// Memory domain for box queries
            /// </summary>
            internal abstract MemoryDomain BoxMemoryDomain { get; }

            internal static PokemonFactory PokemonFactory = new();
            internal static BoxFactory BoxFactory = new ();
            internal abstract MemoryQuery CurrentBoxNumberQuery { get; }

            /// <summary>
            /// Current selected box number
            /// </summary>
            public int CurrentBoxNumber => API.Memory.ReadInt(CurrentBoxNumberQuery);

            public abstract MemoryAddress BoxStartingAddress(int boxNumber);
            /// <summary>
            /// Get the number of Pokemon in the specified box
            /// </summary>
            /// <param name="boxNumber">Box number to get number of pokemon for</param>
            /// <returns>Number of pokemon in the box</returns>
            public int NumberOfPokemonInBox(int boxNumber)
            {
                MemoryQuery query = new(address: BoxStartingAddress(boxNumber), size: 1, domain: BoxMemoryDomain);
                return API.Memory.ReadInt(query);
            }

            /// <summary>
            /// Get the number of Pokemon in the current
            /// </summary>
            /// <returns>Number of pokemon in the box</returns>
            public int NumberOfPokemonInCurrentBox()
            {
                return NumberOfPokemonInBox(CurrentBoxNumber);
            }

            /// <summary>
            /// Get all of the Pokemon objects within a specified box
            /// </summary>
            /// <param name="boxNumber">Box to get pokemon from</param>
            /// <returns>List of Pokemon within the box</returns>
            public IReadOnlyList<IPokemonObject> PokemonInBox(int boxNumber)
            {
                List<IPokemonObject> pokemonObjects = new();
                for (int i = 0; i < NumberOfPokemonInBox(boxNumber); i++)
                {
                    MemoryAddress address = BoxAddressAtPosition(boxNumber, i);
                    IPokemonObject pokemon = PokemonFactory.CreateObject(address: address, PokemonMemoryType.BOX);
                    pokemonObjects.Add(pokemon);
                }
                return pokemonObjects;
            }

            /// <summary>
            /// Get all of the Pokemon objects within the current box
            /// </summary>
            /// <returns>List of Pokemon within the current box</returns>
            public IReadOnlyList<IPokemonObject> PokemonInCurrentBox()
            {
                return PokemonInBox(CurrentBoxNumber);
            }

            /// <summary>
            /// Get all of the pokemon within the PC
            /// </summary>
            /// <returns>All pokemon in the PC as a dictionary where the keys are the box that the pokemon is in</returns>
            public IReadOnlyDictionary<int, IReadOnlyList<IPokemonObject>> PokemonInPC()
            {
                Dictionary<int, IReadOnlyList<IPokemonObject>> result = new();
                for (int i = 0; i < BoxFactory.TotalNumberOfBoxes; i++)
                {
                    result.Add(i, PokemonInBox(boxNumber: i));
                }
                return result;
            }

            /// <summary>
            /// Find the first avialable box in the PC that has at least `capacity` open slots
            /// </summary>
            /// <param name="capacity">Number of open slots to look for in the box</param>
            /// <returns>Box number containing with the expected vacancy</returns>
            /// <exception cref="BoxException">No box was found with the expected capacity</exception>
            public int FirstBoxWithCapacity(int capacity = 1)
            {
                for (int i = 0; i <= BoxFactory.TotalNumberOfBoxes; i++)
                {
                    if (BoxFactory.MaximumPokemonPerBox - NumberOfPokemonInBox(boxNumber: i) >= capacity)
                    {
                        return i;
                    }
                }
                throw new BoxException($"Unable to find a box with at least {capacity} open slot(s)");
            }

            /// <summary>
            /// Get the starting address of a pokemon in a specified box and position
            /// </summary>
            /// <param name="boxNumber">Box number</param>
            /// <param name="position">Position within the box</param>
            /// <returns>Starting address of the pokemon</returns>
            internal MemoryAddress BoxAddressAtPosition(int boxNumber, int position)
            {
                return BoxStartingAddress(boxNumber) + BoxHeaderSize + PokemonFactory.PokemonSizeInBox * (position - 1);
            }

            // TODO: Implement navigating within the box
            // TODO: Implement or move `PerformDepositMenuActions
            // TODO: Implement BoxUI methods
        }

        public interface IBoxConfig
        {
            /// <summary>
            /// Total number of boxes in the PC
            /// </summary>
            public int TotalNumberOfBoxes { get; }

            /// <summary>
            /// Maximum number of Pokemon slots in a single PC box
            /// </summary>
            public int MaximumPokemonPerBox { get; }
        }

        public class BoxFactory : TopLevelModule<ConstructorBuilder, IBoxConfig>, IBoxConfig
        {
            internal override Dictionary<IGame, ConstructorBuilder> GameConstructorMap => throw new NotImplementedException();

            /// <summary>
            /// Total number of boxes in the PC
            /// </summary>
            public int TotalNumberOfBoxes => ConfigInstance.TotalNumberOfBoxes;

            /// <summary>
            /// Maximum number of Pokemon slots in a single PC box
            /// </summary>
            public int MaximumPokemonPerBox => ConfigInstance.MaximumPokemonPerBox;
        }
    }
}
