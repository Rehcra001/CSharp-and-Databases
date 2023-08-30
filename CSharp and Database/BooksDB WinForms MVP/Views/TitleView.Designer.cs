namespace BooksDB_WinForms_MVP.Views
{
    partial class TitleView
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
            pnlHeader = new Panel();
            btnCloseTitleView = new Button();
            lblHeader = new Label();
            tabTitle = new TabControl();
            tabTitleDetailPage = new TabPage();
            cmbPublisherName = new ComboBox();
            label8 = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            txtComments = new TextBox();
            txtSubject = new TextBox();
            txtNotes = new TextBox();
            txtDescription = new TextBox();
            txtISBN = new TextBox();
            txtPubID = new TextBox();
            txtYear_Published = new TextBox();
            txtTitle = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            tabTitleListPage = new TabPage();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAddNew = new Button();
            dgrdTitleList = new DataGridView();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearchTitle = new Label();
            pnlHeader.SuspendLayout();
            tabTitle.SuspendLayout();
            tabTitleDetailPage.SuspendLayout();
            tabTitleListPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgrdTitleList).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(128, 255, 255);
            pnlHeader.Controls.Add(btnCloseTitleView);
            pnlHeader.Controls.Add(lblHeader);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(908, 44);
            pnlHeader.TabIndex = 0;
            // 
            // btnCloseTitleView
            // 
            btnCloseTitleView.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCloseTitleView.Location = new Point(809, 7);
            btnCloseTitleView.Name = "btnCloseTitleView";
            btnCloseTitleView.Size = new Size(39, 29);
            btnCloseTitleView.TabIndex = 1;
            btnCloseTitleView.Text = "X";
            btnCloseTitleView.UseVisualStyleBackColor = true;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblHeader.Location = new Point(12, 9);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(300, 25);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "SQL Books Database: Titles Table";
            // 
            // tabTitle
            // 
            tabTitle.Controls.Add(tabTitleDetailPage);
            tabTitle.Controls.Add(tabTitleListPage);
            tabTitle.Dock = DockStyle.Fill;
            tabTitle.Location = new Point(0, 44);
            tabTitle.Name = "tabTitle";
            tabTitle.SelectedIndex = 0;
            tabTitle.Size = new Size(908, 417);
            tabTitle.TabIndex = 1;
            // 
            // tabTitleDetailPage
            // 
            tabTitleDetailPage.BackColor = Color.Linen;
            tabTitleDetailPage.Controls.Add(cmbPublisherName);
            tabTitleDetailPage.Controls.Add(label8);
            tabTitleDetailPage.Controls.Add(btnCancel);
            tabTitleDetailPage.Controls.Add(btnSave);
            tabTitleDetailPage.Controls.Add(txtComments);
            tabTitleDetailPage.Controls.Add(txtSubject);
            tabTitleDetailPage.Controls.Add(txtNotes);
            tabTitleDetailPage.Controls.Add(txtDescription);
            tabTitleDetailPage.Controls.Add(txtISBN);
            tabTitleDetailPage.Controls.Add(txtPubID);
            tabTitleDetailPage.Controls.Add(txtYear_Published);
            tabTitleDetailPage.Controls.Add(txtTitle);
            tabTitleDetailPage.Controls.Add(label7);
            tabTitleDetailPage.Controls.Add(label6);
            tabTitleDetailPage.Controls.Add(label5);
            tabTitleDetailPage.Controls.Add(label4);
            tabTitleDetailPage.Controls.Add(label3);
            tabTitleDetailPage.Controls.Add(label2);
            tabTitleDetailPage.Controls.Add(label1);
            tabTitleDetailPage.ForeColor = SystemColors.ControlText;
            tabTitleDetailPage.Location = new Point(4, 30);
            tabTitleDetailPage.Name = "tabTitleDetailPage";
            tabTitleDetailPage.Padding = new Padding(3);
            tabTitleDetailPage.Size = new Size(900, 383);
            tabTitleDetailPage.TabIndex = 0;
            tabTitleDetailPage.Text = "Title Detail";
            // 
            // cmbPublisherName
            // 
            cmbPublisherName.FormattingEnabled = true;
            cmbPublisherName.Location = new Point(353, 109);
            cmbPublisherName.Name = "cmbPublisherName";
            cmbPublisherName.Size = new Size(515, 29);
            cmbPublisherName.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(33, 117);
            label8.Name = "label8";
            label8.Size = new Size(94, 21);
            label8.TabIndex = 15;
            label8.Text = "Publisher ID";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(527, 348);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(282, 348);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 14;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // txtComments
            // 
            txtComments.Location = new Point(163, 289);
            txtComments.Multiline = true;
            txtComments.Name = "txtComments";
            txtComments.ScrollBars = ScrollBars.Vertical;
            txtComments.Size = new Size(705, 53);
            txtComments.TabIndex = 13;
            // 
            // txtSubject
            // 
            txtSubject.Location = new Point(163, 246);
            txtSubject.Name = "txtSubject";
            txtSubject.Size = new Size(705, 29);
            txtSubject.TabIndex = 12;
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(163, 203);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(705, 29);
            txtNotes.TabIndex = 11;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(163, 160);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(705, 29);
            txtDescription.TabIndex = 10;
            // 
            // txtISBN
            // 
            txtISBN.Location = new Point(353, 64);
            txtISBN.Name = "txtISBN";
            txtISBN.Size = new Size(515, 29);
            txtISBN.TabIndex = 9;
            // 
            // txtPubID
            // 
            txtPubID.Location = new Point(163, 109);
            txtPubID.Name = "txtPubID";
            txtPubID.Size = new Size(122, 29);
            txtPubID.TabIndex = 8;
            // 
            // txtYear_Published
            // 
            txtYear_Published.Location = new Point(163, 64);
            txtYear_Published.Name = "txtYear_Published";
            txtYear_Published.Size = new Size(122, 29);
            txtYear_Published.TabIndex = 8;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(163, 23);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(707, 29);
            txtTitle.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(33, 293);
            label7.Name = "label7";
            label7.Size = new Size(86, 21);
            label7.TabIndex = 6;
            label7.Text = "Comments";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(33, 250);
            label6.Name = "label6";
            label6.Size = new Size(61, 21);
            label6.TabIndex = 5;
            label6.Text = "Subject";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 207);
            label5.Name = "label5";
            label5.Size = new Size(51, 21);
            label5.TabIndex = 4;
            label5.Text = "Notes";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 164);
            label4.Name = "label4";
            label4.Size = new Size(89, 21);
            label4.TabIndex = 3;
            label4.Text = "Description";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(291, 68);
            label3.Name = "label3";
            label3.Size = new Size(44, 21);
            label3.TabIndex = 2;
            label3.Text = "ISBN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 68);
            label2.Name = "label2";
            label2.Size = new Size(112, 21);
            label2.TabIndex = 1;
            label2.Text = "Year Published";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 27);
            label1.Name = "label1";
            label1.Size = new Size(39, 21);
            label1.TabIndex = 0;
            label1.Text = "Title";
            // 
            // tabTitleListPage
            // 
            tabTitleListPage.BackColor = Color.Linen;
            tabTitleListPage.Controls.Add(btnDelete);
            tabTitleListPage.Controls.Add(btnEdit);
            tabTitleListPage.Controls.Add(btnAddNew);
            tabTitleListPage.Controls.Add(dgrdTitleList);
            tabTitleListPage.Controls.Add(btnSearch);
            tabTitleListPage.Controls.Add(txtSearch);
            tabTitleListPage.Controls.Add(lblSearchTitle);
            tabTitleListPage.Location = new Point(4, 24);
            tabTitleListPage.Name = "tabTitleListPage";
            tabTitleListPage.Padding = new Padding(3);
            tabTitleListPage.Size = new Size(900, 389);
            tabTitleListPage.TabIndex = 1;
            tabTitleListPage.Text = "Title List";
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(748, 215);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(84, 29);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEdit.Location = new Point(748, 147);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(84, 29);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAddNew
            // 
            btnAddNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddNew.Location = new Point(748, 79);
            btnAddNew.Name = "btnAddNew";
            btnAddNew.Size = new Size(84, 29);
            btnAddNew.TabIndex = 4;
            btnAddNew.Text = "Add New";
            btnAddNew.UseVisualStyleBackColor = true;
            // 
            // dgrdTitleList
            // 
            dgrdTitleList.AllowUserToAddRows = false;
            dgrdTitleList.AllowUserToDeleteRows = false;
            dgrdTitleList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgrdTitleList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgrdTitleList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgrdTitleList.Location = new Point(19, 79);
            dgrdTitleList.MultiSelect = false;
            dgrdTitleList.Name = "dgrdTitleList";
            dgrdTitleList.ReadOnly = true;
            dgrdTitleList.RowTemplate.Height = 25;
            dgrdTitleList.Size = new Size(677, 212);
            dgrdTitleList.TabIndex = 3;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(621, 39);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Location = new Point(19, 39);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(596, 29);
            txtSearch.TabIndex = 1;
            // 
            // lblSearchTitle
            // 
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Location = new Point(19, 15);
            lblSearchTitle.Name = "lblSearchTitle";
            lblSearchTitle.Size = new Size(90, 21);
            lblSearchTitle.TabIndex = 0;
            lblSearchTitle.Text = "Search Title";
            // 
            // TitleView
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(908, 461);
            Controls.Add(tabTitle);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4);
            Name = "TitleView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TitleView";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tabTitle.ResumeLayout(false);
            tabTitleDetailPage.ResumeLayout(false);
            tabTitleDetailPage.PerformLayout();
            tabTitleListPage.ResumeLayout(false);
            tabTitleListPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgrdTitleList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeader;
        private TabControl tabTitle;
        private TabPage tabTitleListPage;
        private Label lblSearchTitle;
        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridView dgrdTitleList;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnAddNew;
        private TabPage tabTitleDetailPage;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtComments;
        private TextBox txtSubject;
        private TextBox txtNotes;
        private TextBox txtDescription;
        private TextBox txtISBN;
        private TextBox txtYear_Published;
        private TextBox txtTitle;
        private Button btnCancel;
        private Button btnSave;
        private Label label8;
        private TextBox txtPubID;
        private Button btnCloseTitleView;
        private ComboBox cmbPublisherName;
    }
}