using PokemonFramework.EmulatorBridge.InputInterface;
using PokemonFramework.EmulatorBridge.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Framework.Models.Menu.Object
{
    internal class MenuGoldSilver(MenuType menuType, MemoryQuery cursorQuery, bool vertical = true, bool downIsUp = true) : 
        IMenuObject(menuType, cursorQuery, vertical = true, downIsUp = true)
    {
        public override bool ActiveNavigation(MemoryQuery cursorQuery, int endLocation, InputAction actionOptions, int maxPresses = 100)
        {
            throw new NotImplementedException();
        }

        public override void NavigateMenu(int currentLocation, int endLocation, int maxPresses = 100)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
