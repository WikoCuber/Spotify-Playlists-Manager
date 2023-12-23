namespace SPM_UI.Forms
{
    partial class TracksForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainPanel = new Panel();
            previousButton = new Button();
            nextButton = new Button();
            pageLabel = new Label();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.AutoScroll = true;
            mainPanel.Location = new Point(12, 75);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1160, 453);
            mainPanel.TabIndex = 0;
            // 
            // previousButton
            // 
            previousButton.Enabled = false;
            previousButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            previousButton.Location = new Point(12, 12);
            previousButton.Name = "previousButton";
            previousButton.Size = new Size(136, 42);
            previousButton.TabIndex = 6;
            previousButton.Text = "Poprzednia";
            previousButton.UseVisualStyleBackColor = true;
            previousButton.Click += previousButton_Click;
            // 
            // nextButton
            // 
            nextButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nextButton.Location = new Point(154, 12);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(136, 42);
            nextButton.TabIndex = 5;
            nextButton.Text = "Następna";
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += nextButton_Click;
            // 
            // pageLabel
            // 
            pageLabel.AutoSize = true;
            pageLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            pageLabel.Location = new Point(296, 20);
            pageLabel.Name = "pageLabel";
            pageLabel.Size = new Size(0, 25);
            pageLabel.TabIndex = 7;
            // 
            // TracksForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1184, 541);
            Controls.Add(pageLabel);
            Controls.Add(previousButton);
            Controls.Add(nextButton);
            Controls.Add(mainPanel);
            MaximizeBox = false;
            MinimumSize = new Size(1197, 200);
            Name = "TracksForm";
            Text = "Utwory w playliście: ";
            FormClosed += TracksForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel mainPanel;
        private Button previousButton;
        private Button nextButton;
        private Label pageLabel;
    }
}