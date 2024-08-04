
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Common;
using BizHawk.Common.IOExtensions;
using PokemonFramework.EmulatorBridge;
using PokemonFramework.EmulatorBridge.APIContainer;
using PokemonFramework.Forms;
using PokemonFramework.Tests.Party;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
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
        private Label _headerLabel = new() { AutoSize = true };
        public PokemonFramework()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\log.txt")
                .CreateLogger();

            Serilog.Log.Information("Initializing PokemonFramework");

            ClientSize = new System.Drawing.Size(400, 400);
            _headerLabel.Text = "Pokemon Framework";
            Controls.Add(_headerLabel);
            Controls.Add(new PartyTestFrame());

            IEmulatorInterface emulatorInterface = EmulatorFactory.GetEmulator();
        }

        public override void Restart()
        {
            base.Restart();
            BizHawkAPI.SetAPIContainer(apiContainer: APIContainer);
        }
    }

}