
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using PokemonFramework.EmulatorBridge;
using PokemonFramework.EmulatorBridge.APIContainer;
using PokemonFramework.Tests;
using PokemonFramework.Tests.Menu;
using PokemonFramework.Tests.Party;
using PokemonFramework.Tests.TestUtilities.TestFrame;
using Serilog;
using System.Collections.Generic;
using System.Windows.Forms;


namespace PokemonFramework
{
    [ExternalTool("PokemonFrameworkRunner")]
    [ExternalToolApplicability.AnyRomLoaded]
    public class PokemonFramework : ToolFormBase, IExternalToolForm
    {
        private ApiContainer? _maybeAPIContainer { get; set; }
        private ApiContainer APIContainer => _maybeAPIContainer!;

        protected override string WindowTitleStatic => "Pokemon Framework Runner";

        public PokemonFramework()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\log.txt")
                .CreateLogger();

            Log.Information("Initializing PokemonFramework");

            ClientSize = new System.Drawing.Size(400, 400);
            MainTestControl mainTestControl = new MainTestControl();
            mainTestControl.Dock = DockStyle.Fill;
            Controls.Add(mainTestControl);

        }

        public override void Restart()
        {
            base.Restart();
            BizHawkAPI.SetAPIContainer(apiContainer: APIContainer);
        }
    }

}