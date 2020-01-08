using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Doctorly.CodeTest.Data
{
    public interface IDataContext
    {
        Task<object> Insert(object entity);
        Task<object> Update(object entity);
        Task<object> Delete(int id);
        Task<IEnumerable<object>> Get();
        Task<object> Get(int id);
    }
}
