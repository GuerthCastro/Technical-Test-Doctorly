using Doctorly.CodeTest.Repository.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctorly.CodeTestRepository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUser(int userId);
        Task<UserDTO> InsertUser(UserDTO user);
        Task<UserDTO> UpdatetUser(UserDTO user);
        Task<UserDTO> DeletetUser(int userId);
    }
}
