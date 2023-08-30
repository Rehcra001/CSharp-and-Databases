namespace BooksDB_WinForms_MVP.Views
{
    partial class MainView
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
            pnlMenu = new Panel();
            btnOpenPublishersView = new Button();
            btnOpenTitleView = new Button();
            pnlMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.Controls.Add(btnOpenPublishersView);
            pnlMenu.Controls.Add(btnOpenTitleView);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Margin = new Padding(4);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(139, 690);
            pnlMenu.TabIndex = 0;
            // 
            // btnOpenPublishersView
            // 
            btnOpenPublishersView.Location = new Point(10, 99);
            btnOpenPublishersView.Margin = new Padding(4);
            btnOpenPublishersView.Name = "btnOpenPublishersView";
            btnOpenPublishersView.Size = new Size(122, 32);
            btnOpenPublishersView.TabIndex = 0;
            btnOpenPublishersView.Text = "Publishers";
            btnOpenPublishersView.UseVisualStyleBackColor = true;
            // 
            // btnOpenTitleView
            // 
            btnOpenTitleView.Location = new Point(10, 35);
            btnOpenTitleView.Margin = new Padding(4);
            btnOpenTitleView.Name = "btnOpenTitleView";
            btnOpenTitleView.Size = new Size(122, 32);
            btnOpenTitleView.TabIndex = 0;
            btnOpenTitleView.Text = "Titles";
            btnOpenTitleView.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1097, 690);
            Controls.Add(pnlMenu);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            IsMdiContainer = true;
            Margin = new Padding(4);
            Name = "MainView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainView";
            pnlMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
        private Button btnOpenTitleView;
        private Button btnOpenPublishersView;
    }
}