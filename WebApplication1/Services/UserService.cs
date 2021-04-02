using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Helpers;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        public async Task<User> Authenticate(string username, string password)
        {            
            using (var db = new AdvertContext())
            {
                var user = await Task.Run(() => db.Users.SingleOrDefault(x => x.Username == username && x.Password == password));
                
                if (user == null)
                    return null;

                return user.WithoutPassword();
            }                          
        }
    }
}
