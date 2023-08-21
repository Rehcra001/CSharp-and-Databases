using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB_WinForms_MVP.Views
{
    public interface ITitleView
    {
        //Properties
        string Title { get; set; }
        string Year_Published { get; set; }
        string ISBN { get; set; }
        string PubID { get; set; }
        string Description { get; set; }
        string Notes { get; set; }
        string Subject { get; set; }
        string Comment { get; set; }
        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        //Methods
        void SetTitleBindingSource(BindingSource titleList);
    }
}
