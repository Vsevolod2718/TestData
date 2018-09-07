using System;

namespace CreateTestData.DataManipulation
{
    public class SqlDateTimeFactory
    {
        private Random rand;
        private bool exitLoopDateTime;
        private bool exitLoopDate;
        private int yearValueRandom;
        private int monthValueRandom;
        private int monthDayRandom;
        private int hourValueRandom;
        private int minuteValueRandom;
        private int secondsValueRandom;
        private int milliSecondsValueRandom;
        private int originalDate;
        private int originalTime;
        private int newDate;
        private int newTime;
        private int chYear;
        private int chMonth;
        private int chMonthNumber;
        private int сhHour;
        private int chMinute;
        private int chSeconds;
        private int chMilliSeconds;
        private int yearIncluded;
        private int yearChange;
        private int monthChange;
        private int monthNumberChange;
        private int initialDate;
        private int buildDate;
        private int setYear;
        private int setMonth;
        private int setMonthDay;
        private int setHour;
        private int setMinute;
        private int setSeconds;
        private int setMilliSeconds;
        private int setYearDT;
        private int setMonthDT;
        private int setMonthDayDT;
        private int setHourDT;
        private int setMinuteDT;
        private int setSecondsDT;
        private int setMilliSecondsDT;
        private int setYearD;
        private int setMonthD;
        private int setMonthDayD;
        private int setHourT;
        private int setMinuteT;
        private int setSecondsT;
        private int setMilliSecondsT;
        private int setYearLDT;
        private int setMonthLDT;
        private int setMonthDayLDT;
        private int setHourLDT;
        private int setMinuteLDT;
        private int setSecondsLDT;
        private int setMilliSecondsLDT;
        private int setYearLD;
        private int setMonthLD;
        private int setMonthDayLD;
        private string strChDate;
        private string strChHour;
        private string strChMinute;
        private string strChSeconds;
        private string strChMilliSeconds;
        private string strResultDateTime;
        private string yearValueString;
        private string monthValueString;
        private string monthNumberString;
        private string hourValueString;
        private string minuteValueString;
        private string secondsValueString;
        private string milliSecondsValueString;
        private string dateTimeValueString;
        private string dateValueString;
        private string timeValueString;
        private string timeString;
        private string dateString;
        private string dateTimeStr;
        private string strResultDate;
        private string strYear;
        private string strMonth;
        private string strMonthNumber;
        private string strRandDate;
        private string strRandTime;
        private string strLargerDateTime;
        private string strLargerDate;

        // конструктор класса DateTimeFactory
        public SqlDateTimeFactory()
        {
            rand = new Random();
        }

        // Метод возвращает строку случайно выбраной ДатыВремени в виде объекта DATETIME из диапазона годов от minYear до maxYear включительно.
        // Перегрузка метода возвращает случайно выбранную ДатуВремя через выходные параметры в виде целых чисел. 
        public string GetRandomDateTime(int minYear, int maxYear)
        {
            dateTimeStr = GetRandomDateTime(minYear, maxYear, out setYearDT, out setMonthDT, out setMonthDayDT, out setHourDT, out setMinuteDT, out setSecondsDT, out setMilliSecondsDT);

            return dateTimeStr;
        }
        public string GetRandomDateTime(int minYear, int maxYear, out int outYear, out int outMonth, out int outMonthDay, out int outHour, out int outMinute, out int outSeconds, out int outMilliSeconds)
        {
            dateString = GetRandomDate(minYear, maxYear, out setYear, out setMonth, out setMonthDay);

            timeString = GetRandomTime(out setHour, out setMinute, out setSeconds, out setMilliSeconds);

            // передача значений в выходные параметры метода
            outYear = setYear;
            outMonth = setMonth;
            outMonthDay = setMonthDay;
            outHour = setHour;
            outMinute = setMinute;
            outSeconds = setSeconds;
            outMilliSeconds = setMilliSeconds;

            // инициализация возвращаемой методом строки ДатыВремени 
            dateTimeValueString = dateString + " " + timeString;

            return dateTimeValueString;
        }

