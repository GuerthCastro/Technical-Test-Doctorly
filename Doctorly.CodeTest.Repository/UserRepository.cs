using AutoMapper;
using Doctorly.CodeTest.Data.Entities;
using Doctorly.CodeTest.Provider.Interfaces;
using Doctorly.CodeTest.Repository.DTOS;
using Doctorly.CodeTestRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctorly.CodeTestRepository
{
    public class UserRepository : IUserRepository
    {
        ILogger<UserRepository> Logger { get; }
        IConfiguration Configuration { get; }
        IUserProvider UserProvider { get; }
        IMapper UserMapper { get; }
        IMapper UserDTOMapper { get; }
        public UserRepository(ILogger<UserRepository> logger, IConfiguration configuration, IUserProvider userProvider)
        {
            Logger = logger;
            Configuration = configuration;
            UserProvider = userProvider;
            UserMapper = new MapperConfiguration(cfg => { cfg.CreateMap<UserDTO, User>(); }).CreateMapper();
            UserDTOMapper = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserDTO>(); }).CreateMapper();
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return UserDTOMapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await UserProvider.GetAllUsers());
        }
        public async Task<UserDTO> GetUser(int userId)
        {
            return UserDTOMapper.Map<User, UserDTO>(await UserProvider.GetUser(userId));
        }
        public async Task<UserDTO> InsertUser(UserDTO user)
        {
            var result = UserDTOMapper.Map<User,UserDTO>(await UserProvider.InsertUser(UserMapper.Map<UserDTO, User>(user)));
            return result;
        }
        public async Task<UserDTO> UpdatetUser(UserDTO user)
        {
            var result = UserDTOMapper.Map<User, UserDTO>(await UserProvider.UpdatetUser(UserMapper.Map<UserDTO, User>(user)));
            return result;
        }
        public async Task<UserDTO> DeletetUser(int userId)
        {
            var deletedUser = await UserProvider.DeletetUser(userId);
            var result = UserDTOMapper.Map<User, UserDTO>(deletedUser);
            return result;
        }
    }
}
