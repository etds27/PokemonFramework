using PokemonFramework.Framework.Game;
using PokemonFramework.Framework.Module;
using PokemonFramework.Framework.Party.PartyObject;
using PokemonFramework.Framework.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonFramework.Framework.Pokemon.PokemonObject;
using PokemonFramework.Framework.Pokemon.PokemonConfig;

namespace PokemonFramework.Framework.Pokemon
{
    using Constructor = Func<MemoryAddress, PokemonMemoryType, IPokemonObject>;

    public abstract class IPokemonObject : FrameworkObject
    {
        public MemoryAddress PokemonMemoryAddress;
        public PokemonMemoryType PokemonMemoryType;

        public IPokemonObject(MemoryAddress address, PokemonMemoryType pokemonMemoryType)
        {
            PokemonMemoryAddress = address;
            PokemonMemoryType = pokemonMemoryType;

            Update();
        }

        public abstract int PokemonSize { get; }
    }

    public interface IPokemonConfig
    {
        public int PokemonSizeInParty { get; }
        public int PokemonSizeInBattle { get; }
        public int PokemonSizeInBox { get; }

    }

    public enum PokemonMemoryType
    {
        PARTY,
        BATTLE,
        BOX
    }

    public class PokemonFactory : TopLevelModule<IPokemonObject, IPokemonConfig>, IPokemonConfig
    {
        internal new static Dictionary<IGame, IPokemonConfig> GameConfigMap = new()
        {
            { PokemonGame.GOLD, new PokemonConfigGoldSilver() },
            { PokemonGame.SILVER, new PokemonConfigGoldSilver() },
            { PokemonGame.CRYSTAL, new PokemonConfigCrystal() }
        };

        internal new static Dictionary<IGame, Constructor> GameConstructorMap = new()
        {
            { PokemonGame.GOLD, (MemoryAddress address, PokemonMemoryType memoryType) => new PokemonGoldSilver(address, memoryType) },
            { PokemonGame.SILVER, (MemoryAddress address, PokemonMemoryType memoryType) => new PokemonGoldSilver(address, memoryType) },
            { PokemonGame.CRYSTAL, (MemoryAddress address, PokemonMemoryType memoryType) => new PokemonCrystal(address, memoryType) }
        };

        public IPokemonObject CreateObject(MemoryAddress address, PokemonMemoryType pokemonMemoryType)
        {
            if ((GameConstructorMap ?? []).TryGetValue(CurrentGame, out Constructor tempObject))
            {
                if (tempObject != null)
                {
                    return tempObject(address, pokemonMemoryType);
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

        public int PokemonSizeInParty => StaticModelInstance.PokemonSizeInParty;

        public int PokemonSizeInBattle => StaticModelInstance.PokemonSizeInBattle;

        public int PokemonSizeInBox => StaticModelInstance.PokemonSizeInBox;
    }
}
