using PokemonFramework.Tests.TestUtilities.Models;
using PokemonFramework.Tests.TestUtilities.TestFrame;

namespace PokemonFramework.Tests.Emulator
{
    internal class EmulatorTests : TestClass
    {
        public void TestFrameAdvance()
        {
            int frameAdvance = 60;
            int startingFrames = API.Emulator.GetFrameCount();
            API.Emulator.AdvanceFrames(frames: frameAdvance);
            int frameDelta = API.Emulator.GetFrameCount() - startingFrames;
            AssertEqual(frameDelta, frameAdvance, "Expected frame advance not correct");
        }
    }

    public class EmulatorTestFrame : TestFrame
    {
        public EmulatorTestFrame() : base()
        {
            TestSuite = "Emulator";
        }

        internal override TestClass TestClass => new EmulatorTests();
    }
}
