using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Doctorly.CodeTest.Provider.Interfaces;
using Doctorly.CodeTest.Data;
using Doctorly.CodeTest.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Doctorly.CodeTest.Provider.Providers
{
    public class UserProvider : IUserProvider
    {
        ILogger<UserProvider> Logger { get; }
        IConfiguration Configuration { get; }
        IDataContext DataContex { get; }
        public UserProvider(ILogger<UserProvider> logger, IConfiguration configuration, IDataContext dataContext)
        {
            Logger = logger;
            Configuration = configuration;
            DataContex = dataContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await DataContex.Get();
            return users.Select(u => u as User).AsEnumerable();
        }

        public async Task<User> GetUser(int userId)
        {
            return await DataContex.Get(userId) as User;
        }
        public async Task<User> InsertUser(User user)
        {

            var newUser = await DataContex.Insert(user);
            return newUser as User;
        }
        public async Task<User> UpdatetUser(User user)
        {

            var updatedUser = await DataContex.Update(user);
            return updatedUser as User;
        }
        public async Task<User> DeletetUser(int userId)
        {
            var deletedUser = await DataContex.Delete(userId);
            return deletedUser as User;
        }
    }
}
