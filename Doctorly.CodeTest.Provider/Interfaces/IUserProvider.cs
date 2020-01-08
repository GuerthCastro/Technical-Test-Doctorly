using Doctorly.CodeTest.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctorly.CodeTest.Provider.Interfaces
{
    public interface IUserProvider
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task<User> InsertUser(User user);
        Task<User> UpdatetUser(User user);
        Task<User> DeletetUser(int userId);
    }
}
