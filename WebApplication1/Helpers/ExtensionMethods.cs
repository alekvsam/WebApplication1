using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Очищает пароль у пользователя для вывода.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}
