using BooksDB_WinForms_MVP.Presenters.Common;
using BooksDB_WinForms_MVP.Views;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
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
        private IAuthorRepository _authorRepository;
        private ITitleAuthorRepository _titleAuthorRepository;
        
        private BindingSource _titleBindingSource;
        private BindingSource _publisherBindingSource;
        private BindingSource _authorBindingSource;
        private BindingSource _selectedAuthorBindingSource;
        private IEnumerable<TitleModel> _titleList;
        private IEnumerable<PublisherModel> _publisherList;
        private List<AuthorModel> _authorList;
        private List<AuthorModel> _selectedAuthorsList;
        private List<AuthorModel> _OriginalTitleAuthorList;


        public TitlePresenter(ITitleView view, ITitleRepository titleRepository, 
                              IPublisherRepository publisherRepository, 
                              IAuthorRepository authorRepository,
                              ITitleAuthorRepository titleAuthorRepository)
        {
            this._titleBindingSource = new BindingSource();
            this._publisherBindingSource = new BindingSource();
            this._authorBindingSource = new BindingSource();
            this._selectedAuthorBindingSource = new BindingSource();
            _view = view;
            _titleRepository = titleRepository;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
            _titleAuthorRepository = titleAuthorRepository;
            _selectedAuthorsList = new List<AuthorModel>();

            //Subscribe event handler methods to view events
            this._view.SearchEvent += SearchTitle;
            this._view.AddNewEvent += AddTitle;
            this._view.EditEvent += LoadSelectedTitleToEdit;
            this._view.SaveEvent += SaveTitle;
            this._view.CancelEvent += CancelAction;
            this._view.DeleteEvent += DeleteSelectedTitle;
            this._view.AddAuthorEvent += AddAuthorToList;
            this._view.RemoveAuthorEvent += RemoveSelectedAuthorFromList;


            //Set title binding source
            this._view.SetTitleBindingSource(_titleBindingSource, _publisherBindingSource, 
                                             _authorBindingSource, _selectedAuthorBindingSource);
            //Lasd title list
            LoadAllTitles();
            this._view.Show();
        }

        private void RemoveSelectedAuthorFromList(object? sender, EventArgs e)
        {
            _authorList.Add((AuthorModel)_selectedAuthorBindingSource.Current);
            _selectedAuthorsList.Remove((AuthorModel)_selectedAuthorBindingSource.Current);

            if (_selectedAuthorsList.Count > 0)
            {
                _selectedAuthorBindingSource.DataSource = _selectedAuthorsList.OrderBy(x => x.Author);
                
            }
            else
            {
                _selectedAuthorBindingSource.DataSource = _selectedAuthorsList;
            }
            
            _authorBindingSource.DataSource = _authorList.OrderBy(x => x.Author);

            _selectedAuthorBindingSource.ResetBindings(false);
            _authorBindingSource.ResetBindings(false);
        }

        private void AddAuthorToList(object? sender, EventArgs e)
        {
            _selectedAuthorsList.Add((AuthorModel)_authorBindingSource.Current);
            _authorList.Remove((AuthorModel)_authorBindingSource.Current);

            _selectedAuthorBindingSource.DataSource = _selectedAuthorsList.OrderBy(x => x.Author);
            if (_authorList.Count > 0)
            {
                _authorBindingSource.DataSource = _authorList.OrderBy(x => x.Author);

            }
            else
            {
                _authorBindingSource.DataSource = _authorList;
            }
            _authorBindingSource.DataSource = _authorList.OrderBy(x => x.Author);

            _selectedAuthorBindingSource.ResetBindings(false);
            _authorBindingSource.ResetBindings(false);
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
            _authorList = _authorRepository.GetAll().OrderBy(x => x.Author).ToList();
            _authorBindingSource.DataSource = _authorList;
            _selectedAuthorsList = new List<AuthorModel>();
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

            //record the original list of authors for comparison when saving
            //if _selectedAuthorsList is in _OriginalTitleAuthorList - do nothing
            //if _OriginalTitleAuthorList author not in _selectedAuthorsList - Delete
            //if _selectedAuthorsList not in _OriginalTitleAuthorList - ADD
            _OriginalTitleAuthorList = _authorRepository.GetAuthorsByISBN(title.ISBN!).OrderBy(x => x.Author).ToList();

            //Load title authors
            _selectedAuthorsList = _authorRepository.GetAuthorsByISBN(title.ISBN!).OrderBy(x => x.Author).ToList();
            _selectedAuthorBindingSource.DataSource = _selectedAuthorsList;
            //Load all authors excluding titles author
            _authorList = _authorRepository.GetAll().OrderBy(x => x.Au_ID).ToList();
            foreach (AuthorModel author in _selectedAuthorsList)
            {
                int id = author.Au_ID;
                int index = _authorList.FindIndex(x => x.Au_ID == id);
                _authorList.RemoveAt(index);
            }
            _authorBindingSource.DataSource = _authorList.OrderBy(x => x.Author);
        }

        void DeleteTitleAuthor()
        {
            List<TitleAuthorModel> model = new List<TitleAuthorModel>();
            TitleModel title = (TitleModel)_titleBindingSource.Current;
            //check if _OriginalTitleAuthorList author not in _selectedAuthorsList
            foreach (AuthorModel author in _OriginalTitleAuthorList)
            {
                int id = author.Au_ID;
                if (_selectedAuthorsList.FindIndex(x => x.Au_ID == id) == -1)
                {
                    model.Add(new TitleAuthorModel() { Au_ID = author.Au_ID, ISBN = title.ISBN! });
                }
            }
            //delete
            if (model.Count > 0)
            {
                _titleAuthorRepository.Delete(model);
            }
        }

        void AddTitleAuthor(string isbn)
        {
            List<TitleAuthorModel> model = new List<TitleAuthorModel>();
            TitleModel title = (TitleModel)_titleBindingSource.Current;
            //check if _selectedAuthorsList not in _OriginalTitleAuthorList
            foreach (AuthorModel author in _selectedAuthorsList)
            {
                int id = author.Au_ID;
                if (_OriginalTitleAuthorList == null || 
                    _OriginalTitleAuthorList.FindIndex(x => x.Au_ID == id) == -1)
                {
                    model.Add(new TitleAuthorModel() { Au_ID = author.Au_ID, ISBN = isbn });
                }
            }
            if (model.Count > 0)
            {
                _titleAuthorRepository.Add(model);
            }
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
                    DeleteTitleAuthor();
                    AddTitleAuthor(model.ISBN);
                    _view.Message = "Edit was successful.";
                }
                else
                {
                    _titleRepository.Add(model);
                    AddTitleAuthor(model.ISBN);
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
            _selectedAuthorsList = new List<AuthorModel>();
            _selectedAuthorBindingSource.DataSource = _selectedAuthorsList;
            _authorList = new List<AuthorModel>();
            _authorBindingSource.DataSource = _authorList;
        }

        private void DeleteSelectedTitle(object? sender, EventArgs e)
        {
            try
            {
                TitleModel title = (TitleModel)_titleBindingSource.Current;
                _titleRepository.Delete(title.ISBN);
                _titleAuthorRepository.DeleteByISBN(title);
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
