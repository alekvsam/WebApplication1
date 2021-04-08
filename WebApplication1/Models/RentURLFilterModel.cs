using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NickBuhro.Translit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;

namespace WebApplication1.Models
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RentType : byte
    {
        /// <summary>
        /// Отсутствует
        /// </summary>
        None = 0,
        /// <summary>
        /// Посуточно
        /// </summary>
        [Description("posutochno")]
        Daily = 1,
        /// <summary>
        /// На дительный срок
        /// </summary>
        [Description("na_dlitelnyy_srok")]
        OnLongTerm = 2
    }

    public class RentURLFilterModel : IURLFilter
    {
        /// <summary>
        /// Город в котором размещается объявление
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Количество комнат в квартире
        /// </summary>
        [Range(1, 5)]
        public byte Rooms { get; set; }
        /// <summary>
        /// Тип сдачи недвижимости (посуточно, на длительный срок)
        /// </summary>
        public RentType Type { get; set; }
        /// <summary>
        /// Минимальная цена
        /// </summary>
        [Range(0, 100000)]
        public ushort PriceMin { get; set; }
        /// <summary>
        /// Максимальная цена
        /// </summary>
        [Range(0, 100000)]
        public ushort PriceMax { get; set; }

        /// <summary>
        /// Возвращает URL на основе заполненной модели данных
        /// </summary>
        /// <returns>URL</returns>
        public string ToURLString()
        {
            string _baseURL = "https://www.avito.ru/";
            NameValueCollection query = System.Web.HttpUtility.ParseQueryString(string.Empty);

            //Для примера https://www.avito.ru/krasnodar/kvartiry/sdam/na_dlitelnyy_srok/2-komnatnye-ASgBAQICAkSSA8gQ8AeQUgFAzAgUkFk?cd=1&pmax=25000
            string url = _baseURL.AppendToURL(
                Transliteration.CyrillicToLatin(City), //город
                "kvartiry", //константа
                "sdam", //константа
                Type.GetDescription(), //текстовое представление значения enum (на длительный срок, посуточно)
                $"{Rooms}-komnatnye" //количество комнат
            );
            query.Add("pmax", PriceMax.ToString());
            query.Add("pmin", PriceMin.ToString());
            query.ToString();

            return $"{url}?{query}";
        }
    }
}
