using BooksDB_WinForms_MVP.Views;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace BooksDB_WinForms_MVP.Presenters
{
    public class TitlePresenter
    {
        private ITitleView _view;
        private ITitleRepository _repository;
        private BindingSource _titleBindingSource;
        private IEnumerable<TitleModel> _titleList;

        public TitlePresenter(ITitleView view, ITitleRepository repository)
        {
            this._titleBindingSource = new BindingSource();
            _view = view;
            _repository = repository;

            //Subscribe event handler methods to view events
            this._view.SearchEvent += SearchTitle;
            this._view.AddNewEvent += AddTitle;
            this._view.EditEvent += LoadSelectedTitleToEdit;
            this._view.SaveEvent += SaveTitle;
            this._view.CancelEvent += CancelAction;
            this._view.DeleteEvent += DeleteSelectedTitle;


            //Set title binding source
            this._view.SetTitleBindingSource(_titleBindingSource);
            //Lasd title list
            LoadAllTitles();
        }
        private void LoadAllTitles()
        {
            _titleList = _repository.GetAll();
            _titleBindingSource.DataSource = _titleList;
        }

        private void SearchTitle(object? sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this._view.SearchValue);
            if (emptyValue == false)
            {
                _titleList = _repository.GetByValue(this._view.SearchValue);
            }
            else
            {
                _titleList = _repository.GetAll();
            }
            _titleBindingSource.DataSource = _titleList;
        }

        private void AddTitle(object? sender, EventArgs e)
        {
            _view.IsEdit = false;
        }

        private void LoadSelectedTitleToEdit(object? sender, EventArgs e)
        {
            TitleModel title = (TitleModel)_titleBindingSource.Current;
            _view.Title = title.Title;
            _view.Year_Published = title.Year_Published.ToString();
            _view.ISBN = title.ISBN;
            _view.PubID = title.PubID.ToString();
            _view.Description = title.Description;
            _view.Notes = title.Notes;
            _view.Subject = title.Subject;
            _view.Comment = title.Comments;
            _view.IsEdit = true;
        }

        private void SaveTitle(object? sender, EventArgs e)
        {
            TitleModel model = new TitleModel();
            model.Title = _view.Title;
            model.Year_Published = Convert.ToInt32(_view.Year_Published);
            model.ISBN = _view.ISBN;
            model.PubID = Convert.ToInt32(_view.PubID);
            model.Description = _view.Description;
            model.Notes = _view.Notes;
            model.Subject = _view.Subject;
            model.Comments = _view.Comment;
            //catch errors
            try
            {
                if (_view.IsEdit)
                {
                    _repository.Edit(model);
                    _view.Message = "Edit was successful.";
                }
                else
                {
                    _repository.Add(model);
                    _view.Message = "Add was successful";
                }
                _view.IsSuccessful = true;
                LoadAllTitles();
                CleanFields();
            }
            catch (Exception ex)
            {
                _view.IsSuccessful = false;
                _view.Message = ex.Message;
            }

        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanFields();
        }

        private void CleanFields()
        {
            _view.Title = "";
            _view.Year_Published = "";
            _view.ISBN = "";
            _view.PubID = "";
            _view.Description = "";
            _view.Notes = "";
            _view.Subject = "";
            _view.Comment = "";
        }

        private void DeleteSelectedTitle(object? sender, EventArgs e)
        {
            try
            {
                TitleModel title = (TitleModel)_titleBindingSource.Current;
                _repository.Delete(title.ISBN);
                _view.IsSuccessful = true;
                _view.Message = "Delete was successful";
                LoadAllTitles();
            }
            catch (Exception ex)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error occured, delete action failed";
            }
        }

        
    }
}
