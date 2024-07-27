﻿using PokemonFramework.Framework.Game;
using PokemonFramework.Framework.Module;
using PokemonFramework.Framework.Party.PartyObject;
using PokemonFramework.Framework.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonFramework.Framework.Pokemon.PokemonObject;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Pokemon.PokemonConfig;

namespace PokemonFramework.Framework.Pokemon
{
    using Constructor = Func<MemoryQuery, PokemonMemoryType, IPokemonObject>;

    public abstract class IPokemonObject : FrameworkObject
    {
        public MemoryQuery PokemonMemoryQuery;
        public PokemonMemoryType PokemonMemoryType;

        public IPokemonObject(MemoryQuery query, PokemonMemoryType pokemonMemoryType)
        {
            PokemonMemoryQuery = query;
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
            { PokemonGame.CRYSTAL, new PokemonCrystalConfig() }
        };

        internal new static Dictionary<IGame, Constructor> GameConstructorMap = new()
        {
            { PokemonGame.CRYSTAL, (MemoryQuery query, PokemonMemoryType memoryType) => new PokemonCrystal(query, memoryType) }
        };

        public IPokemonObject CreateObject(MemoryQuery query, PokemonMemoryType pokemonMemoryType)
        {
            if ((GameConstructorMap ?? []).TryGetValue(CurrentGame, out Constructor tempObject))
            {
                if (tempObject != null)
                {
                    return tempObject(query, pokemonMemoryType);
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
