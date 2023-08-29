using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB_WPF_MVVM.Services
{
    public class SharedDataService : ISharedDataService
    {
        private object _parameter = null!;
        public object Parameter 
        { 
            get => _parameter;
            set => _parameter = value; 
        }
    }
}
