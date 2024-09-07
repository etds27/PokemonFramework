using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections.Generic;

namespace PokemonFramework.Framework.Models.Pokemon.PokemonObject
{
    public class PokemonCrystal : IPokemonObject
    {
        public PokemonCrystal(MemoryAddress address, PokemonMemoryType pokemonMemoryType) : base(address, pokemonMemoryType)
        { }

        public override int PokemonSize => throw new NotImplementedException();

        public override bool Shiny
        {
            get {
                return new HashSet<int>([2, 3, 6, 7, 10, 11, 14, 15]).Contains(AttackIV) &&
                    DefenseIV == 10 &&
                    SpeedIV == 10 &&
                    SpecialIV == 10;
            }
        }

        public override int AttackIV => (IVData >> 12) & 0x000F;

        public override int DefenseIV => (IVData >> 8) & 0x000F;

        public override int SpeedIV => (IVData >> 4) & 0x000F;

        public override int SpecialAttackIV => IVData & 0x000F;

        public override int SpecialDefenseIV => IVData & 0x000F;

        public override int SpecialIV => IVData & 0x000F;

        public override int HPIV =>
            AttackIV & 0x1 * 8 +
            DefenseIV * 0x1 * 4 +
            SpeedIV * 0x1 * 2 +
            SpecialAttackIV * 0x1;

        internal override IReadOnlyDictionary<string, MemoryQuery> TrainerOffsetQuery => new Dictionary<string, MemoryQuery> {
            { "species", new(address: PokemonMemoryAddress + 0x0, size: 0, domain: MemoryDomain.WRAM) }
        
        
        };

        internal override IReadOnlyDictionary<string, MemoryQuery> BoxOffsetQuery => throw new NotImplementedException();

        internal override IReadOnlyDictionary<string, MemoryQuery> WildOffsetQuery => throw new NotImplementedException();

        internal override void FetchPokemonData()
        {
            throw new NotImplementedException();
        }
    }
}
