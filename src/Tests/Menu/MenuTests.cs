using PokemonFramework.Framework.Models.Menu;
using PokemonFramework.Tests.TestUtilities.Models;
using PokemonFramework.Tests.TestUtilities.SaveStates;
using PokemonFramework.Tests.TestUtilities.TestFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PokemonFramework.Tests.Menu
{
    internal class MenuTests : TestClass
    {
        public TestStatus TestMenuNavigateUp()
        {
            API.Emulator.LoadState(TestStateManager.TestBagState);
            IMenuObject menu = new MenuFactory().DefaultMenu;
            
            Assert(menu.NavigateMenu(4), "Unable to navigate menu to index 4");
            return TestStatus.Success;
        }

        public TestStatus TestMenuNavigateDown()
        {
            return TestMenuNavigateUp();
        }


    }

    public class MenuTestFrame : TestFrame
    {
        public MenuTestFrame() : base()
        {
            TestSuite = "Menu";
        }

        internal override TestClass TestClass => new MenuTests();
    }
}
