
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Common;
using BizHawk.Common.IOExtensions;
using PokemonFramework.EmulatorBridge;
using PokemonFramework.EmulatorBridge.APIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;


namespace PokemonFramework
{
    [ExternalTool("PokemonFrameworkRunner")]
    public class PokemonFramework : ToolFormBase, IExternalToolForm
    {
        private ApiContainer? _maybeAPIContainer { get; set; }
        private ApiContainer APIContainer => _maybeAPIContainer!;

        protected override string WindowTitleStatic => "Pokemon Framework Runner";
        private Label _headerLabel = new Label() { AutoSize = true };
        public PokemonFramework()
        {
            ClientSize = new System.Drawing.Size(400, 400);
            _headerLabel.Text = "Pokemon Framework";
            Controls.Add(_headerLabel);


        }

        public override void Restart()
        {
            base.Restart();
            BizHawkAPI.SetAPIContainer(apiContainer: APIContainer);

            IEmulatorInterface emulatorInterface = new BizHawkEmulatorBridge();
            List<byte> romName = emulatorInterface.Memory.Read(address: 0x134, size: 10, domain: EmulatorBridge.MemoryInterface.MemoryDomain.ROM).ToList();
            _headerLabel.Text = Encoding.Default.GetString(romName.ToArray());
        }
    }

}