using BooksDB_WPF_MVVM.Services;
using BooksDB_WPF_MVVM.ViewModels;
using BooksDB_WPF_MVVM.Views;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BooksDB_WPF_MVVM
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            //Views and viewmodels
            services.AddSingleton<MainView>(provider => new MainView
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddTransient<TitleViewModel>();
            services.AddTransient<EditTitleViewModel>();
            services.AddTransient<AddTitleViewModel>();
            services.AddTransient<PublisherViewModel>();

            //Services
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ISharedDataService, SharedDataService>();

            services.AddSingleton<Func<Type, BaseViewModel>>(ServiceProvider => viewModelType => (BaseViewModel)ServiceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainView = _serviceProvider.GetRequiredService<MainView>();
            mainView.Show();
            base.OnStartup(e);
        }

        
    }
}
