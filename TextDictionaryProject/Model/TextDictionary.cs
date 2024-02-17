using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDictionaryProject.Model
{
    // Класс представляет модель данных для слов в словаре
    public class TextDictionary
    {
        // Ключевое поле id для идентификации записей в базе данных
        [Key]
        public int id { get; set; }

        // Свойство Word, представляющее слово в словаре
        public string Word { get; set; }
    }

}
