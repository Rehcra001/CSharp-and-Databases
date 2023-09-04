using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public string Add(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        public string Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string Edit(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public AuthorModel GetAuthorByID(int id)
        {
            throw new NotImplementedException();
        }

        public AuthorModel GetAuthorByISBN(string ISBN)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorModel> GetByValue(string searchValue)
        {
            throw new NotImplementedException();
        }
    }
}
