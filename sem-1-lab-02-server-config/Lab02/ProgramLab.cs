namespace Lab02
{
    public class ProgramLab
    {
        static void Main(string[] args)
        {
        }

        public static string CheckConfiguration(int PlayerCount, int ram, bool haveErrors, bool password)
        {
            if (haveErrors == false)
            {
                return "Невозможно запустить север, есть нерешённые ошибки";
            }
            else if (PlayerCount == 0)
            {
                return "Запуск невозможен: количество игроков должно быть больше нуля.";
            }
            else if (password == true)
            {
                return "Запуск возможен с предупреждением: публичный сервер защищён паролем.";

            }
            else if (PlayerCount > 100 && ram < 16)
            {
                return "Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти.";
            }
            else if (ram == 1)
            {
                return "Запуск невозможен: серверу недостаточно оперативной памяти.";
            }
            
            else
            {
                return "Сервер готов к запуску.";
            }

        }




    }
}
