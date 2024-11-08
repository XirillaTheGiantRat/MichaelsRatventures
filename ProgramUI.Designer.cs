using System;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Windows.Forms;

namespace michaelsparty
{
    partial class ProgramUI : Form
    {
        private System.ComponentModel.IContainer components = null;

        private PreMadePrograms preMadePrograms = new PreMadePrograms();
        public ImportFile importFile = new ImportFile();
        private ICharacter character = new Character((0, 0));
        private Grid commandGrid;

        private Label titleLabel;
        private ComboBox comboBox1;
        private Label label2;
        private Label metricsLabel;
        private Button runButton;
        private Button metricsButton;
        private TextBox userInputTextBox; 
        private Button checkButton;
        private Button PathFinderButton;
        string commandString;
        private List<Commands> commandList = new List<Commands>();
        private WriteProgram writeProgram = new WriteProgram();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        protected void InitializeComponent()
        {
            commandGrid = new Grid(15, character);

            titleLabel = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            runButton = new Button();
            metricsButton = new Button();
            metricsLabel = new Label();
            userInputTextBox = new TextBox();
            checkButton = new Button();
            PathFinderButton = new Button();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleLabel.Location = new Point(929, 71);
            titleLabel.Margin = new Padding(5, 0, 5, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(719, 63);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "MICHAELS RATVENTURES";
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.PaleVioletRed;
            comboBox1.FlatStyle = FlatStyle.Popup;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Basic", "Advanced", "Expert", "Import File", "Empty Program" });
            comboBox1.Location = new Point(260, 87);
            comboBox1.Margin = new Padding(5, 4, 5, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(282, 40);
            comboBox1.TabIndex = 1;
            comboBox1.Text = "Other program";
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(2257, 740);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(0, 29);
            label2.TabIndex = 2;
            // 
            // runButton
            // 
            runButton.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            runButton.Location = new Point(597, 852);
            runButton.Margin = new Padding(4);
            runButton.Name = "runButton";
            runButton.Size = new Size(318, 192);
            runButton.TabIndex = 5;
            runButton.Text = "Run";
            runButton.Click += button1_Click;
            // 
            // metricsButton
            // 
            metricsButton.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            metricsButton.Location = new Point(950, 852);
            metricsButton.Margin = new Padding(4);
            metricsButton.Name = "metricsButton";
            metricsButton.Size = new Size(333, 192);
            metricsButton.TabIndex = 5;
            metricsButton.Text = "Metrics";
            metricsButton.Click += button2_Click;
            // 
            // metricsLabel
            // 
            metricsLabel.AutoSize = true;
            metricsLabel.Location = new Point(1400, 900);
            metricsLabel.Name = "metricsLabel";
            metricsLabel.Size = new Size(0, 32);
            metricsLabel.TabIndex = 9;
            // 
            // userInputTextBox
            // 
            userInputTextBox.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            userInputTextBox.Location = new Point(260, 200);
            userInputTextBox.Margin = new Padding(4);
            userInputTextBox.Multiline = true;
            userInputTextBox.Name = "userInputTextBox";
            userInputTextBox.Size = new Size(1023, 605);
            userInputTextBox.TabIndex = 7;
            // 
            // checkButton
            // 
            checkButton.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkButton.Location = new Point(260, 852);
            checkButton.Margin = new Padding(4);
            checkButton.Name = "checkButton";
            checkButton.Size = new Size(291, 192);
            checkButton.TabIndex = 8;
            checkButton.Text = "Check";
            checkButton.Click += CheckButton_Click;
            // 
            // PathFinderButton
            // 
            PathFinderButton.BackColor = Color.Transparent;
            PathFinderButton.Location = new Point(614, 87);
            PathFinderButton.Name = "button1";
            PathFinderButton.Size = new Size(240, 46);
            PathFinderButton.TabIndex = 10;
            PathFinderButton.Text = "Path Finder";
            PathFinderButton.Click += PathFinderButton_Click;
            // 
            // ProgramUI
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Pink;
            ClientSize = new Size(2257, 1132);
            Controls.Add(PathFinderButton);
            Controls.Add(runButton);
            Controls.Add(metricsButton);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(titleLabel);
            Controls.Add(userInputTextBox);
            Controls.Add(checkButton);
            Controls.Add(metricsLabel);
            Margin = new Padding(5, 4, 5, 4);

            Controls.Add(commandGrid);
            commandGrid.Location = new Point(1400, 200);

            Name = "ProgramUI";
            Text = "HAPPY BIRTHDAY MICHAEL";
            Load += ProgramUI_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            commandGrid.ResetGrid();
            commandString = userInputTextBox.Text;
            commandList = writeProgram.GenerateCommandsFromInput(commandString, (Character)character);

            if (commandList.Count == 0)
            {
                MessageBox.Show("Michael is sleeping (please provide some input or select one of the premade programs)");
            }
            else
            {
                commandGrid.ExecuteCommands(commandList);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(commandString))
            {
                MessageBox.Show("Please first select/write a program before calculating metrics");
                return;
            }

            List<Metrics> metricsList = new List<Metrics>
            {
                new NumberOfCommands(),
                new NumberOfRepeats(),
                new MAXNesting(),
            };

            StringBuilder metricsResult = new StringBuilder(); 

            foreach (var metric in metricsList)
            {
                int result = metric.CalculateMetric(commandString);
                metricsResult.AppendLine($"{metric.GetType().Name}: {result}");
            }
            metricsLabel.Text = metricsResult.ToString();
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            commandString = userInputTextBox.Text;
            commandList = writeProgram.GenerateCommandsFromInput(commandString, (Character)character);

            if (commandList.Count == 0)
            {
                MessageBox.Show("Invalid input. Please consult one of the premade programs (basic/advanced/expert) or the giant RAT (that makes all of the rules)");
            }
            else
            {
                MessageBox.Show("Michael approves. Let the adventure begin!!");
                commandList = CommandExecutor.GenerateCommandInstances(commandString, (Character)character);
            }
        }

        #endregion

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            commandString = ""; 
            metricsLabel.Text = "";
            commandList = new List<Commands>();
            commandGrid.ResetGrid(); 
            switch (selectedItem)
            {
                case "Basic":
                    commandString = preMadePrograms.ChooseBasicProgram();
                    break;
                case "Advanced":
                    commandString = preMadePrograms.ChooseAdvancedProgram();
                    break;
                case "Expert":
                    commandString = preMadePrograms.ChooseExpertProgram();
                    break;
                case "Import File":
                    commandString = importFile.FileToString(); 

                    if (string.IsNullOrEmpty(commandString))
                    {
                        MessageBox.Show("Failed to import file. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "Empty Program":
                    commandString = "";
                    userInputTextBox.Text = ""; 
                    return;  
            }

            if (!string.IsNullOrEmpty(commandString))
            {
                commandList = CommandExecutor.GenerateCommandInstances(commandString, (Character)character);
                userInputTextBox.Text = commandString; 
            }
        }

        

        private void PathFinderButton_Click(object sender, EventArgs e)
        {
            PathFinder pathFinder = new PathFinder(); 
            string filePath = pathFinder.OpenFileDialog();

            if (!string.IsNullOrEmpty(filePath))
            {
                var gridData = pathFinder.LoadGridFromFile(filePath);

                if (gridData != null)
                {
                    commandGrid.LoadAndDisplayGrid(gridData);
                }
            }
            else
            {
                MessageBox.Show("No grid selected");
            }

        }
        
    }
}