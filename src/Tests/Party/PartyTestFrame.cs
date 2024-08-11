using PokemonFramework.Tests.TestUtilities.Models;
using PokemonFramework.Tests.TestUtilities.SaveStates;
using PokemonFramework.Tests.TestUtilities.TestFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonFramework.Tests.Party
{
    internal class PartyTests : TestClass
    {
        public void TestCheckPartySize()
        {
            API.Emulator.LoadState(TestStateManager.TestCheckPartySize);
            Random random = new();
            List<TestStatus> testStatuses = Enum.GetValues(typeof(TestStatus)).Cast<TestStatus>().ToList();
            int index = random.Next(testStatuses.Count);
            Thread.Sleep(2000);
        }

        public void TestCheckMaxPartySize()
        {
            TestCheckPartySize();
        }
    }

    public class PartyTestFrame : TestFrame
    {
        public PartyTestFrame() : base() {
            TestSuite = "Party";
        }

        internal override TestClass TestClass => new PartyTests();
    }
}
