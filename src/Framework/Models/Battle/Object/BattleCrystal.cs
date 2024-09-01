using PokemonFramework.EmulatorBridge.MemoryInterface;
using PokemonFramework.Framework.Models.Menu;
using PokemonFramework.Framework.Utility.Coordinate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Models.Battle.Object
{
    public class BattleCrystal : IBattleObject
    {
        public class BattleMenuCrystal() : IBattleMenu(menuFactory.DefaultMultiCursorXQuery, menuFactory.DefaultMultiCursorYQuery)
        {
            internal override Coordinate BattleMenuCoordinate => new(1, 1);

            internal override Coordinate PokemonMenuCoordinate => new(2, 1);

            internal override Coordinate PackMenuCoordinate => new(1, 2);

            internal override Coordinate RunMenuCoordinate => new(2, 2);
        }

        public override IBattleMenu Menu => new BattleMenuCrystal();

        internal override MemoryQuery PlayerTurnCounterQuery => new(0xC6DD, 1, MemoryDomain.WRAM);

        internal override MemoryQuery EnemyTurnCounterQuery => new(0xC6DC, 1, MemoryDomain.WRAM);

        public override void OpenPack()
        {
            throw new NotImplementedException();
        }

        public override void RunFromPokemon()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override CatchStatus WaitForCatch()
        {
            throw new NotImplementedException();
        }
    }
}
