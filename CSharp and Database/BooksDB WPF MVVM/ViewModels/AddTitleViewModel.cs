using BooksDB_WPF_MVVM.Commands;
using BooksDB_WPF_MVVM.Common;
using BooksDB_WPF_MVVM.Services;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BooksDB_WPF_MVVM.ViewModels
{
    public class AddTitleViewModel : BaseViewModel
    {
        public string FormHeader { get; private set; } = "Edit Title";

        private TitleRepository _titleRepository;
        private PublisherRepository _publisherRepository;

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

        public RelayCommand SaveTitleCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public AddTitleViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            _titleRepository = new TitleRepository(GetConnectionString());
            _publisherRepository = new PublisherRepository(GetConnectionString());

            Title = new TitleModel();

            Publishers = _publisherRepository.GetAll();

            SaveTitleCommand = new RelayCommand(SaveTitle, CanSaveTitle);
            CancelCommand = new RelayCommand(CancelAdd, CanCancelAdd);
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
