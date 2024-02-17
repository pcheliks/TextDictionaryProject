using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace TextDictionaryProject.Repositories
{
    public static class Db
    {
        private static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True";
        public static void CreateTable()
        {
            var sqlExpression = @"
    IF object_id('Words') is null
                    CREATE TABLE  Words
                    (
                        Id INT IDENTITY PRIMARY KEY,
                        Word NVARCHAR(30) NOT NULL,
                        Count INT DEFAULT 0,
                    ); 
";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }

        public static List<string> GetWords(string start)
        {
            var sqlExpression = $@"
            SELECT DISTINCT TOP 5 Word, WordCount FROM Words
            WHERE Word LIKE N'{start}%'
            ORDER BY WordCount DESC;
            ";

            var words = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(sqlExpression, connection);
                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var word = reader.GetValue(0);
                            words.Add(word.ToString());
                        }
                    }
                    return words;
                }
            }
        }

        public static void AddWords(IEnumerable<(string, int)> values)
        {
            var words = values.Select(item => $"(N'{item.Item1}', {item.Item2})");
            var sqlExpression = $@"
                INSERT INTO WORDS(Word, Count) VALUES {string.Join(", ", words)}
                ";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var number = command.ExecuteNonQuery();
            }
        }
        public static void Delete()
        {
            var sqlExpression = $"DELETE Words;";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }
    }
}
