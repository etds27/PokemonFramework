using PokemonFramework.Framework.Models.Clock;
using PokemonFramework.Framework.Models.Menu;
using PokemonFramework.Tests.TestUtilities.SaveStates;
using PokemonFramework.Tests.TestUtilities.TestFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Tests.Clock
{
    internal class ClockTests : TestClass
    {
        private readonly IClockObject clock = new ClockFactory().CreateObject();

        public void TestClockDay()
        {
            API.Emulator.LoadState(TestStateManager.TestBagState);
            AssertEqual(clock.Day, 1, "Day did not match expect value");
        }

        public void TestClockHour()
        {
            API.Emulator.LoadState(TestStateManager.TestBagState);
            AssertEqual(clock.Hour, 1, "Day did not match expect value");
        }

        public void TestClockMinute()
        {
            API.Emulator.LoadState(TestStateManager.TestBagState);
            AssertEqual(clock.Minute, 1, "Day did not match expect value");
        }

        public void TestClockSecond()
        {
            API.Emulator.LoadState(TestStateManager.TestBagState);
            AssertEqual(clock.Second, 1, "Day did not match expect value");
        }
    }

    public class ClockTestFrame : TestFrame
    {
        public ClockTestFrame() : base()
        {
            TestSuite = "Clock";
        }

        internal override TestClass TestClass => new ClockTests();
    }
}
