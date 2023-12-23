namespace SPM_UI.Forms
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
            playlistsPanel = new Panel();
            playlistsLabel = new Label();
            completeButton = new Button();
            createButton = new Button();
            SuspendLayout();
            // 
            // playlistsPanel
            // 
            playlistsPanel.AutoSize = true;
            playlistsPanel.Location = new Point(12, 75);
            playlistsPanel.Name = "playlistsPanel";
            playlistsPanel.Size = new Size(770, 378);
            playlistsPanel.TabIndex = 0;
            // 
            // playlistsLabel
            // 
            playlistsLabel.AutoSize = true;
            playlistsLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            playlistsLabel.Location = new Point(347, 12);
            playlistsLabel.Name = "playlistsLabel";
            playlistsLabel.Size = new Size(113, 37);
            playlistsLabel.TabIndex = 1;
            playlistsLabel.Text = "Playlisty";
            // 
            // completeButton
            // 
            completeButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            completeButton.Location = new Point(585, 12);
            completeButton.Name = "completeButton";
            completeButton.Size = new Size(197, 42);
            completeButton.TabIndex = 2;
            completeButton.Text = "Uzupełnij z polubionych";
            completeButton.UseVisualStyleBackColor = true;
            completeButton.Click += completeButton_Click;
            // 
            // createButton
            // 
            createButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            createButton.Location = new Point(12, 12);
            createButton.Name = "createButton";
            createButton.Size = new Size(197, 42);
            createButton.TabIndex = 3;
            createButton.Text = "Utwórz playlistę";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(792, 465);
            Controls.Add(createButton);
            Controls.Add(completeButton);
            Controls.Add(playlistsLabel);
            Controls.Add(playlistsPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Spotify Playlist Manager";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel playlistsPanel;
        private Label playlistsLabel;
        private Button completeButton;
        private Button createButton;
    }
}