        // Метод возвращает строку случайно выбраной Даты в виде объекта DATE из диапазона годов от minYear до maxYear включительно.
        // Перегрузка метода возвращает случайно выбранную Дату через выходные параметры в виде целых чисел. 
        public string GetRandomDate(int maxYear, int minYear)
        {
            strRandDate = GetRandomDate(maxYear, minYear, out setYearD, out setMonthD, out setMonthDayD);

            return strRandDate;
        }
        public string GetRandomDate(int maxYear, int minYear, out int outYear, out int outMonth, out int outMonthDay)
        {
            yearIncluded = minYear + 1;

            // Год
            yearValueRandom = rand.Next(maxYear, yearIncluded);
            yearValueString = Convert.ToString(yearValueRandom);
            // Месяц                
            monthValueRandom = rand.Next(1, 13);
            monthValueString = Convert.ToString(monthValueRandom);
            if (monthValueString.Length == 1)
            {
                monthValueString = "0" + monthValueString;
            }
            // Число месяца
            switch (monthValueRandom)
            {
                case 1: // январь 31 

                    monthDayRandom = rand.Next(1, 32);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 2: // февраль 28 и 29 дней, високосный год или нет

                    // Проверка года на високосность
                    if (yearValueRandom % 4 == 0)
                    {
                        monthDayRandom = rand.Next(1, 30);

                        monthNumberString = Convert.ToString(monthDayRandom);
                    }
                    else
                    {
                        monthDayRandom = rand.Next(1, 29);

                        monthNumberString = Convert.ToString(monthDayRandom);
                    }
                    break;

                case 3: // март 31  

                    monthDayRandom = rand.Next(1, 32);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 4: // апрель 30 

                    monthDayRandom = rand.Next(1, 31);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 5: // май 31  

                    monthDayRandom = rand.Next(1, 32);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 6: // июнь 30  

                    monthDayRandom = rand.Next(1, 31);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 7: // июль 31 

                    monthDayRandom = rand.Next(1, 32);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 8: // август 31  

                    monthDayRandom = rand.Next(1, 32);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 9: // сентябрь 30 

                    monthDayRandom = rand.Next(1, 31);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 10: // октябрь 31 

                    monthDayRandom = rand.Next(1, 32);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 11: // ноябрь 30 

                    monthDayRandom = rand.Next(1, 31);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                case 12: // декабрь 31 

                    monthDayRandom = rand.Next(1, 32);

                    monthNumberString = Convert.ToString(monthDayRandom);

                    break;

                default: // генерируем исключение

                    throw new Exception("Выход за диапазон значений порядкового номера месяца!");
            }
            if (monthNumberString.Length == 1)
            {
                monthNumberString = "0" + monthNumberString;
            }

            // передача значений в выходные параметры метода
            outYear = yearValueRandom;
            outMonth = monthValueRandom;
            outMonthDay = monthDayRandom;

            // инициализация возвращаемой методом строки Даты
            dateValueString = yearValueString + monthValueString + monthNumberString;

            return dateValueString;
        }

