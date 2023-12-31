﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksDB_WinForms_MVP.Views
{
    public partial class TitleView : Form, ITitleView
    {
        private bool _isEdit;
        private bool _isSuccessful;
        private string _message;

        public TitleView()
        {
            InitializeComponent();
            AnnotateAndRaiseEvents();
            tabTitle.TabPages.Remove(tabTitleDetailPage);
            btnCloseTitleView.Click += delegate { this.Close(); };
        }

        private void AnnotateAndRaiseEvents()
        {
            //Search Event
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };

            //Add Event
            btnAddNew.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabTitle.TabPages.Remove(tabTitleListPage);
                tabTitle.TabPages.Add(tabTitleDetailPage);
                tabTitleDetailPage.Text = "Add new title";
            };

            //Edit Event
            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabTitle.TabPages.Remove(tabTitleListPage);
                tabTitle.TabPages.Add(tabTitleDetailPage);
                tabTitleDetailPage.Text = "Edit title";
            };

            //Save Event
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (_isSuccessful)
                {
                    tabTitle.TabPages.Remove(tabTitleDetailPage);
                    tabTitle.TabPages.Add(tabTitleListPage);
                }
                MessageBox.Show(Message);
            };

            //Cancel Event
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabTitle.TabPages.Remove(tabTitleDetailPage);
                tabTitle.TabPages.Add(tabTitleListPage);
            };

            //Delete Event
            btnDelete.Click += delegate
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                      "Delete Record",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question,
                                                      MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            btnAddAuthor.Click += delegate
            {
                AddAuthorEvent?.Invoke(this, EventArgs.Empty);
            };

            btnRemoveAuthor.Click += delegate
            {
                RemoveAuthorEvent?.Invoke(this, EventArgs.Empty);
            };
        }


        public string Title { get => txtTitle.Text; set => txtTitle.Text = value; }
        public string Year_Published { get => txtYear_Published.Text; set => txtYear_Published.Text = value; }
        public string ISBN { get => txtISBN.Text; set => txtISBN.Text = value; }
        public string PubID { get => txtPubID.Text; set => txtPubID.Text = value; }
        public string Description { get => txtDescription.Text; set => txtDescription.Text = value; }
        public string Notes { get => txtNotes.Text; set => txtNotes.Text = value; }
        public string Subject { get => txtSubject.Text; set => txtSubject.Text = value; }
        public string Comments { get => txtComments.Text; set => txtComments.Text = value; }
        public string SearchValue { get => txtSearch.Text; set => txtSearch.Text = value; }
        public bool IsEdit { get => _isEdit; set => _isEdit = value; }
        public bool IsSuccessful { get => _isSuccessful; set => _isSuccessful = value; }
        public string Message { get => _message; set => _message = value; }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler AddAuthorEvent;
        public event EventHandler RemoveAuthorEvent;

        public void SetTitleBindingSource(BindingSource titleList, BindingSource publisherList, 
                                          BindingSource authorList, BindingSource selectedAuthorList)
        {
            dgrdTitleList.DataSource = titleList;
            cmbPublisherName.DataSource = publisherList;
            cmbPublisherName.DisplayMember = "Name";
            cmbPublisherName.ValueMember = "PubID";
            cmbPublisherName.DataBindings.Add("SelectedValue", titleList, "PubID");
            txtPubID.DataBindings.Add("Text", publisherList, "PubID");
            cmbAuthorList.DataSource = authorList;
            cmbAuthorList.DisplayMember = "Author";
            lstAuthors.DataSource = selectedAuthorList;
            lstAuthors.DisplayMember = "Author";
        }

        public static TitleView? _instance;
        public static TitleView GetInstance(Form parentContainer)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new TitleView();
                _instance.MdiParent = parentContainer;
                _instance.FormBorderStyle = FormBorderStyle.None;
                _instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (_instance.WindowState == FormWindowState.Minimized)
                {
                    _instance.WindowState = FormWindowState.Normal;
                }
                _instance.BringToFront();
            }
            return _instance;
        }
    }
}
