using PokemonFramework.Framework.Models.Menu;
using PokemonFramework.Tests.TestUtilities.SaveStates;
using PokemonFramework.Tests.TestUtilities.TestFrame;

namespace PokemonFramework.Tests.Menu
{
    internal class MenuTests : TestClass
    {
        private readonly IMenuObject menu = new MenuFactory().DefaultMenu;

        public void TestMenuNavigateUp()
        {
            API.Emulator.LoadState(TestStateManager.TestBagState);
            
            Assert(menu.NavigateMenu(5), "Unable to navigate menu to index 5");
        }

        public void TestMenuNavigateDown()
        {
            API.Emulator.LoadState(TestStateManager.TestBagState);

            Assert(menu.NavigateMenu(1), "Unable to navigate menu to index 1");
        }

        public void TestActiveNavigateUp()
        {
            API.Emulator.LoadState(TestStateManager.TestBagState);
            Assert(menu.ActiveNavigation(5), "Unable to actively navigate to index 5");
        }

        public void TestActiveNavigateDown()
        {
            API.Emulator.LoadState(TestStateManager.TestBagState);
            Assert(menu.ActiveNavigation(1), "Unable to actively navigate to index 1");
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
