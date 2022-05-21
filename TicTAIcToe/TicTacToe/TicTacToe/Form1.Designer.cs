
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1: Form
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
            this.Input = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.winLabel = new System.Windows.Forms.Label();
            this.historyBox = new System.Windows.Forms.GroupBox();
            this.historyLabel = new System.Windows.Forms.Label();
            this.Input.SuspendLayout();
            this.historyBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Input
            // 
            this.Input.Controls.Add(this.button9);
            this.Input.Controls.Add(this.button8);
            this.Input.Controls.Add(this.button7);
            this.Input.Controls.Add(this.button6);
            this.Input.Controls.Add(this.button5);
            this.Input.Controls.Add(this.button4);
            this.Input.Controls.Add(this.button3);
            this.Input.Controls.Add(this.button2);
            this.Input.Controls.Add(this.button1);
            this.Input.Location = new System.Drawing.Point(52, 33);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(584, 376);
            this.Input.TabIndex = 0;
            this.Input.TabStop = false;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(333, 221);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(79, 79);
            this.button9.TabIndex = 8;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.inputButtonClicked);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(248, 221);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(79, 79);
            this.button8.TabIndex = 7;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.inputButtonClicked);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(163, 221);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(79, 79);
            this.button7.TabIndex = 6;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.inputButtonClicked);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(333, 136);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(79, 79);
            this.button6.TabIndex = 5;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.inputButtonClicked);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(248, 136);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(79, 79);
            this.button5.TabIndex = 4;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.inputButtonClicked);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(163, 136);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(79, 79);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.inputButtonClicked);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(333, 51);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 79);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.inputButtonClicked);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(248, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 79);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.inputButtonClicked);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 79);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.inputButtonClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(643, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Teraz kolej gracza:";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(643, 373);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(100, 35);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetGame);
            // 
            // winLabel
            // 
            this.winLabel.AutoSize = true;
            this.winLabel.Location = new System.Drawing.Point(643, 116);
            this.winLabel.Name = "winLabel";
            this.winLabel.Size = new System.Drawing.Size(66, 15);
            this.winLabel.TabIndex = 3;
            this.winLabel.Text = "WYGRANE:";
            // 
            // historyBox
            // 
            this.historyBox.Controls.Add(this.historyLabel);
            this.historyBox.Location = new System.Drawing.Point(643, 169);
            this.historyBox.Name = "historyBox";
            this.historyBox.Size = new System.Drawing.Size(145, 164);
            this.historyBox.TabIndex = 4;
            this.historyBox.TabStop = false;
            this.historyBox.Text = "HISTORIA RUCHÓW";
            // 
            // historyLabel
            // 
            this.historyLabel.AutoSize = true;
            this.historyLabel.Location = new System.Drawing.Point(41, 19);
            this.historyLabel.MaximumSize = new System.Drawing.Size(50, 140);
            this.historyLabel.Name = "historyLabel";
            this.historyLabel.Size = new System.Drawing.Size(43, 15);
            this.historyLabel.TabIndex = 0;
            this.historyLabel.Text = "X: (0,2)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.historyBox);
            this.Controls.Add(this.winLabel);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Input);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Input.ResumeLayout(false);
            this.historyBox.ResumeLayout(false);
            this.historyBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox Input;
        private Button button1;
        private Label label1;
        private Button button9;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button resetButton;
        private Label winLabel;
        private GroupBox historyBox;
        private Label historyLabel;
    }
}

