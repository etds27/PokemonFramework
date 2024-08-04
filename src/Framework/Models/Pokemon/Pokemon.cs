using PokemonFramework.Framework.Models.Game;
using PokemonFramework.Framework.Models.Module;
using PokemonFramework.Framework.Models.Pokemon.PokemonConfig;
using PokemonFramework.Framework.Models.Pokemon.PokemonObject;
using System;
using System.Collections.Generic;

namespace PokemonFramework.Framework.Models.Pokemon
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
            { PokemonGame.GOLD, (address, memoryType) => new PokemonGoldSilver(address, memoryType) },
            { PokemonGame.SILVER, (address, memoryType) => new PokemonGoldSilver(address, memoryType) },
            { PokemonGame.CRYSTAL, (address, memoryType) => new PokemonCrystal(address, memoryType) }
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
