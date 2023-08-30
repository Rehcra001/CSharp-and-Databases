using System;
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
    public partial class PublisherView : Form, IPublisherView
    {
        public PublisherView()
        {
            InitializeComponent();
            AnnotateAndRaiseEvents();
            btnClose.Click += delegate { this.Close(); };
        }

        private void AnnotateAndRaiseEvents()
        {
            //First record event
            btnFirstRecord.Click += delegate
            {
                FirstRecordEvent?.Invoke(this, EventArgs.Empty);                
            };

            //Previous record event
            btnPreviousRecord.Click += delegate
            {
                PreviousRecordEvent?.Invoke(this, EventArgs.Empty);
            };

            //Next record event
            btnNextRecord.Click += delegate
            {
                NextRecordEvent?.Invoke(this, EventArgs.Empty);
            };

            //Last record event
            btnLastRecord.Click += delegate
            {
                LastRecordEvent?.Invoke(this, EventArgs.Empty);
            };

            //search event
            btnSearch.Click += delegate
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnAddNew.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
            };

            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
            };

            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
            };

            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
            };

            btnDelete.Click += delegate
            {
                DeleteEvent?.Invoke(this, EventArgs.Empty);
                txtSearch.Clear();
            };
        }

        public string SearchValue { get => txtSearch.Text; set => txtSearch.Text = value; }
        public bool IsSuccessful { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AppState { get => _viewState; private set => _viewState = value; }

        public event EventHandler FirstRecordEvent;
        public event EventHandler PreviousRecordEvent;
        public event EventHandler NextRecordEvent;
        public event EventHandler LastRecordEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SearchEvent;

        public void SetPublisherBindingSource(BindingSource publisherList)
        {
            txtPubID.DataBindings.Add("Text", publisherList, "PubID");
            txtName.DataBindings.Add("Text", publisherList, "Name");
            txtCompanyName.DataBindings.Add("Text", publisherList, "Company_name");
            txtAddress.DataBindings.Add("Text", publisherList, "Address");
            txtCity.DataBindings.Add("Text", publisherList, "City");
            txtState.DataBindings.Add("Text", publisherList, "State");
            txtZip.DataBindings.Add("Text", publisherList, "Zip");
            txtTelephone.DataBindings.Add("Text", publisherList, "Telephone");
            txtFax.DataBindings.Add("Text", publisherList, "Fax");
            txtComments.DataBindings.Add("Text", publisherList, "Comments");
        }

        public void SetViewState(string appState)
        {
            AppState = appState;

            switch (AppState)
            {
                case "View":
                    txtPubID.ReadOnly = true;
                    txtName.ReadOnly = true;
                    txtCompanyName.ReadOnly = true;
                    txtAddress.ReadOnly = true;
                    txtCity.ReadOnly = true;
                    txtState.ReadOnly = true;
                    txtZip.ReadOnly = true;
                    txtTelephone.ReadOnly = true;
                    txtFax.ReadOnly = true;
                    txtComments.ReadOnly = true;

                    txtSearch.ReadOnly = false;

                    btnFirstRecord.Enabled = true;
                    btnPreviousRecord.Enabled = true;
                    btnNextRecord.Enabled = true;
                    btnLastRecord.Enabled = true;
                    btnAddNew.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;

                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    break;

                default:
                    txtPubID.ReadOnly = false;
                    txtName.ReadOnly = false;
                    txtCompanyName.ReadOnly = false;
                    txtAddress.ReadOnly = false;
                    txtCity.ReadOnly = false;
                    txtState.ReadOnly = false;
                    txtZip.ReadOnly = false;
                    txtTelephone.ReadOnly = false;
                    txtFax.ReadOnly = false;
                    txtComments.ReadOnly = false;

                    txtSearch.ReadOnly = true;

                    btnFirstRecord.Enabled = false;
                    btnPreviousRecord.Enabled = false;
                    btnNextRecord.Enabled = false;
                    btnLastRecord.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
            }
        }

        //Add this view as a singleton childview of MainView
        public static PublisherView? _instance;
        private string _viewState;

        public static PublisherView GetInstance(Form parentControl)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new PublisherView();
                _instance.MdiParent = parentControl;
                _instance.FormBorderStyle = FormBorderStyle.None;
                _instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (_instance.WindowState == FormWindowState.Minimized || _instance != null)
                {
                    _instance.WindowState = FormWindowState.Normal;
                }
                _instance!.BringToFront();
            }
            return _instance;
        }


    }
}
