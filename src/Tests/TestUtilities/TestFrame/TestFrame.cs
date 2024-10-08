﻿using PokemonFramework.EmulatorBridge;
using PokemonFramework.Tests.TestUtilities.Models;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace PokemonFramework.Tests.TestUtilities.TestFrame
{
    public abstract class TestFrame : UserControl
    {
        internal IEmulatorInterface API = new BizHawkEmulatorBridge();
        private Label testSuiteNameLabel;
        private FlowLayoutPanel testSuiteLayoutPanel;
        internal string _testSuite = "TEST SUITE";
        internal abstract TestClass TestClass { get; }

        public string TestSuite
        {

            get { return _testSuite; }
            set 
            { 
                _testSuite = value;
                testSuiteNameLabel.Text = value;
            }
        }

        public TestFrame()
        {
            InitializeComponent();
            LoadTests();
        }

        private void InitializeComponent()
        {
            this.testSuiteNameLabel = new System.Windows.Forms.Label();
            this.testSuiteLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // testSuiteNameLabel
            // 
            this.testSuiteNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.testSuiteNameLabel.AutoSize = true;
            this.testSuiteNameLabel.Location = new System.Drawing.Point(275, 11);
            this.testSuiteNameLabel.Name = "testSuiteNameLabel";
            this.testSuiteNameLabel.Size = new System.Drawing.Size(55, 13);
            this.testSuiteNameLabel.TabIndex = 0;
            this.testSuiteNameLabel.Text = "Test Suite";
            this.testSuiteNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // testSuiteLayoutPanel
            // 
            this.testSuiteLayoutPanel.AutoScroll = true;
            this.testSuiteLayoutPanel.AutoSize = true;
            this.testSuiteLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.testSuiteLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.testSuiteLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.testSuiteLayoutPanel.Name = "testSuiteLayoutPanel";
            this.testSuiteLayoutPanel.Size = new System.Drawing.Size(589, 0);
            this.testSuiteLayoutPanel.TabIndex = 1;
            this.testSuiteLayoutPanel.WrapContents = false;
            // 
            // TestFrame
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.Controls.Add(this.testSuiteLayoutPanel);
            this.Controls.Add(this.testSuiteNameLabel);
            this.Name = "TestFrame";
            this.Size = new System.Drawing.Size(589, 368);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoadTests()
        {
            MethodInfo[] classMethods = this.TestClass.GetType().GetMethods();
            foreach (MethodInfo method in classMethods)
            {
                if (method.Name.StartsWith("Test") && method.GetParameters().Length == 0)
                {
                    Action testFunc = (Action)Delegate.CreateDelegate(typeof(Action), TestClass, method);

                    TestView testView = new()
                    {
                        testName = method.Name,
                        testStatus = TestStatus.None,
                        TestCallback = testFunc,
                        SetupCallback = TestClass.TestCaseSetup,
                        TearDownCallback = TestClass.TestCaseTearDown
                    };

                    testSuiteLayoutPanel.Controls.Add(testView);
                }
            }
        }
    }
}
