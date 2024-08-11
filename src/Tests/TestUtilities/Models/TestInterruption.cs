using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Tests.TestUtilities.Models
{
    public class TestInterruption(TestStatus status, string errorString = "") : Exception
    {
        public TestStatus TestStatus = status;
        public string ErrorString = errorString;
    }
}
