using PokemonFramework.Tests.Menu;
using PokemonFramework.Tests.Party;
using PokemonFramework.Tests.TestUtilities.TestFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonFramework.Tests
{
    public class MainTestControl : UserControl
    {

        private TabControl TestTabControl = new();

        private IReadOnlyList<TestFrame> TestFrames = [
                new PartyTestFrame(),
                new MenuTestFrame()
        ];

        public MainTestControl() {
            TestTabControl.Dock = DockStyle.Fill;
            Controls.Add(TestTabControl);
            CreateTestViews();
        }


        public void CreateTestViews()
        {

            for (int i = 0; i < TestFrames.Count; i++)
            {
                TestFrame frame = TestFrames[i];
                TabPage tabPage = new()
                {
                    Text = frame.TestSuite
                };
                tabPage.Controls.Add(frame);

                TestTabControl.Controls.Add(tabPage);
            }
        }
    }
}
