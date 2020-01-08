
using Doctorly.CodeTest.Repository.DTOS;
using System;
using System.Collections.Generic;

namespace Doctorly.CodeTest.Test.Helper
{
    public class ConcreteObjects
    {
        public static ServiceConfigOptions ServiceOptions => new ServiceConfigOptions
        {
            Environment = "Test Environment",
            ServiceName = "Doctorly.CodeTest.REST",
            ServiceVersion = "1.0.0"

        };
        public static IEnumerable<UserDTO> GetUsers()
        {
            List<UserDTO> users = new List<UserDTO>();
            users.Add(new UserDTO { Id = 1, FirstName = "Test User 01", LastName = "Last Namne 01" });
            users.Add(new UserDTO { Id = 2, FirstName = "Test User 02", LastName = "Last Namne 02" });
            users.Add(new UserDTO { Id = 3, FirstName = "Test User 03", LastName = "Last Namne 03" });
            users.Add(new UserDTO { Id = 4, FirstName = "Test User 04", LastName = "Last Namne 04" });
            users.Add(new UserDTO { Id = 5, FirstName = "Test User 05", LastName = "Last Namne 05" });
            users.Add(new UserDTO { Id = 6, FirstName = "Test User 06", LastName = "Last Namne 06" });
            users.Add(new UserDTO { Id = 7, FirstName = "Test User 07", LastName = "Last Namne 07" });

            return users;
        }
    }
}
