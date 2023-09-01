using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPublisherRepository
    {
        string Add(PublisherModel publisher);
        string Edit(PublisherModel publisher);
        string Delete(int id);
        IEnumerable<PublisherModel> GetAll();
        IEnumerable<PublisherModel> GetByValue(string searchValue);
        PublisherModel GetByID(int id);
    }
}
