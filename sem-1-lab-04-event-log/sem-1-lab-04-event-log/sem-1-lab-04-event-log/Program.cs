

namespace sem_1_lab_04_event_log
{ // Fraction - дробь(неправильная)
    struct LogEntry
    {
        public DateTime Timestamp;
        public string Level;
        public string Category;
        public string Message;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("event.log");

            LogEntry[] entries = ParseLog(lines);
            
        }


        // сначала level, после category
        private static LogEntry[] ParseLog(string[] lines)
        {
            List<LogEntry> entries = new List<LogEntry>();

            int firstOpenBracket;
            int firstCloseBracket;
            int secondOpenBracket;
            int secondCloseBracket;
            for (int i = 0; i < lines.Length; i++)
            {
                firstOpenBracket = lines[i].IndexOf('[');
                firstCloseBracket = lines[i].IndexOf(']', firstOpenBracket);
                secondOpenBracket = lines[i].IndexOf('[', firstCloseBracket);
                secondCloseBracket = lines[i].IndexOf(']', secondOpenBracket);

                LogEntry entry = new LogEntry();
                entry.Level = lines[i].Substring(firstOpenBracket + 1, firstCloseBracket - firstOpenBracket - 1);
                entry.Category = lines[i].Substring(secondOpenBracket + 1, secondCloseBracket - secondOpenBracket - 1);
                entry.Message = lines[i].Substring(secondCloseBracket + 2);
                entry.Timestamp = DateTime.Parse(lines[i].Substring(0, 23));

                entries.Add(entry);
            }

            return entries.ToArray();

        }
        
        private static LogEntry[] FilterByDate(LogEntry[] entries, DateTime date)
        {
            List<LogEntry> entriesRes = new List<LogEntry>();

            for (int i = 0; i < entries.Length; i++)
            { 

                if (entries[i].Timestamp.Date == date.Date)
                {
                    entriesRes.Add(entries[i]);
                }
                
            }

            return entriesRes.ToArray();
        }
        
        private static LogEntry[] FilterByLevel(LogEntry[] entries, string level)
        {
            List<LogEntry> entriesRes = new List<LogEntry>();

            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == level)
                {
                    entriesRes.Add(entries[i]);
                }

            }

            return entriesRes.ToArray();
        }

        private static LogEntry[] FilterByCategory(LogEntry[] entries, string category) 
        {
            List<LogEntry> entriesRes = new List<LogEntry>();

            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Category == category)
                {
                    entriesRes.Add(entries[i]);
                }

            }

            return entriesRes.ToArray();
        }

        private static LogEntry[] Search(LogEntry[] entries, string text)
        {
            List<LogEntry> entriesRes = new List<LogEntry>();

            for (int i = 0; i < entries.Length; i++)
            {
                if ((entries[i].Message.Contains(text, StringComparison.OrdinalIgnoreCase)))
                {
                    entriesRes.Add(entries[i]);
                }

            }

            return entriesRes.ToArray();
        }
        private static int CountByLevel(LogEntry[] entries, string level)
        {
            int count = 0;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == level)
                {
                    count++;
                }

            }

            return count;
        }
        private static string GetServerStatus(LogEntry[] entries)
        {
            string res = "Сервер работает штатно";
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == "Fatal" && entries[i].Category == "Server")
                {
                    return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";
                }
                else if (entries[i].Level == "Error" || entries[i].Level == "Fatal")
                {
                    res = "Есть ошибки: требуется проверка";
                }

            }

            return res;
        }
    }
}
