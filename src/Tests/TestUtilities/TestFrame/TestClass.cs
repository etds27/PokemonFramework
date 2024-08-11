using PokemonFramework.EmulatorBridge;
using PokemonFramework.Tests.TestUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFramework.Tests.TestUtilities.TestFrame
{
    /// <summary>
    /// Contains all of the tests that should be ran for a test suite
    /// </summary>
    internal class TestClass
    {
        internal IEmulatorInterface API = EmulatorFactory.GetEmulator();
        internal bool ContinueAfterFailure = false;


        internal TestStatus Assert(bool status, string message = "")
        {
            if (!status && !ContinueAfterFailure)
            {
                throw new TestInterruption(TestStatus.Fail, message);
            }
            else if (!status && ContinueAfterFailure)
            {
                return TestStatus.Fail;
            }
            return TestStatus.Success;
        }

        internal TestStatus AssertFalse(bool status, string message = "")
        {
            return Assert(!status, message);
        }

        internal TestStatus AssertEqual<T>(IEquatable<T> equatable1, IEquatable<T> equatable2, string message = "")
        {
            string updatedMesage = $"{equatable1} != {equatable2}: {message}";
            return Assert(equatable1.Equals(equatable2), updatedMesage);
        }

        internal TestStatus AssertNonEqual<T>(IEquatable<T> equatable1, IEquatable<T> equatable2, string message = "")
        {
            string updatedMesage = $"{equatable1} == {equatable2}: {message}";
            return Assert(!equatable1.Equals(equatable2), updatedMesage);
        }

        internal TestStatus AssertGreaterThan(IComparable comparable1, IComparable comparable2, string message = "")
        {
            string updatedMesage = $"{comparable1} <= {comparable2}: {message}";
            return Assert(comparable1.CompareTo(comparable2) > 0, updatedMesage);
        }

        internal TestStatus AssertLessThan(IComparable comparable1, IComparable comparable2, string message = "")
        {
            string updatedMesage = $"{comparable1} >= {comparable2}: {message}";
            return Assert(comparable1.CompareTo(comparable2) < 0, updatedMesage);
        }
    }
}
