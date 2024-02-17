using TextDictionaryProject.Parsers;
using TextDictionaryProject.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace TextDictionaryProject.Services
{
    public static class DictionaryService
    {
        public static void CreateDictionary(string path)
        {
            Db.CreateTable();
            AddWords(path);
        }
        public static void CreateTable()
        {

            Db.CreateTable();
            Db.Delete();
        }

        public static void UpdateDictionary(string path)
        {
            AddWords(path);
        }

        public static void CleanDictionary()
        {
            Db.Delete();
        }
        public static List<string> GetWords(string start)
        {
            var words = Db.GetWords(start);
            return words;
        }

        private static void AddWords(string path)
        {
                var dictionaryNumberWords = new Dictionary<string, int>();
                foreach (var word in Parser.ParseFile(path))
                {
                    if (dictionaryNumberWords.ContainsKey(word))
                        dictionaryNumberWords[word]++;
                    else dictionaryNumberWords[word] = 1;
                }

                var items = dictionaryNumberWords.Where(item => item.Key.Length >= 3 && item.Value >= 3).Select(t => (t.Key, t.Value)).ToList();
                while (items.Any())
                {
                    Db.AddWords(items.Take(1000));
                    items = items.Skip(1000).ToList();
                }

        }
    }
}