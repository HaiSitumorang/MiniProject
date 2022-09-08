using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Service.Interface.Service
{
    public interface IDBService
    {
        Task<int> ModifyData(string command, object param);
        Task<List<T>> GetData<T>(string command, object param);
    }
}
