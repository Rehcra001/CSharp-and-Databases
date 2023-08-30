using BooksDB_WinForms_MVP.Views;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB_WinForms_MVP.Presenters
{
    public class MainPresenter
    {
        private IMainView _mainView;
        private readonly string _sqlConnection;

        public MainPresenter(IMainView mainView, string sqlConnection)
        {
            _mainView = mainView;
            _sqlConnection = sqlConnection;
            this._mainView.ShowTitleView += ShowTitlesView;
            this._mainView.ShowPublisherView += ShowPublishersView;
        }

        private void ShowPublishersView(object? sender, EventArgs e)
        {
            if (PublisherView._instance == null || PublisherView._instance.IsDisposed)
            {
                IPublisherView view = PublisherView.GetInstance((Form)_mainView);
                IPublisherRepository publisherRepository = new PublisherRepository(_sqlConnection);
                new PublisherPresenter(view, publisherRepository);
            }
            else
            {
                PublisherView._instance.BringToFront();
            }
            
        }

        private void ShowTitlesView(object sender, EventArgs e)
        {
            if (TitleView._instance == null || TitleView._instance.IsDisposed)
            {
                ITitleView view = TitleView.GetInstance((Form)_mainView);
                ITitleRepository titleRepository = new TitleRepository(_sqlConnection);
                IPublisherRepository publisherRepository = new PublisherRepository(_sqlConnection);
                new TitlePresenter(view, titleRepository, publisherRepository);
            }
            else
            {
                TitleView._instance.BringToFront();
            }
            
        }
    }
}