        // Метод возвращает строку случайно выбранного Времени в виде объекта TIME.
        // Перегрузка метода возвращает случайно выбранное Время через выходные параметры в виде целых чисел.
        public string GetRandomTime()
        {
            strRandTime = GetRandomTime(out setHourT, out setMinuteT, out setSecondsT, out setMilliSecondsT);

            return strRandTime;
        }
        public string GetRandomTime(out int outHour, out int outMinute, out int outSeconds, out int outMilliSeconds)
        {
            // Часы
            hourValueRandom = rand.Next(0, 24);
            hourValueString = Convert.ToString(hourValueRandom);
            if (hourValueString.Length == 1)
            {
                hourValueString = "0" + hourValueString;
            }
            // Минуты 
            minuteValueRandom = rand.Next(0, 60);
            minuteValueString = Convert.ToString(minuteValueRandom);
            if (minuteValueString.Length == 1)
            {
                minuteValueString = "0" + minuteValueString;
            }
            // секунды 
            secondsValueRandom = rand.Next(0, 60);
            secondsValueString = Convert.ToString(secondsValueRandom);
            if (secondsValueString.Length == 1)
            {
                secondsValueString = "0" + secondsValueString;
            }
            // миллисекунды 
            milliSecondsValueRandom = rand.Next(0, 1000);
            milliSecondsValueString = Convert.ToString(milliSecondsValueRandom);
            if (milliSecondsValueString.Length == 3)
            {

            }
            else if (milliSecondsValueString.Length == 2)
            {
                milliSecondsValueString = "0" + milliSecondsValueString;
            }
            else
            {
                milliSecondsValueString = "00" + milliSecondsValueString;
            }

            // передача значений в выходные параметры метода
            outHour = hourValueRandom;
            outMinute = minuteValueRandom;
            outSeconds = secondsValueRandom;
            outMilliSeconds = milliSecondsValueRandom;

            // инициализация возвращаемой методом строки Времени
            timeValueString = hourValueString + ":" + minuteValueString + ":" + secondsValueString + "." + milliSecondsValueString;

            return timeValueString;
        }

