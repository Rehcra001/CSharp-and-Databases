using BooksDB_WPF_MVVM.Commands;
using BooksDB_WPF_MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BooksDB_WPF_MVVM.ViewModels
{
    class MainViewModel : BaseViewModel
    {
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


		public ICommand	 NavigateToTitleViewCommand { get; set; }
        public MainViewModel(INavigationService navigation, ISharedDataService sharedDataService)
        {
			Navigation = navigation;
			NavigateToTitleViewCommand = new RelayCommand(NavigateToTitleView, CanNavigateToTitleView);
			SharedData = sharedDataService;
        }

        private bool CanNavigateToTitleView(object obj)
        {
            return true;
        }

        private void NavigateToTitleView(object obj)
        {
            Navigation.NavigateTo<TitleViewModel>();
        }
    }
}
