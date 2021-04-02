using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage = "Не указано имя пользователя.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Не указан пароль.")]
        public string Password { get; set; }
    }
}
