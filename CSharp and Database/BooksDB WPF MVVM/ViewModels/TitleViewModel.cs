using BooksDB_WPF_MVVM.Commands;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace BooksDB_WPF_MVVM.ViewModels
{
    class TitleViewModel : BaseViewModel
    {
        private TitleRepository _repository;
        private IEnumerable<TitleModel> _titles;

        public string FormHeader { get; set; } = "TITLES LIST";
        public IEnumerable<TitleModel> Titles 
        { 
            get => _titles;
            set
            {
                _titles = value;
                OnPropertyChanged();
            }
        }

        private int _titleIndex;

        public int TitleIndex
        {
            get { return _titleIndex; }
            set 
            { 
                _titleIndex = value;
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

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        

        public TitleViewModel(INavigationService navigation, ISharedDataService sharedDataService)
        {
            _repository = new TitleRepository(GetConnectionString());
            Titles = _repository.GetAll();
            SearchCommand = new RelayCommand(SearchByValue, CanSearchByValue);
            ClearCommand = new RelayCommand(ClearSearchValue, CanClearSearchValue);
            EditCommand = new RelayCommand(EditTitle, CanEditTitle);
            AddCommand = new RelayCommand(AddNewTitle, CanAddNewTitle);
            DeleteCommand = new RelayCommand(DeleteTitle, CanDeleteTitle);

            Navigation = navigation;
            SharedData = sharedDataService;
        }

        private bool CanDeleteTitle(object obj)
        {
            return true;
        }

        private void DeleteTitle(object obj)
        {
            if (obj != null)
            {
                //title selected for deletion
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Title?", "Delete Title", 
                                                          MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    string isbn = ((string)obj);
                    TitleModel title = Titles.First(x => x.ISBN == isbn);

                    string message = _repository.Delete(isbn);
                    new TitleAuthorRepository(GetConnectionString()).DeleteByISBN(title);
                    if (message != null)
                    {
                        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        Navigation.NavigateTo<TitleViewModel>();
                    }
                }
            }
            else
            {
                MessageBox.Show("No title selected for deletion!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private bool CanAddNewTitle(object obj)
        {
            return true;
        }

        private void AddNewTitle(object obj)
        {
            Navigation.NavigateTo<AddTitleViewModel>();
        }

        private bool CanEditTitle(object obj)
        {
            return true;
        }

        private void EditTitle(object obj)
        {
            if (obj == null)
            {
                MessageBox.Show("Please select a title to edit!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SharedData.Parameter = obj;
                Navigation.NavigateTo<EditTitleViewModel>();
            }
        }

        private bool CanClearSearchValue(object obj)
        {
            return true;
        }

        private void ClearSearchValue(object obj)
        {
            Titles = _repository.GetAll();
        }

        private bool CanSearchByValue(object obj)
        {
            return true;
        }

        private void SearchByValue(object obj)
        {
            string searchValue = (string)obj;
            if (searchValue == null || searchValue == "")
            {
                return;
            }
            else
            {
                Titles = _repository.GetByValue(searchValue);
            }
        }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SQLBOOKSDB"].ConnectionString;
        }

    }
}
