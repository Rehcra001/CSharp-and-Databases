using BooksDB_WinForms_MVP.Presenters;
using BooksDB_WinForms_MVP.Views;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System.Configuration;

namespace BooksDB_WinForms_MVP
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SQLBOOKSDB"].ConnectionString;
            IMainView view = new MainView();
            new MainPresenter(view, sqlConnectionString);
            Application.Run((Form)view);
        }
    }
}