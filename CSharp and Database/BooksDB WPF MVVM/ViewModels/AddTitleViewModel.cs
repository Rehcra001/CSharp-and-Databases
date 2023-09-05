using BooksDB_WPF_MVVM.Commands;
using BooksDB_WPF_MVVM.Common;
using BooksDB_WPF_MVVM.Services;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BooksDB_WPF_MVVM.ViewModels
{
    public class AddTitleViewModel : BaseViewModel
    {
        public string FormHeader { get; private set; } = "Add New Title";

        private TitleRepository _titleRepository;
        private PublisherRepository _publisherRepository;
        private TitleAuthorRepository _titleAuthorRepository;
        private AuthorRepository _authorRepository;

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

        //private AuthorModel _author;

        //public AuthorModel Author
        //{
        //    get { return _author; }
        //    set
        //    {
        //        _author = value;
        //        OnPropertyChanged();
        //    }
        //}

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

        public AddTitleViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            _titleRepository = new TitleRepository(GetConnectionString());
            _publisherRepository = new PublisherRepository(GetConnectionString());
            _authorRepository = new AuthorRepository(GetConnectionString());
            _titleAuthorRepository = new TitleAuthorRepository(GetConnectionString());

            Title = new TitleModel();

            Publishers = _publisherRepository.GetAll();

            Authors = _authorRepository.GetAll().OrderBy(x => x.Author).ToList();
            AuthorsIndex = 0;

            SelectedAuthors = new List<AuthorModel>();

            SaveTitleCommand = new RelayCommand(SaveTitle, CanSaveTitle);
            CancelCommand = new RelayCommand(CancelAdd, CanCancelAdd);
            AddAuthorCommand = new RelayCommand(AddAuthor, CanAddAuthor);
            RemoveAuthorCommand = new RelayCommand(RemoveAuthor, CanRemoveAuthor);
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
                //SelectedAuthorsIndex = 0;

                Authors.RemoveAt(AuthorsIndex);
                Authors = Authors.OrderBy(x => x.Author).ToList();
                //AuthorsIndex = 0;
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

        private bool CanCancelAdd(object obj)
        {
            return true;
        }

        private void CancelAdd(object obj)
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
                string message = _titleRepository.Add(Title);
                if (SelectedAuthors.Count > 0)
                {
                    List<TitleAuthorModel> titleAuthors = new List<TitleAuthorModel>();
                    foreach (AuthorModel author in SelectedAuthors)
                    {
                        TitleAuthorModel titleAuthor = new TitleAuthorModel();
                        titleAuthor.Au_ID = author.Au_ID;
                        titleAuthor.ISBN = Title.ISBN!;
                        titleAuthors.Add(titleAuthor);
                        
                    }
                    _titleAuthorRepository.Add(titleAuthors);
                }
                
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
