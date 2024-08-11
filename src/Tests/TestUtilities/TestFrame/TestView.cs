using PokemonFramework.Tests.TestUtilities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonFramework.Tests.TestUtilities.TestFrame
{
    internal class TestView : UserControl
    {
        private Label testNameLabel;
        private Label testStatusLabel;
        private Button runTestButton;

        private String _testName = "TEST NAME";
        public String testName
        {
            get { return _testName; }
            set 
            {
                _testName = value;
                testNameLabel.Text = _testName;
            }
        }

        public Func<TestStatus> TestCallback;

        private TestStatus _testStatus = TestStatus.None;
        public TestStatus testStatus
        {
            get { return _testStatus; }
            set {
                _testStatus = value;
                testStatusLabel.Text = _testStatus.ToString();
                switch (_testStatus)
                {
                    case TestStatus.Success:
                        testStatusLabel.BackColor = Color.Green;
                        break;
                    case TestStatus.Fail:
                        testStatusLabel.BackColor = Color.Red;
                        break;
                    case TestStatus.KnownFailure:
                        testStatusLabel.BackColor = Color.Yellow;
                        break;
                    case TestStatus.None:
                        testStatusLabel.BackColor = Color.Gray;
                        break;
                    case TestStatus.ExpectedFailure:
                        testStatusLabel.BackColor = Color.LightBlue;
                        break;
                    case TestStatus.Abort:
                        testStatusLabel.BackColor = Color.Orange;
                        break;
                }
            }
        }

        public TestView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.runTestButton = new System.Windows.Forms.Button();
            this.testNameLabel = new System.Windows.Forms.Label();
            this.testStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // runTestButton
            // 
            this.runTestButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.runTestButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.runTestButton.Location = new System.Drawing.Point(212, 0);
            this.runTestButton.Name = "runTestButton";
            this.runTestButton.Size = new System.Drawing.Size(43, 36);
            this.runTestButton.TabIndex = 0;
            this.runTestButton.Text = "Go";
            this.runTestButton.UseVisualStyleBackColor = true;
            this.runTestButton.Click += new System.EventHandler(this.runTestButton_Click);
            // 
            // testNameLabel
            // 
            this.testNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testNameLabel.AutoSize = true;
            this.testNameLabel.Location = new System.Drawing.Point(3, 10);
            this.testNameLabel.Name = "testNameLabel";
            this.testNameLabel.Size = new System.Drawing.Size(69, 13);
            this.testNameLabel.TabIndex = 1;
            this.testNameLabel.Text = "TEST NAME";
            // 
            // testStatusLabel
            // 
            this.testStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.testStatusLabel.AutoSize = true;
            this.testStatusLabel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.testStatusLabel.Location = new System.Drawing.Point(173, 10);
            this.testStatusLabel.Name = "testStatusLabel";
            this.testStatusLabel.Size = new System.Drawing.Size(33, 13);
            this.testStatusLabel.TabIndex = 2;
            this.testStatusLabel.Text = "None";
            // 
            // TestView
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.testStatusLabel);
            this.Controls.Add(this.testNameLabel);
            this.Controls.Add(this.runTestButton);
            this.Name = "TestView";
            this.Size = new System.Drawing.Size(255, 36);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void runTestButton_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(RunTest);
            thread.IsBackground = true;
            thread.Start();
        }

        private void RunTest()
        {
            runTestButton.Text = "Running...";
            TestStatus executionStatus;
            try
            {
                executionStatus = TestCallback();
            }
            catch (TestInterruption e)
            {
                Serilog.Log.Warning("Encountered Test Interruption\n{e}", e);
                executionStatus = e.TestStatus;
                MessageBox.Show(e.ErrorString);
            }
            catch (Exception e)
            {
                Serilog.Log.Warning("Encountered Unclass Exception\n{e}", e);
                executionStatus = TestStatus.Abort;
                MessageBox.Show(e.Message);
            }
            runTestButton.Text = "Go";
            testStatus = executionStatus;
        }
    }
}
