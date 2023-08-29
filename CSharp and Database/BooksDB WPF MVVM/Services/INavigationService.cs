using BooksDB_WPF_MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB_WPF_MVVM.Services
{
    public interface INavigationService
    {
        BaseViewModel CurrentView { get; }
        void NavigateTo<T>() where T : BaseViewModel;
    }
}
