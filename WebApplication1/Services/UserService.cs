using Microsoft.EntityFrameworkCore;
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
        /// <summary>
        /// Проверка аутентификации
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Пользователя если пара логин/пароль существует в базе, Null если иначе.</returns>
        Task<User> Authenticate(string username, string password);

        /// <summary>
        /// Регистрирует нового пользовтеля
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> Register(string username, string password);
    }

    public class UserService : IUserService
    {
        public async Task<User> Authenticate(string username, string password)
        {            
            using (var db = new AdvertContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
                
                if (user == null)
                    return null;

                return user.WithoutPassword();
            }                          
        }

        public async Task<bool> Register(string username, string password)
        {
            using (var db = new AdvertContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Email = username, Password = password });
                    await db.SaveChangesAsync();
                    
                    return true;
                }
            }

            return false;
        }
    }
}
