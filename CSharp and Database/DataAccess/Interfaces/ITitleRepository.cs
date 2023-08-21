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
        void Add(TitleModel title);
        void Edit(TitleModel title);
        void Delete(string isbn);
        IEnumerable<TitleModel> GetAll();
        IEnumerable<TitleModel> GetByValue(string searchValue);
    }
}
