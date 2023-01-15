using System;
using System.Text;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        //Загрузка данных в текстовый файл,(файл должен быть создан), передаем путь к файлу
        public static void LoadToFile(Note[] notes, string file)
        {
            StreamWriter sw = new StreamWriter(file, true, Encoding.ASCII); // потоковая запись файла
            for (int i = 0; i < 8; i++)
                sw.WriteLine(notes[i].Info());
            sw.Close();
        }

        //Ищем человека по фамилии
        public static void FindSurname(Note[] notes, string surname)
        {
            //Тут ищем человека, если находим, то выводим о нем инфу и завершаем функцию
            for (int i = 0; i < 8; i++)
            {
                if (surname == notes[i].surname)
                {
                    Console.WriteLine(notes[i].Info());
                    return;
                }
            }
            Console.WriteLine($"Пользователей с фамилией {surname} не найдено");
        }

        //Выводим данные из файла на экран(передаем путь к файлу)
        public static void ReadFromFile(string file)
        {
            //Открытие на чтение
            StreamReader sr = new StreamReader(file); // потоковое чтение файла
            string line = sr.ReadLine();
            //Выводим построчно
            while (line != null)
            {
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            sr.Close();
        }
        //Сортируем наши данные по двум первым цифрам телефона
        public static Note[] SortStruct(Note[] notes)
        {
            Note note = new Note();
            //Тут у нас сортировка пузырьком
            for (int i = 0; i < 8 - 1; i++)
            {
                for (int j = 0; j < 8 - i - 1; j++)
                {
                    //Вот тут мы берем первые две цифры нашего номера телефона
                    //Такой способ:Преобразуем номер телефона в строка, методом substring(0,2) извлекаем из этой страке два первых символа
                    //А потом с помощью Convert.toint32 возвращаем обратно к числу
                    if (Convert.ToInt32(notes[j].number.ToString().Substring(0, 2)) > Convert.ToInt32(notes[j + 1].number.ToString().Substring(0, 2)))
                    {
                        note = notes[j];
                        notes[j] = notes[j + 1];
                        notes[j + 1] = note;
                    }
                }
            }
            return notes;
        }



        static void Main(string[] args)
        {
            Note[] notes = new Note[8];
            string name, surname;
            int number = 0;
            int[] date = new int[3];
            //Задаем наших пользователей
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"Введите имя {i + 1} Пользователя: ");
                name = Console.ReadLine();
                Console.Write($"Введите фамилию {i + 1} Пользователя: ");
                surname = Console.ReadLine();
                Console.Write($"Введите номер телефона {i + 1} Пользователя: ");
                number = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Введите дату рождения {i + 1} Пользователя: ");
                Console.Write("День: ");
                date[0] = Convert.ToInt32(Console.ReadLine());
                Console.Write("Месяц: ");
                date[1] = Convert.ToInt32(Console.ReadLine());
                Console.Write("Год: ");
                date[2] = Convert.ToInt32(Console.ReadLine());
                notes[i].Set(name, surname, number, date);
            }
            //Сортируем пользователей по номеру телефона
            notes = SortStruct(notes);
            //Сохраняем их в файл (C:\\users.txt)
            LoadToFile(notes, "C:\\Dora.txt");
            //Ищем челика по фамилии

            Console.Clear();
            Console.WriteLine("Введите фамилию человека");
            surname = Console.ReadLine();
            FindSurname(notes, surname);
            //Считываем наши данные из файла
            ReadFromFile("notes.txt");
        }
        public struct Note
        {
            public string name;
            public string surname;
            public int number;
            public int[] date;
            //Конструктор
            public Note(string name, string surname, int number, int[] date)
            {
                this.name = name;
                this.surname = surname;
                this.number = number;
                this.date = new int[3];
                this.date[0] = date[0];
                this.date[1] = date[1];
                this.date[2] = date[2];
            }
            //Задаем определенные значения нашей структуре
            public void Set(string name, string surname, int number, int[] date)
            {
                this.name = name;
                this.surname = surname;
                this.number = number;
                this.date = new int[3];
                this.date[0] = date[0];
                this.date[1] = date[1];
                this.date[2] = date[2];
            }
            //Выводим инфу о нашем челике
            public string Info()
            {
                return $"{this.name} {this.surname} {this.number} {this.date[0]}.{this.date[1]}.{this.date[2]}";
            }
        }
    }
}