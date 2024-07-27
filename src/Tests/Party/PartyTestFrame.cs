using PokemonFramework.Tests.TestUtilities.Models;
using PokemonFramework.Tests.TestUtilities.TestFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonFramework.Tests.Party
{
    public class PartyTestFrame : TestFrame
    {
        internal new String _testSuite = "Party";

        public TestStatus TestCheckPartySize()
        {
            Random random = new();
            List<TestStatus> testStatuses = Enum.GetValues(typeof(TestStatus)).Cast<TestStatus>().ToList();
            int index = random.Next(testStatuses.Count);
            Thread.Sleep(2000);
            return testStatuses[index];
        }

        public TestStatus TestCheckMaxPartySize()
        {
            return TestCheckPartySize();
        }
    }
}
