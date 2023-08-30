using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB_WinForms_MVP.Views
{
    public interface IPublisherView
    {
        //Properties
        string SearchValue { get; set; }
        string AppState { get; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler FirstRecordEvent;
        event EventHandler PreviousRecordEvent;
        event EventHandler NextRecordEvent;
        event EventHandler LastRecordEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler DeleteEvent;
        event EventHandler SearchEvent;

        //Methods 
        void SetPublisherBindingSource(BindingSource publisherList);
        void SetViewState(string viewState);
        void Show();//Optional
    }
}
