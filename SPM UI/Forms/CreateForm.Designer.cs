namespace SPM_UI.Forms
{
    partial class CreateForm
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
            playlistLabel = new Label();
            createButton = new Button();
            playlistComboBox = new ComboBox();
            SuspendLayout();
            // 
            // playlistLabel
            // 
            playlistLabel.AutoSize = true;
            playlistLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            playlistLabel.Location = new Point(12, 9);
            playlistLabel.Name = "playlistLabel";
            playlistLabel.Size = new Size(169, 25);
            playlistLabel.TabIndex = 0;
            playlistLabel.Text = "Utwórz playlistę z: ";
            // 
            // createButton
            // 
            createButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            createButton.Location = new Point(132, 41);
            createButton.Name = "createButton";
            createButton.Size = new Size(87, 36);
            createButton.TabIndex = 1;
            createButton.Text = "Utwórz";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // playlistComboBox
            // 
            playlistComboBox.FormattingEnabled = true;
            playlistComboBox.Location = new Point(174, 12);
            playlistComboBox.Name = "playlistComboBox";
            playlistComboBox.Size = new Size(167, 23);
            playlistComboBox.TabIndex = 2;
            // 
            // CreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 95);
            Controls.Add(playlistComboBox);
            Controls.Add(createButton);
            Controls.Add(playlistLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "CreateForm";
            Text = "Utwórz playlistę";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label playlistLabel;
        private Button createButton;
        private ComboBox playlistComboBox;
    }
}