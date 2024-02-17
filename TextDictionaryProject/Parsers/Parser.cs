using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextDictionaryProject.Parsers
{
    public static class Parser
    {
        public static IEnumerable<string> ParseFile(string path)
        {
            using (var fstream = File.OpenRead(path))
            {
                var buffer = new byte[fstream.Length];
                fstream.Read(buffer, 0, buffer.Length);
                var s = Encoding.UTF8.GetString(buffer);
                return Parser.Parse(s);
            }
        }

        private static IEnumerable<string> Parse(string text)
        {
            //Списоксимволо по которым надо разделять текст
            var stringsSplit = new string[] {" ", "\n", "\t", ".", ",", ";", ":", "(", ")"};
            //var dict = new Dictionary<string, int>();
            //foreach(var word in text.Split(stringsSplit, System.StringSplitOptions.RemoveEmptyEntries))
            //{
            //    if (!dict.ContainsKey(word))
            //        dict.Add(word, 0);
            //    dict[word]++;
            //}
            return text.Split(stringsSplit, System.StringSplitOptions.RemoveEmptyEntries);
        }

    }
}