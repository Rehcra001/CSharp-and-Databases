using BooksDB_WPF_MVVM.Commands;
using BooksDB_WPF_MVVM.Services;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BooksDB_WPF_MVVM.Common;
using DataAccess.Interfaces;

namespace BooksDB_WPF_MVVM.ViewModels
{
    public class EditTitleViewModel : BaseViewModel
    {        
        public string FormHeader { get; private set; } = "Edit Title";

        private TitleRepository _titleRepository;
        private PublisherRepository _publisherRepository;
        private AuthorRepository _authorRepository;
        private List<AuthorModel> _originalSelectedAuthorsList;

        private TitleModel _title;

        public TitleModel Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                OnPropertyChanged();
            }
        }

        private PublisherModel _publisher;

        public PublisherModel Publisher
        {
            get { return _publisher; }
            set 
            { 
                _publisher = value;              
                Title.PubID = _publisher.PubID;
                OnPropertyChanged();
            }
        }


        private IEnumerable<PublisherModel> _publishers;

        public IEnumerable<PublisherModel> Publishers
        {
            get { return _publishers; }
            set 
            { 
                _publishers = value;
                OnPropertyChanged();
            }
        }

        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get { return _navigation; }
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }


        private ISharedDataService _sharedData;

        public ISharedDataService SharedData
        {
            get { return _sharedData; }
            set
            {
                _sharedData = value;
                OnPropertyChanged();
            }
        }

        private List<AuthorModel> _authors;

        public List<AuthorModel> Authors
        {
            get { return _authors; }
            set
            {
                _authors = value;
                OnPropertyChanged();
            }
        }



        private List<AuthorModel> _selectedAuthors;

        public List<AuthorModel> SelectedAuthors
        {
            get { return _selectedAuthors; }
            set
            {
                _selectedAuthors = value;
                OnPropertyChanged();
            }
        }

        private int _authorsIndex;

        public int AuthorsIndex
        {
            get { return _authorsIndex; }
            set
            {
                _authorsIndex = value;
                OnPropertyChanged();
            }
        }

        private int _selectedAuthorsIndex;

        public int SelectedAuthorsIndex
        {
            get { return _selectedAuthorsIndex; }
            set
            {
                _selectedAuthorsIndex = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveTitleCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddAuthorCommand { get; set; }
        public RelayCommand RemoveAuthorCommand { get; set; }

        public EditTitleViewModel(INavigationService navigation, ISharedDataService sharedData)
        {
            Navigation = navigation;
            SharedData = sharedData;

            _titleRepository = new TitleRepository(GetConnectionString());
            _publisherRepository = new PublisherRepository(GetConnectionString());
            _authorRepository = new AuthorRepository(GetConnectionString());

            Title = _titleRepository.GetTitleByISBN((string)SharedData.Parameter);
            
            Publishers = _publisherRepository.GetAll();

            Publisher = Publishers.First(x => x.PubID == Title.PubID);

            SelectedAuthors = _authorRepository.GetAuthorsByISBN(Title.ISBN!).OrderBy(x => x.Author).ToList();
            _originalSelectedAuthorsList = _authorRepository.GetAuthorsByISBN(Title.ISBN!).OrderBy(x => x.Author).ToList();

            Authors = _authorRepository.GetAll().OrderBy(x => x.Author).ToList();
            foreach (AuthorModel author in SelectedAuthors)
            {
                int id = author.Au_ID;
                int index = Authors.FindIndex(x => x.Au_ID == id);
                Authors.RemoveAt(index);
            }
            AuthorsIndex = 0;

            

            SaveTitleCommand = new RelayCommand(SaveTitle, CanSaveTitle);
            CancelCommand = new RelayCommand(CancelEdit, CanCancelEdit);
            AddAuthorCommand = new RelayCommand(AddAuthor, CanAddAuthor);
            RemoveAuthorCommand = new RelayCommand(RemoveAuthor, CanRemoveAuthor);
        }

        private void DeleteTitleAuthor()
        {
            List<TitleAuthorModel> model = new List<TitleAuthorModel>();
            //check if _OriginalTitleAuthorList author not in _selectedAuthorsList
            foreach (AuthorModel author in _originalSelectedAuthorsList)
            {
                int id = author.Au_ID;
                if (SelectedAuthors.FindIndex(x => x.Au_ID == id) == -1)
                {
                    model.Add(new TitleAuthorModel() { Au_ID = author.Au_ID, ISBN = Title.ISBN! });
                }
            }
            //delete
            if (model.Count > 0)
            {
                new TitleAuthorRepository(GetConnectionString()).Delete(model);
            }
        }

        private void AddTitleAuthor()
        {
            List<TitleAuthorModel> model = new List<TitleAuthorModel>();
            //check if _selectedAuthorsList not in _OriginalTitleAuthorList
            foreach (AuthorModel author in SelectedAuthors)
            {
                int id = author.Au_ID;
                if (_originalSelectedAuthorsList == null ||
                    _originalSelectedAuthorsList.FindIndex(x => x.Au_ID == id) == -1)
                {
                    model.Add(new TitleAuthorModel() { Au_ID = author.Au_ID, ISBN = Title.ISBN! });
                }
            }
            if (model.Count > 0)
            {
                new TitleAuthorRepository(GetConnectionString()).Add(model);
            }
        }

        private bool CanAddAuthor(object obj)
        {
            return true;
        }

        private void AddAuthor(object obj)
        {
            if (AuthorsIndex != -1)
            {
                SelectedAuthors.Add(Authors[AuthorsIndex]);
                SelectedAuthors = SelectedAuthors.OrderBy(x => x.Author).ToList();

                Authors.RemoveAt(AuthorsIndex);
                Authors = Authors.OrderBy(x => x.Author).ToList();
            }

        }

        private bool CanRemoveAuthor(object obj)
        {
            return SelectedAuthors.Count > 0;
        }

        private void RemoveAuthor(object obj)
        {
            if (SelectedAuthorsIndex != -1)
            {
                Authors.Add(SelectedAuthors[SelectedAuthorsIndex]);
                Authors = Authors.OrderBy(x => x.Author).ToList();
                //AuthorsIndex = 0;

                SelectedAuthors.RemoveAt(SelectedAuthorsIndex);
                SelectedAuthors = SelectedAuthors.OrderBy(x => x.Author).ToList();
                //SelectedAuthorsIndex = 0;
            }

        }
        private bool CanCancelEdit(object obj)
        {
            return true;
        }

        private void CancelEdit(object obj)
        {
            Navigation.NavigateTo<TitleViewModel>();
        }

        private bool CanSaveTitle(object obj)
        {
            return true;
        }

        private void SaveTitle(object obj)
        {
            try
            {
                new ModelValidation().Validate(Title);
                string message = _titleRepository.Edit(Title);
                DeleteTitleAuthor();
                AddTitleAuthor();
                if (message == null)
                {
                    Navigation.NavigateTo<TitleViewModel>();
                }
                else
                {
                    MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Edit Title", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            

        }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SQLBOOKSDB"].ConnectionString;
        }
    }
}