        // метод возвращает строку ДатыВремени большей или равной ДатеВремени представленнаой через параметры этого метода.   
        // Случайная Дата по умолчанию. находится в диапазоне от года  "year"  до года "year + 1" включительно.
        // Параметры:
        // year - значение года
        // month - порядковый номер месяца
        // monthNumber - число месяца
        // hour - значение часа
        // minute - значение минут
        // seconds - значение секунд
        // milliSeconds - значение миллисекунд      
        public string GetLargerDateTime(int year, int month, int monthNumber, int hour, int minute, int seconds, int milliSeconds)
        {
            strLargerDateTime = GetLargerDateTime(year, month, monthNumber, hour, minute, seconds, milliSeconds, out setYearLDT, out setMonthLDT, out setMonthDayLDT, out setHourLDT, out setMinuteLDT, out setSecondsLDT, out setMilliSecondsLDT);

            return strLargerDateTime;
        }
        public string GetLargerDateTime(int year, int month, int monthNumber, int hour, int minute, int seconds, int milliSeconds, out int outYear, out int outMonth, out int outMonthDay, out int outHour, out int outMinute, out int outSeconds, out int outMilliSeconds)
        {
            originalDate = year * 10000 + month * 100 + monthNumber;
            originalTime = hour * 10000000 + minute * 100000 + seconds * 1000 + milliSeconds;

            // флаг для выхода из цикла ставим в исходное положение
            exitLoopDateTime = false;

            do
            {
                // случайный выбор  от года (year) до конца года  (year + 1)
                chYear = rand.Next(year, year + 2);
                // случайный выбор месяца 
                chMonth = rand.Next(1, 13);
                // случайный выбор месяца
                switch (chMonth)
                {
                    case 1: // январь 31 

                        chMonthNumber = rand.Next(1, 32);

                        break;

                    case 2: // февраль 28 и 29 дней, високосный год или нет

                        // Проверка года на високосность
                        if (chYear % 4 == 0)
                        {
                            chMonthNumber = rand.Next(1, 30);
                        }
                        else
                        {
                            chMonthNumber = rand.Next(1, 29);
                        }
                        break;

                    case 3: // март 31  

                        chMonthNumber = rand.Next(1, 32);

                        break;

                    case 4: // апрель 30 

                        chMonthNumber = rand.Next(1, 31);

                        break;

                    case 5: // май 31  

                        chMonthNumber = rand.Next(1, 32);

                        break;

                    case 6: // июнь 30  

                        chMonthNumber = rand.Next(1, 31);

                        break;

                    case 7: // июль 31 

                        chMonthNumber = rand.Next(1, 32);

                        break;

                    case 8: // август 31  

                        chMonthNumber = rand.Next(1, 32);

                        break;

                    case 9: // сентябрь 30 

                        chMonthNumber = rand.Next(1, 31);

                        break;

                    case 10: // октябрь 31 

                        chMonthNumber = rand.Next(1, 32);

                        break;

                    case 11: // ноябрь 30 

                        chMonthNumber = rand.Next(1, 31);

                        break;

                    case 12: // декабрь 31 

                        chMonthNumber = rand.Next(1, 32);

                        break;

                    default: // генерируем исключение

                        throw new Exception("Выход за диапазон значений порядкового номера месяца!");
                }
                // случайный выбор часа
                сhHour = rand.Next(0, 24);
                // случайный выбор минут 
                chMinute = rand.Next(0, 60);
                // случайный выбор секунд
                chSeconds = rand.Next(0, 60);
                // случайный выбор миллисекунд 
                chMilliSeconds = rand.Next(0, 1000);

                newDate = chYear * 10000 + chMonth * 100 + chMonthNumber;
                newTime = сhHour * 10000000 + chMinute * 100000 + chSeconds * 1000 + chMilliSeconds;

                if (originalDate < newDate)
                {
                    // конвертируем значение созданной даты в строку
                    strChDate = Convert.ToString(newDate);
                    // конвертация значения созданого времени в строку из отдельных компонентов
                    // часы
                    strChHour = Convert.ToString(сhHour);
                    if (strChHour.Length == 1)
                    {
                        strChHour = "0" + strChHour;
                    }
                    // минуты
                    strChMinute = Convert.ToString(chMinute);
                    if (strChMinute.Length == 1)
                    {
                        strChMinute = "0" + strChMinute;
                    }
                    // секунды
                    strChSeconds = Convert.ToString(chSeconds);
                    if (secondsValueString.Length == 1)
                    {
                        strChSeconds = "0" + strChSeconds;
                    }
                    // миллисекунды
                    strChMilliSeconds = Convert.ToString(chMilliSeconds);
                    if (strChMilliSeconds.Length == 3)
                    {

                    }
                    else if (strChMilliSeconds.Length == 2)
                    {
                        strChMilliSeconds = "0" + strChMilliSeconds;
                    }
                    else
                    {
                        strChMilliSeconds = "00" + strChMilliSeconds;
                    }

                    // помещаем строку датуВремя в переменную  strResultDateTime
                    strResultDateTime = strChDate + " " + strChHour + ":" + strChMinute + ":" + strChSeconds + "." + strChMilliSeconds;

                    // выход из цикла
                    exitLoopDateTime = true;
                }

                if (originalDate == newDate && originalTime <= newTime)
                {
                    // конвертируем значение созданной даты в строку
                    strChDate = Convert.ToString(newDate);
                    // конвертация значения созданого времени в строку из отдельных компонентов
                    // часы
                    strChHour = Convert.ToString(сhHour);
                    if (strChHour.Length == 1)
                    {
                        strChHour = "0" + strChHour;
                    }
                    // минуты
                    strChMinute = Convert.ToString(chMinute);
                    if (strChMinute.Length == 1)
                    {
                        strChMinute = "0" + strChMinute;
                    }
                    // секунды
                    strChSeconds = Convert.ToString(chSeconds);
                    if (secondsValueString.Length == 1)
                    {
                        strChSeconds = "0" + strChSeconds;
                    }
                    // миллисекунды
                    strChMilliSeconds = Convert.ToString(chMilliSeconds);
                    if (strChMilliSeconds.Length == 3)
                    {

                    }
                    else if (strChMilliSeconds.Length == 2)
                    {
                        strChMilliSeconds = "0" + strChMilliSeconds;
                    }
                    else
                    {
                        strChMilliSeconds = "00" + strChMilliSeconds;
                    }

                    // помещаем строку датуВремя в переменную  strResultDateTime
                    strResultDateTime = strChDate + " " + strChHour + ":" + strChMinute + ":" + strChSeconds + "." + strChMilliSeconds;

                    // выход из цикла
                    exitLoopDateTime = true;
                }
            }
            while (exitLoopDateTime != true);

            // передача значений в выходные параметры метода
            outYear = chYear;
            outMonth = chMonth;
            outMonthDay = chMonthNumber;
            outHour = сhHour;
            outMinute = chMinute;
            outSeconds = chSeconds;
            outMilliSeconds = chMilliSeconds;

            return strResultDateTime;
        }

