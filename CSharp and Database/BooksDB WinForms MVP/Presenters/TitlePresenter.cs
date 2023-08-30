using BooksDB_WinForms_MVP.Presenters.Common;
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
        private ITitleRepository _titleRepository;
        private IPublisherRepository _publisherRepository;
        private BindingSource _titleBindingSource;
        private BindingSource _publisherBindingSource;
        private IEnumerable<TitleModel> _titleList;
        private IEnumerable<PublisherModel> _publisherList;

        public TitlePresenter(ITitleView view, ITitleRepository titleRepository, IPublisherRepository publisherRepository)
        {
            this._titleBindingSource = new BindingSource();
            this._publisherBindingSource = new BindingSource();
            _view = view;
            _titleRepository = titleRepository;
            _publisherRepository = publisherRepository;

            //Subscribe event handler methods to view events
            this._view.SearchEvent += SearchTitle;
            this._view.AddNewEvent += AddTitle;
            this._view.EditEvent += LoadSelectedTitleToEdit;
            this._view.SaveEvent += SaveTitle;
            this._view.CancelEvent += CancelAction;
            this._view.DeleteEvent += DeleteSelectedTitle;


            //Set title binding source
            this._view.SetTitleBindingSource(_titleBindingSource, _publisherBindingSource);
            //Lasd title list
            LoadAllTitles();
            this._view.Show();
        }
        private void LoadAllTitles()
        {
            _titleList = _titleRepository.GetAll();
            _titleBindingSource.DataSource = _titleList;

            _publisherList = _publisherRepository.GetAll().OrderBy(x => x.Name);
            _publisherBindingSource.DataSource = _publisherList;
        }

        private void SearchTitle(object? sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this._view.SearchValue);
            if (emptyValue == false)
            {
                _titleList = _titleRepository.GetByValue(this._view.SearchValue);
            }
            else
            {
                _titleList = _titleRepository.GetAll();
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
            _view.Comments = title.Comments;
            _view.IsEdit = true;
        }

        private void SaveTitle(object? sender, EventArgs e)
        {
            TitleModel model = new TitleModel();
            model.Title = _view.Title;
            model.Year_Published = int.TryParse(_view.Year_Published, out _) ? Convert.ToInt32(_view.Year_Published) : 0;
            model.ISBN = _view.ISBN;
            model.PubID = int.TryParse(_view.PubID, out _) ? Convert.ToInt32(_view.PubID) : 0;
            model.Description = _view.Description;
            model.Notes = _view.Notes;
            model.Subject = _view.Subject;
            model.Comments = _view.Comments;
            //catch errors
            try
            {
                new ModelValidation().Validate(model);
                if (_view.IsEdit)
                {
                    _titleRepository.Edit(model);
                    _view.Message = "Edit was successful.";
                }
                else
                {
                    _titleRepository.Add(model);
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
            LoadAllTitles();
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
            _view.Comments = "";
        }

        private void DeleteSelectedTitle(object? sender, EventArgs e)
        {
            try
            {
                TitleModel title = (TitleModel)_titleBindingSource.Current;
                _titleRepository.Delete(title.ISBN);
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
