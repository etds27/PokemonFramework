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


        internal TestStatus WorstTestStatus = TestStatus.Success;
        internal string WorstTestMessage = "";
        internal void TestCaseSetup()
        {
            // Reset the previous test's worst result
            WorstTestStatus = TestStatus.Success;
            WorstTestMessage = "";
        }

        /// <summary>
        /// Cleans up from the previous test execution and delivers the final test status
        /// </summary>
        internal TestStatus TestCaseTearDown()
        {
            // Reset the continue after failure
            ContinueAfterFailure = false;
            return WorstTestStatus;
        }

        private void UpdateWorstStatus(TestStatus status, string message)
        {
            if (status < WorstTestStatus)
            {
                WorstTestStatus = status;
                WorstTestMessage = message;
            }
        }

        internal void Assert(bool status, string message = "")
        {
            if (!status && !ContinueAfterFailure)
            {
                throw new TestInterruption(TestStatus.Fail, message);
            }
            else if (!status && ContinueAfterFailure)
            {
                UpdateWorstStatus(TestStatus.Fail, message);
            }
        }

        internal void AssertFalse(bool status, string message = "")
        {
            Assert(!status, message);
        }

        internal void AssertEqual<T>(IEquatable<T> equatable1, IEquatable<T> equatable2, string message = "")
        {
            string updatedMesage = $"{equatable1} != {equatable2}: {message}";
            Assert(equatable1.Equals(equatable2), updatedMesage);
        }

        internal void AssertNonEqual<T>(IEquatable<T> equatable1, IEquatable<T> equatable2, string message = "")
        {
            string updatedMesage = $"{equatable1} == {equatable2}: {message}";
            Assert(!equatable1.Equals(equatable2), updatedMesage);
        }

        internal void AssertGreaterThan(IComparable comparable1, IComparable comparable2, string message = "")
        {
            string updatedMesage = $"{comparable1} <= {comparable2}: {message}";
            Assert(comparable1.CompareTo(comparable2) > 0, updatedMesage);
        }

        internal void AssertLessThan(IComparable comparable1, IComparable comparable2, string message = "")
        {
            string updatedMesage = $"{comparable1} >= {comparable2}: {message}";
            Assert(comparable1.CompareTo(comparable2) < 0, updatedMesage);
        }
    }
}
