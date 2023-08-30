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
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            btnOpenTitleView.Click += delegate { ShowTitleView?.Invoke(this, EventArgs.Empty); };
            btnOpenPublishersView.Click += delegate { ShowPublisherView?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler ShowTitleView;
        public event EventHandler ShowPublisherView;
        public event EventHandler ShowAuthorView;
    }
}
