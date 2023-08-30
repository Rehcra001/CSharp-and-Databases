using BooksDB_WinForms_MVP.Presenters.Common;
using BooksDB_WinForms_MVP.Views;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB_WinForms_MVP.Presenters
{
    public class PublisherPresenter
    {
        private IPublisherView _view;
        private IPublisherRepository _publisherRepository;
        private BindingSource _publisherBindingSource;
        private List<PublisherModel> _publisherList;
        private int _bookMark = 0;

        public PublisherPresenter(IPublisherView view, IPublisherRepository publisherRepository)
        {
            _publisherBindingSource = new BindingSource();
            _view = view;
            _publisherRepository = publisherRepository;
            _view.SetPublisherBindingSource(_publisherBindingSource);
            _publisherList = _publisherRepository.GetAll().OrderBy(x => x.Name).ToList();
            _publisherBindingSource.DataSource = _publisherList;

            //Subscribe event handler methods to view events
            this._view.FirstRecordEvent += FirstRecord;
            this._view.PreviousRecordEvent += PreviousRecord;
            this._view.NextRecordEvent += NextRecord;
            this._view.LastRecordEvent += LastRecord;
            this._view.SearchEvent += SearchPublisherName;
            this._view.AddNewEvent += AddNew;
            this._view.EditEvent += EditPublisher;
            this._view.CancelEvent += CancelAction;
            this._view.SaveEvent += SavePublisher;
            this._view.DeleteEvent += DeletePublisher;

            this._view.SetViewState("View");
            this._view.Show();
        }

        private void DeletePublisher(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this publisher?",
                                                  "Delete Publisher",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning,
                                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                PublisherModel publisher = (PublisherModel)_publisherBindingSource.Current;
                _bookMark = _publisherBindingSource.CurrencyManager.Position;
                try
                {
                    string errorMessage = _publisherRepository.Delete(publisher.PubID);

                    if (errorMessage != null)
                    {
                        MessageBox.Show(errorMessage);
                        return;
                    }
                    else
                    {
                        _publisherList = _publisherRepository.GetAll().OrderBy(x => x.Name).ToList();
                        _publisherBindingSource.DataSource = _publisherList;
                        MessageBox.Show("Publisher Deleted!");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                return;
            }
            
        }

        private void SavePublisher(object? sender, EventArgs e)
        {
            PublisherModel publisher = (PublisherModel)this._publisherBindingSource.Current;
            try
            {
                new ModelValidation().Validate(publisher);
                if (this._view.AppState.Equals("Add"))
                {
                    string errorMessage = _publisherRepository.Add(publisher);
                    if (errorMessage != null)
                    {
                        MessageBox.Show(errorMessage);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Publisher saved!");

                        _publisherList = _publisherRepository.GetAll().OrderBy(x => x.Name).ToList();
                        _publisherBindingSource.DataSource = _publisherList;
                        _publisherBindingSource.CurrencyManager.Refresh();
                    }
                }
                else if (this._view.AppState.Equals("Edit"))
                {
                    string errorMessage = _publisherRepository.Edit(publisher);
                    if (errorMessage != null)
                    {
                        MessageBox.Show(errorMessage);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Publisher saved!");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            this._view.SetViewState("View");
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            int bookMark = _publisherBindingSource.CurrencyManager.Position;
            if (this._view.AppState.Equals("Add"))
            {
                this._publisherList.RemoveAt(bookMark);
                this._publisherBindingSource.CurrencyManager.Position = this._bookMark;
                this._view.SetViewState("View");
            }
            else if (this._view.AppState.Equals("Edit"))
            {
                PublisherModel publisher = (PublisherModel)_publisherBindingSource.Current;
                
                this._publisherList[bookMark] = _publisherRepository.GetByID(publisher.PubID);
                this._publisherBindingSource.CurrencyManager.Refresh();
                this._view.SetViewState("View");
            }
        }

        private void EditPublisher(object? sender, EventArgs e)
        {
            this._view.SetViewState("Edit");            
        }

        private void AddNew(object? sender, EventArgs e)
        {
            this._view.SetViewState("Add");
            this._bookMark = _publisherBindingSource.CurrencyManager.Position;
            this._publisherList.Add(new PublisherModel());
            int bookMark = this._publisherList.Count - 1;
            this._publisherBindingSource.CurrencyManager.Position = bookMark;
        }

        private void SearchPublisherName(object? sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this._view.SearchValue);
            if (emptyValue)
            {
                this._publisherList = _publisherRepository.GetAll().ToList();
            }
            else
            {
                this._publisherList = _publisherRepository.GetByValue(_view.SearchValue).ToList();
                if (_publisherList.Count == 0)
                {
                    MessageBox.Show("No publishers found that match your search criteria!",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    _publisherList = _publisherRepository.GetAll().OrderBy(x => x.Name).ToList();
                    _publisherBindingSource.DataSource = _publisherList;
                }
            }
            this._publisherBindingSource.DataSource = _publisherList;
        }

        private void FirstRecord(object? sender, EventArgs e)
        {
            _publisherBindingSource.CurrencyManager.Position = 0;
        }
        private void PreviousRecord(object? sender, EventArgs e)
        {
            _publisherBindingSource.CurrencyManager.Position--;
        }
        private void NextRecord(object? sender, EventArgs e)
        {
            _publisherBindingSource.CurrencyManager.Position++;
        }
        private void LastRecord(object? sender, EventArgs e)
        {
            int bookMark = _publisherList.Count - 1;
            _publisherBindingSource.CurrencyManager.Position = bookMark;
        }


    }
}
