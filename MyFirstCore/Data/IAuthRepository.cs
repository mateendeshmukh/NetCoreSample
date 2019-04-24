using MyFirstCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstCore.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string passowrd);
        Task<User> Login(string username, string password);
        Task<bool> UserExsits(string username);
    }
}
