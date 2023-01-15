using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите дату");
            string[] input = Console.ReadLine().Split('.');
            Date date = new Date(Int32.Parse(input[0]), Int32.Parse(input[1]), Int32.Parse(input[2]));
            Console.WriteLine("Введите m");
            int m = Int32.Parse(Console.ReadLine());
            Console.WriteLine(date.BeforeDate(m));

        }


        struct Date
        {
            private int day;
            private int month;
            private int year;

            //Конструктор класса
            public Date(int day, int month, int year)
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }

            //Получить текущую дату
            public string CurrentDate()
            {
                return $"Дата сейчас = {DateOfNumber(CurrentNumber())}";
            }
            //Получить дату, которая была n дней назад
            public string BeforeDate(int n)
            {
                return $"{n} дней назад было: {DateOfNumber(CurrentNumber() - n)}";
            }

            //Получить дату в строке по дню от рождества христова
            private string DateOfNumber(int current_number)
            {
                return $"{GetDay(current_number)}.{GetMonth(current_number)}.{GetYear(current_number)}";
            }

            //Получить текущий день от рождества христова
            private int CurrentNumber()
            {
                //Функция по дате возвращает колличество дней, например ввели 12.12.2022 - функция вернула все дни с 1.1.0 по 12.12.2022
                return YearsInDays(this.year - 1) + MonthInDays(this.month - 1, this.year) + this.day;
            }

            //(год,день,месяц) по дню от рождества христова
            private int GetDay(int current_number)
            {
                //Нам известно колличество дней в прошлых годах  (Years_In_Days(Get_year(current_number) - 1)
                //Известно колличество дней в этом году, не считая текущего месяца   Month_In_Days(Get_month(current_number) - 1, Get_year(current_number)))
                //Тут минус 1 везде потому что нам не надо считать количество дней в текущем месяце и текущем году
                //Ну и из нашего большого числа вычитаем все это и получаем номер дня, в нашей дате
                return current_number - (YearsInDays(GetYear(current_number) - 1) + MonthInDays(GetMonth(current_number) - 1, GetYear(current_number)));
            }
            private int GetMonth(int current_number)
            {
                //Ну, тут находим номер месяца в нашем году, ищем также по дням
                //Из общего колличество дней вычитаем Все дни, которые находиться в предыдущих годах
                //Например, у нас дата 31.5.2023, в функции Current_Number() мы преобразовали дату в дни
                //В Get_year Мы по этим дням определили текущий год

                //Здесь мы находим колличество дней в нашем году
                //из current_number(это все дни) Вычитаем колличестве дней за прошлые года, важно, не вычитать с этим годом, т.е если 2023 вычитаем все до 2022, т.к функция Year_In_Days() Считает все дни в годах включая год
                //В итоге получаем колличество дней в шаем году
                //По нему находим наш месяц
                int days_in_year = current_number - YearsInDays(GetYear(current_number) - 1);
                int current_month = 0;
                //Запускаем цикл, пока дней больше нуля, работа, по определенным условия будем вычитать из него дни
                //В current_month Будем записывать номер месяца, на каждом шагу цикла он будет увеличиваться
                while (days_in_year > 0)
                {
                    //Прибавляем наш месяц
                    current_month++;
                    //Тут будут идти два главных условия if, проверка на то, четный у нас месяц или нечетных
                    if (current_month % 2 != 0)
                    {
                        //В первых семи нечетных месяцах года 31 день, поэтому вычитает 31
                        if (current_month <= 7)
                            days_in_year -= 31;
                        //Иначе,если у нас нечетный месяц, но во второй половине года, вычитаем 30
                        else
                            days_in_year -= 30;
                    }
                    //Тут ситуация аналогичная, но все наоборот, рассматриваем четные месяцы
                    else if (current_month % 2 == 0)
                    {
                        if (current_month <= 7)
                            days_in_year -= 30;
                        else
                            days_in_year -= 31;
                        //Вот здесь единственное отличие,если наш месяц февраль(в нем 28 или 29 дней) 
                        if (current_month == 2)
                        {
                            //Прибавляем 2 дня, так как мы вычили за него 30, (считаем, что в нем 28)
                            days_in_year += 2;
                            //А тут условие на високосный год, если выполняется, то возвращаем 1 день, считаем, что в феврале 29
                            if (GetYear(current_number) % 4 == 0)
                                days_in_year -= 1;
                        }
                    }
                }

                return current_month;
            }
            //Тут мы получаем год по нашему числу, указывающему сколько дней прошло от 1.1.0
            private int GetYear(int current_number)
            {
                //1461=(365+365+365+366),как известно, каждый 4-ый год високосный, и в нем 366 дней, так что это надо учитывать
                //(current_number / 1461) * 4 - тут будет выводиться ближайший к нашем году високосный , например изначальная дата 2023, этот оператор выведет 2020
                //Для получения точного года прибавляем остаток: ((current_number % 1461) / 365)
                int current_year = (current_number / 1461) * 4 + ((current_number % 1461) / 365);
                //МЫ получили год, допустим 2022 и осталось еще что-то в остатке, например 3, то тогда, логично, что наш год 2023, январь, 3 число, если же у нас 0, то сейчас 31.12.2022
                //Это условие именно это проверяет
                if ((current_year / 4 * 366) + (current_year - (current_year / 3)) * 365 > current_year)
                    current_year++;
                return current_year;
            }

            //Колличество дней в этом и предыдущих годах(передаем год)
            private int YearsInDays(int year)
            {
                //year/4-колличество високосных годов до текущего,       (year - (year / 4))-колличество невисокосных         Умножаем их на колличество дней 
                //Функцияя будет передавать колличество дней, например, ввели 2023, функция вывела все дни считая с 1.1.0 по 31.12.2023
                return ((year / 4 * 366) + (year - (year / 4)) * 365);
            }
            //Колличество дней в этом и предыдущих месяцах в указанном году
            //Сюда передаем месяц и год, т.к года бывают високосными
            private int MonthInDays(int month, int year)
            {
                int month_in_days = 0;
                //Узнаем какой по счету идет день в этом году, считаем, что если месяц нечетный, то в нем 31, если четный, то в нем 30 дней, дальше корректируем эти условия
                month_in_days += ((month / 2) * 30) + ((month - month / 2) * 31);
                //Если больше 1 месяца, То вычитаем 2 дня, так как в феврале 28 или 29
                if (month >= 2)
                    month_in_days -= 2;
                //если месяц 10-ый или 8-ой то прибавляем 1 день, это делает потому, что в 7 и 8 (июле и августе) по 31-му дню подряд
                if (month == 10 || month == 8)
                    month_in_days++;
                //Ну и если год високосный и уже февраль прошел, то прибавляем еще 1 день (29 учитываем)
                if (year % 4 == 0 && month >= 2)
                    month_in_days++;
                //Передаем колличество дней в году по месяцу, например, если ввели апрель, 2023 (то функция находит все дни в 2023 году с 1 января по 30 апреля)
                return month_in_days;
            }

        }
    }
}