using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ITitleRepository
    {
        string Add(TitleModel title);
        string Edit(TitleModel title);
        string Delete(string isbn);
        IEnumerable<TitleModel> GetAll();
        IEnumerable<TitleModel> GetByValue(string searchValue);
        TitleModel GetTitleByISBN(string ISBN);
    }
}
