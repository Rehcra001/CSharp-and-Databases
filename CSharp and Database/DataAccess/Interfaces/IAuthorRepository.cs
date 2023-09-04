using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IAuthorRepository
    {
        string Add(AuthorModel author);
        string Edit(AuthorModel author);
        string Delete(int id);
        IEnumerable<AuthorModel> GetAll();
        IEnumerable<AuthorModel> GetByValue(string searchValue);
        AuthorModel GetAuthorByID(int id);
        AuthorModel GetAuthorByISBN(string ISBN);
    }
}
