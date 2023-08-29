using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPublisherRepository
    {
        void Add(PublisherModel publisher);
        void Edit(PublisherModel publisher);
        void Delete(int id);
        IEnumerable<PublisherModel> GetAll();
        IEnumerable<PublisherModel> GetByValue(string searchValue);
        PublisherModel GetByID(int id);
    }
}
