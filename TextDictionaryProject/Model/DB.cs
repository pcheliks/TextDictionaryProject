using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDictionaryProject.Model
{
    // Класс DB наследуется от DbContext и представляет контекст базы данных
    public class DB : DbContext
    {
        // Конструктор класса с передачей строки подключения "DbConnection"
        public DB()
            : base("DbConnection")
        {
            // Создание базы данных, если она не существует
            Database.CreateIfNotExists();

            // Проверка, содержит ли таблица Words какие-либо записи
            if (!Words.Any())
            {
                // Добавление начальных данных, если таблица Words пуста
                Words.AddRange(new List<TextDictionary>() { new TextDictionary { Word = "Пока" }, new TextDictionary { Word = "Покараю" }, new TextDictionary { Word = "Никита" }, new TextDictionary { Word = "Никровил" } });

                // Сохранение изменений в базе данных
                SaveChanges();
            }
        }

        // DbSet для работы с таблицей Words в базе данных
        public DbSet<TextDictionary> Words { get; set; }
    }

}