        // метод возвращает строку Даты больше или равной Дате представленная через параметры метода     
        // Параметры:
        // year - значение года
        // month - порядковый номер месяца
        // monthNumber - число месяца
        public string GetLargerDate(int year, int month, int monthNumber)
        {
            strLargerDate = GetLargerDate(year, month, monthNumber, out setYearLD, out setMonthLD, out setMonthDayLD);

            return strLargerDate;
        }
        public string GetLargerDate(int year, int month, int monthNumber, out int outYear, out int outMonth, out int outMonthDay)
        {
            initialDate = year * 10000 + month * 100 + monthNumber;

            exitLoopDate = false;

            do
            {
                // год
                yearChange = rand.Next(year, 2019);
                // месяц
                monthChange = rand.Next(1, 13);
                // число месяца
                switch (monthChange)
                {
                    case 1: // январь 31 

                        monthNumberChange = rand.Next(1, 32);

                        break;

                    case 2: // февраль 28 и 29 дней, високосный год или нет

                        // Проверка года на високосность
                        if (yearChange % 4 == 0)
                        {
                            monthNumberChange = rand.Next(1, 30);
                        }
                        else
                        {
                            monthNumberChange = rand.Next(1, 29);
                        }
                        break;

                    case 3: // март 31  

                        monthNumberChange = rand.Next(1, 32);

                        break;

                    case 4: // апрель 30 

                        monthNumberChange = rand.Next(1, 31);

                        break;

                    case 5: // май 31  

                        monthNumberChange = rand.Next(1, 32);

                        break;

                    case 6: // июнь 30  

                        monthNumberChange = rand.Next(1, 31);

                        break;

                    case 7: // июль 31 

                        monthNumberChange = rand.Next(1, 32);

                        break;

                    case 8: // август 31  

                        monthNumberChange = rand.Next(1, 32);

                        break;

                    case 9: // сентябрь 30 

                        monthNumberChange = rand.Next(1, 31);

                        break;

                    case 10: // октябрь 31 

                        monthNumberChange = rand.Next(1, 32);

                        break;

                    case 11: // ноябрь 30 

                        monthNumberChange = rand.Next(1, 31);

                        break;

                    case 12: // декабрь 31 

                        monthNumberChange = rand.Next(1, 32);

                        break;

                    default: // генерируем исключение

                        throw new Exception("Выход за диапазон значений порядкового номера месяца!");
                }

                buildDate = yearChange * 10000 + monthChange * 100 + monthNumberChange;

                // если начальная дата initialDate меньше ил равна созданной даты buildDate, то созданную дату buildDate конвнртируем в строку и выходим из цикла do...while
                if (initialDate <= buildDate)
                {
                    // год
                    strYear = Convert.ToString(yearChange);
                    // месяц
                    strMonth = Convert.ToString(monthChange);
                    if (strMonth.Length == 1)
                    {
                        strMonth = "0" + strMonth;
                    }
                    // число месяца
                    strMonthNumber = Convert.ToString(monthNumberChange);
                    if (strMonthNumber.Length == 1)
                    {
                        strMonthNumber = "0" + strMonthNumber;
                    }

                    strResultDate = strYear + strMonth + strMonthNumber;

                    // флаг для выхода из цикла while
                    exitLoopDate = true;
                }
            }
            while (exitLoopDate != true);

            // передача значений в выходные параметры метода
            outYear = yearChange;
            outMonth = monthChange;
            outMonthDay = monthNumberChange;

            return strResultDate;
        }
    }
}
