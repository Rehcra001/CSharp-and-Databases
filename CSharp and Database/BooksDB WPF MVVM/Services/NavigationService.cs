using BooksDB_WPF_MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB_WPF_MVVM.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private BaseViewModel _currentView;
        private readonly Func<Type, BaseViewModel> _viewModelFactory;

        public BaseViewModel CurrentView
        {
            get
            {
                return _currentView;
            }
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            BaseViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }

    }
}
