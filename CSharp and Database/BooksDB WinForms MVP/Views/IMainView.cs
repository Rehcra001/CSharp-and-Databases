using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB_WinForms_MVP.Views
{
    public interface IMainView
    {
        event EventHandler ShowTitleView;
        event EventHandler ShowPublisherView;
        event EventHandler ShowAuthorView;
    }
}
