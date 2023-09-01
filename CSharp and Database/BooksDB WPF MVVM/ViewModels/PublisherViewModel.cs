using BooksDB_WPF_MVVM.Commands;
using BooksDB_WPF_MVVM.Common;
using BooksDB_WPF_MVVM.Services;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BooksDB_WPF_MVVM.ViewModels
{
    internal class PublisherViewModel : BaseViewModel
    {
		private PublisherRepository _publisherRepository;
        private string _viewState;
        private PublisherModel _undoEdit;

		private PublisherModel	_publisher;

		public PublisherModel Publisher
		{
			get { return _publisher; }
			set 
			{ 
				_publisher = value;
				OnPropertyChanged();
			}
		}

		private int _gridIndex;

		public int GridIndex
		{
			get { return _gridIndex; }
			set 
			{ 
				_gridIndex = value;
				OnPropertyChanged();
			}
		}

        private bool _gridEnabled;

        public bool GridEnabled
        {
            get { return _gridEnabled; }
            set 
            { 
                _gridEnabled = value;
                OnPropertyChanged();
            }
        }



        private List<PublisherModel> _publishers;

		public List<PublisherModel> Publishers
		{
			get { return _publishers; }
			set 
			{ 
				_publishers = value;
				OnPropertyChanged();
			}
		}

        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set 
            { 
                _isReadOnly = value;
                OnPropertyChanged();
            }
        }

        private string _searchValue;

        public string SearchValue
        {
            get { return _searchValue; }
            set 
            { 
                _searchValue = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand FirstRecordCommand { get; set; }
        public RelayCommand PreviousRecordCommand { get; set; }
        public RelayCommand NextRecordCommand { get; set; }
        public RelayCommand LastRecordCommand { get; set; }
        public RelayCommand AddNewPublisherCommand { get; set; }
        public RelayCommand EditPublisherCommand { get; set; }
        public RelayCommand DeletePublisherCommand { get; set; }
        public RelayCommand SavePublisherCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }



        public PublisherViewModel()
        {
            _publisherRepository = new PublisherRepository(GetConnectionString());

			Publishers = _publisherRepository.GetAll().ToList();
			//Move to the first record
			GridIndex = 0;

			//Commands
			FirstRecordCommand = new RelayCommand(MoveToFirstRecord, CanMoveToFirstRecord);
			PreviousRecordCommand = new RelayCommand(MoveToPreviousRecord, CanMoveToPreviousRecord);
			NextRecordCommand = new RelayCommand(MoveToNextRecord, CanMoveToNextRecord);
			LastRecordCommand = new RelayCommand(MoveToLastRecord, CanMoveToLastRecord);
            AddNewPublisherCommand = new RelayCommand(AddNewPublisher, CanAddNewPublisher);
            CancelCommand = new RelayCommand(CancelAction, CanCancelAction);
            EditPublisherCommand = new RelayCommand(EditPublisher, CanEditPublisher);
            SavePublisherCommand = new RelayCommand(SavePublisher, CanSavePulisher);
            DeletePublisherCommand = new RelayCommand(DeletePublisher, CanDeletePublisher);
            SearchCommand = new RelayCommand(SearchName, CanSearchName);

            SetViewState("View");
		}

        private bool CanSearchName(object obj)
        {
            return _viewState.Equals("View");
        }

        private void SearchName(object obj)
        {
            if (SearchValue == null || SearchValue == "")
            {

                Publishers = _publisherRepository.GetAll().ToList();
                GridIndex = 0;
            }
            else
            {
                //Publishers = _publisherRepository.GetByValue(SearchValue).ToList();
                Publishers = Publishers.FindAll(x => x.Name.ToLower().StartsWith(SearchValue.ToLower()));
                GridIndex = 0;

            }
        }

        private bool CanDeletePublisher(object obj)
        {
            return _viewState.Equals("View");
        }

        private void DeletePublisher(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this publisher?",
                                                      "Delete Publisher", MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning,
                                                      MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                _publisherRepository.Delete(Publishers[GridIndex].PubID);
                Publishers.RemoveAt(GridIndex);
                GridIndex = 0;
            }
        }

        private bool CanSavePulisher(object obj)
        {
            return !_viewState.Equals("View");
        }

        private void SavePublisher(object obj)
        {
            try
            {
                new ModelValidation().Validate(Publisher);
                if (_viewState.Equals("Add"))
                {
                    string errorMessage = _publisherRepository.Add(Publisher);
                    if (errorMessage != null)
                    {
                        MessageBox.Show(errorMessage);
                        return;
                    }
                }
                else if (_viewState.Equals("Edit"))
                {
                    string errorMessage = _publisherRepository.Edit(Publisher);
                    if (errorMessage != null)
                    {
                        MessageBox.Show(errorMessage);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SetViewState("View");
        }

        private bool CanEditPublisher(object obj)
        {
            return _viewState.Equals("View");
        }

        private void EditPublisher(object obj)
        {
            //Save the current record in case the edit is cancelled
            _undoEdit = _publisherRepository.GetByID(Publisher.PubID);
            SetViewState("Edit");
        }

        private bool CanCancelAction(object obj)
        {
            return !_viewState.Equals("View");
        }

        private void CancelAction(object obj)
        {
            if (_viewState.Equals("Add"))
            {
                int index = Publishers.Count - 1;
                Publishers.RemoveAt(index);
                GridIndex = 0;                
            }
            else if (_viewState.Equals("Edit"))
            {
                Publishers[GridIndex] = _undoEdit;

                //Force OnPropertyChanged
                if (GridIndex == 0)
                {
                    int index = GridIndex;
                    GridIndex++;
                    GridIndex = index;
                }
                else
                {
                    GridIndex--;
                    GridIndex++;
                }
            }
            SetViewState("View");
        }

        private bool CanAddNewPublisher(object obj)
        {
            return _viewState.Equals("View");
        }

        private void AddNewPublisher(object obj)
        {
            Publishers.Add(new PublisherModel());
            GridIndex = Publishers.Count - 1;
            SetViewState("Add");
        }

        private bool CanMoveToFirstRecord(object obj)
        {
            return _viewState.Equals("View");
        }

        private void MoveToFirstRecord(object obj)
        {
            GridIndex = 0;
        }

        private bool CanMoveToPreviousRecord(object obj)
        {
            return _viewState.Equals("View");
        }

        private void MoveToPreviousRecord(object obj)
        {
            if (GridIndex > 0)
            {
                GridIndex--;
            }
        }

        private bool CanMoveToNextRecord(object obj)
        {
            return _viewState.Equals("View");
        }

        private void MoveToNextRecord(object obj)
        {
            int index = Publishers.Count - 1;
            if (GridIndex < index)
            {
                GridIndex++;
            }
        }

        private bool CanMoveToLastRecord(object obj)
        {
            return _viewState.Equals("View");
        }

        private void MoveToLastRecord(object obj)
        {
			GridIndex = Publishers.Count - 1;
        }

        private void SetViewState(string viewState)
        {
            _viewState = viewState;

            switch (_viewState)
            {
                case "View":
                    IsReadOnly = true;
                    GridEnabled = true;
                    break;
                default:
                    IsReadOnly = false;
                    GridEnabled = false;
                    break;
            }
        }

        private string GetConnectionString()
        {
			return ConfigurationManager.ConnectionStrings["SQLBOOKSDB"].ConnectionString;
        }
    }
}
