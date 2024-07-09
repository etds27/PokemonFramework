using PokemonFramework.EmulatorBridge;
using System;
using System.Windows.Forms;

namespace PokemonFramework.Forms
{
    public class PokemonFrameworkForm : UserControl
    {
        public PokemonFrameworkForm()
        {
            InitializeComponent();
        }

        private Button pauseButton;

        private void InitializeComponent()
        {
            this.pauseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(0, 0);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(321, 211);
            this.pauseButton.TabIndex = 0;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // PokemonFrameworkForm
            // 
            this.Controls.Add(this.pauseButton);
            this.Name = "PokemonFrameworkForm";
            this.Size = new System.Drawing.Size(323, 212);
            this.ResumeLayout(false);
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            IEmulatorInterface emulator = new BizHawkEmulatorBridge();
            if (emulator.Emulator.IsRunning())
            {
                emulator.Emulator.Pause();
                pauseButton.Text = "Resume";
            } else
            {
                emulator.Emulator.Resume();
                pauseButton.Text = "Pause";
            }
        }
    }
}
