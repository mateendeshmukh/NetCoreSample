using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstCore.Models;

namespace MyFirstCore.Data
{
    public class AuthRepository : IAuthRepository
    {
        DataContext _Context;
        public AuthRepository(DataContext Context)
        {
            _Context = Context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user=await _Context.Users.FirstOrDefaultAsync(x=>x.UserName.ToLower().Trim()==username.ToLower().Trim());
            if (user == null)
                return null;
            if(!VerifyPasswordash(password,user.PasswordHash,user.PassowrdSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordash(string password, byte[] passwordHash, byte[] passowrdSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passowrdSalt))
            {
               
              var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputeHash.Length; i++)
                {
                    if (ComputeHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string passowrd)
        {
            byte[] PassowrdHash, PassowrdSalt;
            CreatePasswordHas(passowrd, out PassowrdHash, out PassowrdSalt);
            user.PasswordHash = PassowrdHash;
            user.PassowrdSalt = PassowrdSalt;
            await _Context.AddAsync(user);
            await _Context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHas(string passowrd, out byte[] passowrdHash, out byte[] passowrdSalt)
        {
            using (var hmac =new System.Security.Cryptography.HMACSHA256())
            {
                passowrdSalt = hmac.Key;
                passowrdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passowrd));
            }
            
        }

        public async Task<bool> UserExsits(string username)
        {
            if (await _Context.Users.AnyAsync(x => x.UserName.ToLower().Trim() == username.ToLower().Trim()))
                return true;
            else
                return false;
        }
    }
}
