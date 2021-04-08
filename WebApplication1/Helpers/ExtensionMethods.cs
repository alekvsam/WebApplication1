using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
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

        /// <summary>
        ///Добавляет к URL сегменты с разделителем
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="segments"></param>
        /// <returns></returns>
        public static string AppendToURL(this string baseURL, params string[] segments)
        {
            return string.Join("/", new[] { baseURL.TrimEnd('/') }
                .Concat(segments.Select(s => s.Trim('/'))));
        }

        /// <summary>
        /// Получаем текстовое значение из аттрибута Description
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
