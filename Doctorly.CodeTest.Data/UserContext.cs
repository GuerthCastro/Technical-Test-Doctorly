using Doctorly.CodeTest.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorly.CodeTest.Data
{
    public class UserContext : IDataContext
    {
        //This is a Draft about how I think the a db context coud be developed, a generic or base class with the base operation i'ta a good idea
        ILogger<UserContext> Logger { get; }
        IConfiguration Configuration { get; }
        DatabaseContext DBContext{ get; }
        public UserContext(ILogger<UserContext> logger, IConfiguration configuration)
        {
            Logger = logger;
            Configuration = configuration;
            DBContext = new DatabaseContext(configuration);
        }
        public async Task<object> Insert(object entity)
        {
            try
            {
                var oldUser = DBContext.Users.Where(u => u.UserId.Equals((entity as User).UserId)).FirstOrDefault();
                if (oldUser == null)
                {
                    (entity as User).UserId = 0;
                    var newEntity = DBContext.Users.Add(entity as User);
                    await DBContext.SaveChangesAsync();
                    return entity;
                }
                else
                {
                    var exception = new Exception($"The user with Id: {(entity as User).UserId} already exist");
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error raised trying to insert a new  user");
                throw ex;
            }
        }
        public async Task<object> Update(object entity)
        {
            try
            {
                var oldUser = DBContext.Users.Where(u => u.UserId.Equals((entity as User).UserId)).FirstOrDefault();
                if (oldUser != null)
                {
                    DBContext.Entry(oldUser).CurrentValues.SetValues(entity);
                    await DBContext.SaveChangesAsync();
                    return entity;
                }
                else
                {
                    var exception = new Exception($"The user with Id: {(entity as User).UserId} do not exist");
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error raised trying to update an existing user");
                throw ex;
            }
        }
        public async Task<object> Delete(int id)
        {
            try
            {
                var entity = DBContext.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    var newEntity = DBContext.Users.Remove(entity);
                    await DBContext.SaveChangesAsync();
                    return entity;
                }
                else
                {
                    var exception = new Exception($"The user with Id: {id} do not exist");
                    throw exception;
                }                
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error raised trying to delete an existing user");
                throw ex;
            }
        }
        public async Task<IEnumerable<object>> Get()
        {
            try
            {
                return await Task.FromResult(DBContext.Users.AsEnumerable<object>());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error raised trying to get all existing users");
                throw ex;
            }
        }
        public async Task<object> Get(int id)
        {
            try
            {
                var entity = DBContext.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    return await Task.FromResult<object>(entity);
                }
                else
                {
                    var exception = new Exception($"The user with Id: {id} do not exist");
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error raised trying to get the user with id: {id}");
                throw ex;
            }
        }
    }
}
