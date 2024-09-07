using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Models.Game;
using PokemonFramework.Framework.Models.Module;
using PokemonFramework.Framework.Models.Move;
using PokemonFramework.Framework.Models.Pokemon.PokemonConfig;
using PokemonFramework.Framework.Models.Pokemon.PokemonObject;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PokemonFramework.Framework.Models.Pokemon
{
    using Constructor = Func<MemoryAddress, PokemonMemoryType, IPokemonObject>;

    public struct Stats(int attack, int defense, int hp, int speed, int specialAttack, int specialDefense)
    {
        public int Attack = attack;
        public int Defense = defense;
        public int HP = hp;
        public int Speed = speed;
        public int SpecialAttack = specialAttack;
        public int SpecialDefense = specialDefense;
    }


    public class PokemonException : Exception
    {
        public PokemonException() : base() { }
        public PokemonException(string message) : base(message) { }
        public PokemonException(string message, Exception innerException) : base(message, innerException) { }
    }

    public abstract class IPokemonObject : FrameworkObject
    {



        public MemoryAddress PokemonMemoryAddress;
        public PokemonMemoryType PokemonMemoryType;

        public IPokemonObject(MemoryAddress address, PokemonMemoryType pokemonMemoryType)
        {
            PokemonMemoryAddress = address;
            PokemonMemoryType = pokemonMemoryType;
        }

        internal abstract IReadOnlyDictionary<string, MemoryQuery> TrainerOffsetQuery { get; }
        internal abstract IReadOnlyDictionary<string, MemoryQuery> BoxOffsetQuery { get; }
        internal abstract IReadOnlyDictionary<string, MemoryQuery> WildOffsetQuery { get; }

        // Raw memory values
        public int Species => API.Memory.ReadInt(PokemonPropertyQueryLookup("species"));
        public Item.Item HeldItem =>
            new(API.Memory.Read(PokemonPropertyQueryLookup("held_item"))[0]);

        public Move.Move Move1 => new(API.Memory.Read(PokemonPropertyQueryLookup("move_1"))[0]);
        public Move.Move Move2 => new(API.Memory.Read(PokemonPropertyQueryLookup("move_2"))[0]);
        public Move.Move Move3 => new(API.Memory.Read(PokemonPropertyQueryLookup("move_3"))[0]);
        public Move.Move Move4 => new(API.Memory.Read(PokemonPropertyQueryLookup("move_4"))[0]);

        public int TrainerID => API.Memory.ReadInt(PokemonPropertyQueryLookup("trainer_id"));
        public int EXPPoints => API.Memory.ReadInt(PokemonPropertyQueryLookup("exp_points"));

        public int HPEV => API.Memory.ReadInt(PokemonPropertyQueryLookup("hp_ev"));
        public int AttackEV => API.Memory.ReadInt(PokemonPropertyQueryLookup("attack_ev"));
        public int DefenseEV => API.Memory.ReadInt(PokemonPropertyQueryLookup("defense_ev"));
        public int SpeedEV => API.Memory.ReadInt(PokemonPropertyQueryLookup("speed_ev"));
        public int SpecialEV => API.Memory.ReadInt(PokemonPropertyQueryLookup("special_ev"));
        public int SpecialAttackEV => API.Memory.ReadInt(PokemonPropertyQueryLookup("special_attack_ev"));
        public int SpecialDefenseEV => API.Memory.ReadInt(PokemonPropertyQueryLookup("special_defense_ev"));
        public int IVData => API.Memory.ReadInt(PokemonPropertyQueryLookup("iv_data"));
        public int MovePP1 => API.Memory.ReadInt(PokemonPropertyQueryLookup("move_pp_1"));
        public int MovePP2 => API.Memory.ReadInt(PokemonPropertyQueryLookup("move_pp_2"));
        public int MovePP3 => API.Memory.ReadInt(PokemonPropertyQueryLookup("move_pp_3"));
        public int MovePP4 => API.Memory.ReadInt(PokemonPropertyQueryLookup("move_pp_4"));
        public int Friendship => API.Memory.ReadInt(PokemonPropertyQueryLookup("friendship"));
        public bool Pokerus => API.Memory.ReadBool(PokemonPropertyQueryLookup("pokerus"));
        public int CaughtData => API.Memory.ReadInt(PokemonPropertyQueryLookup("caught_data"));
        public int Level => API.Memory.ReadInt(PokemonPropertyQueryLookup("level"));

        // TODO: Change to Status enum
        public int Status => API.Memory.ReadInt(PokemonPropertyQueryLookup("status"));
        public int CurrentHP => API.Memory.ReadInt(PokemonPropertyQueryLookup("current_hp"));
        public int HPStat => API.Memory.ReadInt(PokemonPropertyQueryLookup("hp_stat"));
        public int AttackStat => API.Memory.ReadInt(PokemonPropertyQueryLookup("attack_stat"));
        public int DefenseStat => API.Memory.ReadInt(PokemonPropertyQueryLookup("defense_stat"));
        public int SpeedStat => API.Memory.ReadInt(PokemonPropertyQueryLookup("speed_stat"));
        public int SpecialAttackStat => API.Memory.ReadInt(PokemonPropertyQueryLookup("spattack_stat"));
        public int SpecialDefenseStat => API.Memory.ReadInt(PokemonPropertyQueryLookup("spdefense_stat"));



        // Derived values
        public IReadOnlyList<Tuple<Move.Move, int>> Moves => new List<Tuple<Move.Move, int>>()
        {
            new (Move1, MovePP1),
            new (Move2, MovePP2),
            new (Move3, MovePP3),
            new (Move4, MovePP4)
        };


        public abstract int AttackIV { get; }
        public abstract int DefenseIV { get; }
        public abstract int SpeedIV { get; }
        public abstract int SpecialAttackIV { get; }
        public abstract int SpecialDefenseIV {  get; }
        public abstract int SpecialIV { get; }
        public abstract int HPIV { get; }

        public Stats IVs => new(
            attack: AttackIV,
            defense: DefenseIV,
            speed: SpeedIV,
            specialAttack: SpecialAttackIV,
            specialDefense: SpecialDefenseIV,
            hp: HPIV
            );


        public Stats EVs => new (
            attack: AttackEV,
            defense: DefenseEV,
            speed: SpeedEV,
            hp: HPEV,
            specialAttack: SpecialAttackEV,
            specialDefense: SpecialDefenseEV
        );

        public Stats Stats => new (
            hp: HPStat,
            attack: AttackStat,
            defense: DefenseStat,
            speed: SpeedStat,
            specialAttack: SpecialAttackStat,
            specialDefense: SpecialDefenseStat
        );

        public abstract bool Shiny { get; }

        internal MemoryQuery PokemonPropertyQueryLookup(string property)
        {
            IReadOnlyDictionary<string, MemoryQuery> queryDictionary;
            switch (PokemonMemoryType)
            {
                case PokemonMemoryType.PARTY:
                    queryDictionary = TrainerOffsetQuery; 
                    break;
                case PokemonMemoryType.BOX:
                    queryDictionary = BoxOffsetQuery;
                    break;
                case PokemonMemoryType.BATTLE:
                    queryDictionary = WildOffsetQuery;
                    break;
                default:
                    throw new PokemonException($"Unknown Pokemon Memory Type for query lookup: {PokemonMemoryType}");
            }
            MemoryQuery query;

            if (queryDictionary.TryGetValue(key: property, out query))
            {
                throw new PokemonException($"Unknown Pokemon Property Type: {property}")
            }
            return query;
        }



        internal abstract void FetchPokemonData();

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

    public class PokemonFactory : TopLevelModule<Constructor, IPokemonConfig>, IPokemonConfig
    {
        internal new Dictionary<IGame, IPokemonConfig> GameConfigMap = new()
        {
            { PokemonGame.GOLD, new PokemonConfigGoldSilver() },
            { PokemonGame.SILVER, new PokemonConfigGoldSilver() },
            { PokemonGame.CRYSTAL, new PokemonConfigCrystal() }
        };

        internal override Dictionary<IGame, Constructor> GameConstructorMap => new()
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

        public int PokemonSizeInParty => ConfigInstance.PokemonSizeInParty;

        public int PokemonSizeInBattle => ConfigInstance.PokemonSizeInBattle;

        public int PokemonSizeInBox => ConfigInstance.PokemonSizeInBox;
    }
}
