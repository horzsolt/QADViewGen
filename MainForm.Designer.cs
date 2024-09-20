namespace QADBrowser
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            connectionTextField = new TextBox();
            goButton = new Button();
            errorTextBox = new TextBox();
            checkBox1 = new CheckBox();
            listViewResults = new ListView();
            genAlterViewButton = new Button();
            richTextBoxSQL = new RichTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 13);
            label1.Name = "label1";
            label1.Size = new Size(87, 20);
            label1.TabIndex = 1;
            label1.Text = "Connection:";
            // 
            // connectionTextField
            // 
            connectionTextField.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            connectionTextField.Location = new Point(117, 10);
            connectionTextField.Name = "connectionTextField";
            connectionTextField.Size = new Size(553, 27);
            connectionTextField.TabIndex = 2;
            // 
            // goButton
            // 
            goButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            goButton.Location = new Point(707, 11);
            goButton.Name = "goButton";
            goButton.Size = new Size(94, 29);
            goButton.TabIndex = 5;
            goButton.Text = "List views";
            goButton.UseVisualStyleBackColor = true;
            goButton.Click += goButton_Click;
            // 
            // errorTextBox
            // 
            errorTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            errorTextBox.Location = new Point(12, 472);
            errorTextBox.Name = "errorTextBox";
            errorTextBox.Size = new Size(776, 27);
            errorTextBox.TabIndex = 6;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(688, 16);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(18, 17);
            checkBox1.TabIndex = 7;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // listViewResults
            // 
            listViewResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listViewResults.Location = new Point(24, 53);
            listViewResults.Name = "listViewResults";
            listViewResults.Size = new Size(198, 368);
            listViewResults.TabIndex = 8;
            listViewResults.UseCompatibleStateImageBehavior = false;
            // 
            // genAlterViewButton
            // 
            genAlterViewButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            genAlterViewButton.Location = new Point(24, 435);
            genAlterViewButton.Name = "genAlterViewButton";
            genAlterViewButton.Size = new Size(198, 29);
            genAlterViewButton.TabIndex = 10;
            genAlterViewButton.Text = "Alter View";
            genAlterViewButton.UseVisualStyleBackColor = true;
            genAlterViewButton.Click += genAlterView_Click;
            // 
            // richTextBoxSQL
            // 
            richTextBoxSQL.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxSQL.Location = new Point(239, 53);
            richTextBoxSQL.Name = "richTextBoxSQL";
            richTextBoxSQL.Size = new Size(549, 368);
            richTextBoxSQL.TabIndex = 11;
            richTextBoxSQL.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(805, 505);
            Controls.Add(richTextBoxSQL);
            Controls.Add(genAlterViewButton);
            Controls.Add(listViewResults);
            Controls.Add(checkBox1);
            Controls.Add(errorTextBox);
            Controls.Add(goButton);
            Controls.Add(connectionTextField);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "ALTER VIEW";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox connectionTextField;
        private Button goButton;
        private TextBox errorTextBox;
        private CheckBox checkBox1;
        private ListView listViewResults;
        private Button genAlterViewButton;
        private RichTextBox richTextBoxSQL;
    }
}
