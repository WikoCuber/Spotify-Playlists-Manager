namespace SPM_UI.Forms
{
    partial class CompleteForm
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
            playButton = new Button();
            albumPictureBox = new PictureBox();
            titleLabel = new Label();
            addButton = new Button();
            playlistLabel = new Label();
            playlistComboBox = new ComboBox();
            progressLabel = new Label();
            warningLabel = new Label();
            noneButton = new Button();
            ((System.ComponentModel.ISupportInitialize)albumPictureBox).BeginInit();
            SuspendLayout();
            // 
            // playButton
            // 
            playButton.Location = new Point(21, 93);
            playButton.Name = "playButton";
            playButton.Size = new Size(30, 30);
            playButton.TabIndex = 0;
            playButton.UseVisualStyleBackColor = true;
            // 
            // albumPictureBox
            // 
            albumPictureBox.Location = new Point(175, 12);
            albumPictureBox.Name = "albumPictureBox";
            albumPictureBox.Size = new Size(64, 64);
            albumPictureBox.TabIndex = 1;
            albumPictureBox.TabStop = false;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(63, 100);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(0, 15);
            titleLabel.TabIndex = 2;
            // 
            // addButton
            // 
            addButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            addButton.Location = new Point(316, 148);
            addButton.Name = "addButton";
            addButton.Size = new Size(76, 33);
            addButton.TabIndex = 3;
            addButton.Text = "Dodaj";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // playlistLabel
            // 
            playlistLabel.AutoSize = true;
            playlistLabel.Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point);
            playlistLabel.Location = new Point(10, 153);
            playlistLabel.Name = "playlistLabel";
            playlistLabel.Size = new Size(75, 23);
            playlistLabel.TabIndex = 4;
            playlistLabel.Text = "Playlista:";
            // 
            // playlistComboBox
            // 
            playlistComboBox.FormattingEnabled = true;
            playlistComboBox.Location = new Point(91, 153);
            playlistComboBox.Name = "playlistComboBox";
            playlistComboBox.Size = new Size(137, 23);
            playlistComboBox.TabIndex = 5;
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point);
            progressLabel.Location = new Point(18, 9);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(35, 23);
            progressLabel.TabIndex = 6;
            progressLabel.Text = "0/0";
            // 
            // warningLabel
            // 
            warningLabel.AutoSize = true;
            warningLabel.Location = new Point(13, 187);
            warningLabel.Name = "warningLabel";
            warningLabel.Size = new Size(0, 15);
            warningLabel.TabIndex = 7;
            // 
            // noneButton
            // 
            noneButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            noneButton.Location = new Point(234, 148);
            noneButton.Name = "noneButton";
            noneButton.Size = new Size(76, 33);
            noneButton.TabIndex = 8;
            noneButton.Text = "Żadna";
            noneButton.UseVisualStyleBackColor = true;
            noneButton.Click += noneButton_Click;
            // 
            // CompleteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(404, 222);
            Controls.Add(noneButton);
            Controls.Add(warningLabel);
            Controls.Add(progressLabel);
            Controls.Add(playlistComboBox);
            Controls.Add(playlistLabel);
            Controls.Add(addButton);
            Controls.Add(titleLabel);
            Controls.Add(albumPictureBox);
            Controls.Add(playButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "CompleteForm";
            Text = "Dodaj utwór";
            FormClosed += CompleteForm_FormClosed;
            ((System.ComponentModel.ISupportInitialize)albumPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button playButton;
        private PictureBox albumPictureBox;
        private Label titleLabel;
        private Button addButton;
        private Label playlistLabel;
        private ComboBox playlistComboBox;
        private Label progressLabel;
        private Label warningLabel;
        private Button noneButton;
    }
}