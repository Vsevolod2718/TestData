using CreateTestData.DataManipulation;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CreateTestData
{
    // Класс заполняет строки случайными данными
    public class DataFactory
    {
        private SqlDateTimeFactory creatdatetime = new SqlDateTimeFactory();

        // флаг указывающий отменен ли процесс (true) или нет (false)
        private bool cancelled = false;      
        // значение колличества заполняемых строк в таблице Users
        private const int countLinesUsers = 100000;
        // значение колличества заполняемых строк в таблице Resulttests
        private const int countLinesResulttests = 500000;
        // значение колличества заполняемых строк в таблице ListCities
        private int countLinesCities;
        // значение общего колличества запорлняемых строк
        private float totalNumberLines;
        // счетчик количества заполненых строк 
        private int numberFilledLines = 0;
        // Поле для случайных чисел 
        private Random rand = new Random();
        // чтение полученных данных
        private SqlDataReader DataReader = null;
        // счетчик 
        public int Increment { get; private set; } = 0;
        // Поле для подключения к базе данных
        private SqlConnection connection;
        // команда вставки данных в таблицу ListCities
        private SqlCommand insertDataListCities;
        // команда вставки данных в таблицу Users со значением отчества
        private SqlCommand insertDataUsers;
        // команда вставки данных в таблицу Users без значения отчества
        private SqlCommand insertDataUsersWithoutMiddleName;
        // команда вставки данных в таблицу Resulttests с параметром "end"
        private SqlCommand insertDataResulttests;
        // команда вставки данных в таблицу Resulttests без параматера "end"
        private SqlCommand insertDataResulttestsWithoutEnd;
        // поле для получения колличества строк из таблицы 
        private SqlCommand GetCountStringTable;
        // поле для получения данных из столбца id
        private SqlCommand GetValuesId;
        //строка значения имени города 
        private string stringCityName;
        // Строка значения женской фамилии
        private string stringWomanLastName;
        // Строка значения женского имени
        private string stringWomanFirstName;
        // Строка значения женского отчества
        private string stringWomanMiddleName;
        // Строка значения мужской фамилии
        private string stringMenLastName;
        // Строка значения мужского имени
        private string stringMenFirstName;
        // Строка значения мужской отчества
        private string stringMenMiddleName;
        // строковое значение общей даты
        private string DateValueString;
        // индекс для элементов массива женских имен
        private int indexWomanFirstName;
        // индекс для элементов массива женских фамилий
        private int indexWomanLastName;
        // индекс для элементов массива женских отчеств
        private int indexWomanMiddleName;
        // индекс для элементов массива мужских имен
        private int indexMenFirstName;
        // индекс для элементов массива мужских фамилий
        private int indexMenLastName;
        // индекс для элементов массива мужских отчеств
        private int indexMenMiddleName;
        // индекс для элементов массива ArrayIdListCities[] 
        private int indexCityid;
        // индекс для элементов массива ArrayIdUsers[] 
        private int indexIdUsers;
        // случайное значение возраста 
        private int randAge;
        // случайное значение id города из массива ArrayIdListCities
        private int valueCityid;
        // случайное значение userid пользователя из массива ArrayIdUsers[]
        private int valueUserid;
        // значение id опроса для поля surveyId 
        private int valueSurveyId;
        // значение статуса 
        private int valueStatus;
        // случайный выбор значения заполнения имени пола т.е. мужчина или женщина
        private int categorySelection;
        // случайное значение заполнения или не заполнения отчества у женщин
        private int nullAndNotNullSelectionWoman;
        // случайное значение заполнения или не заполнения отчества у мужчин
        private int nullAndNotNullSelectionMen;
        // случайное значение заполнения или не заполнения столбца "end"
        private int nullOrNotNullSelection;
        // получение значений с выходных параметров метода
        private int setYear;
        private int setMonth;
        private int setMonthDay;
        private int setHour;
        private int setMinute;
        private int setSeconds;
        private int setMilliSeconds;

        // массив для заполнения значениями столбца id из таблицы ListCities
        public int[] ArrayIdListCities { get; private set; }
        // массив для заполнения значениями столбца userid из таблицы Users
        public int[] ArrayIdUsers { get; private set; }
        // массив мужских фамилий
        public string[] ArrayMenLastName { get; } = new string[] { "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров", "Соколов", "Михайлов", "Новиков", "Федоров", "Морозов", "Волков", "Алексеев", "Лебедев", "Семенов", "Егоров", "Павлов", "Козлов", "Степанов", "Николаев", "Орлов", "Андреев", "Макаров", "Никитин", "Захаров", "Зайцев", "Соловьев", "Борисов", "Яковлев", "Григорьев", "Далакян", "Верник", "Стриженов", "Колобков", "Солнцев" };
        // массив мужских имен 
        public string[] ArrayMenFirstName { get; } = new string[] { "Василий", "Роман", "Сергей", "Борис", "Алексей", "Александр", "Анатолий", "Арсений", "Богдан", "Валерий", "Константин", "Валентин", "Евгений", "Григорий", "Георгий", "Егор", "Иван", "Станислав", "Игорь", "Илья", "Кирилл", "Леонид", "Никита", "Николай", "Олег", "Павел", "Петр", "Степан", "Фёдор", "Ярослав", "Юрий", "Владимир", "Эдуард", "Виталий", "Семён" };
        // массив мужских отчеств 
        public string[] ArrayMenMiddleName { get; } = new string[] { "Иванович", "Александрович", "Алексеевич", "Васильевич", "Ярославович", "Егорович", "Витальевич", "Леонидович", "Константинович", "Эдуардович", "Степанович", "Сергеевич", "Семенович", "Павлович", "Всеволодович", "Игнатьевич", "Олегович", "Матвеевич", "Юрьевич", "Евгеньевич", "Феликсович", "Валентинович", "Денисович", "Данилович", "Андреевич", "Михайлович", "Игоревич", "Яковлевич", "Федорович", "Богданович", "Романович", "Николаевич", "Григорьевич", "Аркадьевич", "Владимирович" };
        // массив женских фамилий 
        public string[] ArrayWomanLastName { get; } = new string[] { "Афанасьева", "Маркина", "Соколова", "Петрова", "Демьяненко", "Усачева", "Аросьева", "Ковальчук", "Семенова", "Стриженова", "Василевская", "Лисицина", "Гусейнова", "Акулова", "Астахова", "Крылова", "Булгакова", "Бойцова", "Воробьева", "Гаврилова", "Грибоедова", "Ежёва", "Ивкина", "Смирнова", "Яровая", "Мельникова", "Овчинникова", "Петрыкина", "Орлова", "Юнусова", "Михальченко", "Лунная", "Климкина", "Кошелева", "Кудрина" };
        // массив женских имен 
        public string[] ArrayWomanFirstName { get; } = new string[] { "Алла", "Анастасия", "Анна", "Тамара", "Ольга", "Марина", "Роза", "Полина", "Анфиса", "Мария", "Ася", "Жанна", "Елена", "Виктория", "Алиса", "Наталья", "Снежана", "Ксения", "София", "Светлана", "Эльвира", "Вероника", "Кристина", "Валерия", "Варвара", "Ванесса", "Дана", "Дарья", "Екатерина", "Жасмин", "Зоя", "Зинаида", "Ирина", "Карина", "Любовь" };
        // массив женских отчеств 
        public string[] ArrayWomanMiddleName { get; } = new string[] { "Александровна", "Алексеевна", "Анатольевна", "Андреевна", "Анатольевна", "Богдановна", "Борисовна", "Валентиновна", "Васильевна", "Викторовна", "Васильевна", "Геннадиевна", "Григорьевна", "Данилова", "Дмитриевна", "Евгеньевна", "Ефимовна", "Ивановна", "Леонидовна", "Львовна", "Константиновна", "Максимовна", "Николаевна", "Олеговна", "Павловна", "Робертовна", "Романовна", "Семеновна", "Сергеевна", "Станиславовна", "Степановна", "Тарасовна", "Тимофеевна", "Федоровна", "Юрьевна" };
        // Массив названий городов 
        public string[] ArrayCityName { get; } = new string[] { "Брянск", "Бологое", "Белозерск", "Балашиха", "Анапа", "Анадырь", "Москва", "Омск", "Воронеж", "Санкт-Петербург", "Иркутск", "Волгоград", "Сочи", "Клин", "Курск", "Мурманск", "Нижний Новгород", "Новороссийск", "Новокузнецк", "Пермь", "Псков", "Самара", "Рязань", "Грозный", "Иваново", "Ярославль", "Можайск", "Смоленск", "Тверь", "Владивосток", "Калининград", "Севастополь", "Рыбинск", "Ростов-на-дону", "Пятигорск", "Казань", "Пенза", "Оренбург", "Норильск", "Новосибирск", "Мичуринск", "Махачкала", "Липецк", "Краснодар", "Ижевск" };


        // конструктор класса DataFactory
        public DataFactory(string connectionString)
        {
            connection = new SqlConnection(connectionString);

            // открытие соединения с базой данных
            OpenConnection();
        }

        // создание строк 
        public async void CreatingRows()
        {
            // выключение конопки
            ButtonMode(false);

            #region заполнение таблицы ListCities

            insertDataListCities = new SqlCommand("INSERT INTO [ListCities] (CityName) VALUES(@CityName)", connection);

            // колличество имен городов в массиве равно колличеству строк в таблице ListCities
            countLinesCities = ArrayCityName.Length;

            // получаем общее колличество всех заполняемых строк в таблицах. 
            totalNumberLines = countLinesCities + countLinesUsers + countLinesResulttests;


            // колличество строк в таблице ListCities равно countcityname
            for (int i = 0; i < countLinesCities; i++)
            {
                // проверка отменен ли процесс заполнения данных
                if (cancelled)
                {
                    break;
                }

                insertDataListCities.Parameters.Clear();

                stringCityName = ArrayCityName[i];

                insertDataListCities.Parameters.AddWithValue("CityName", stringCityName);

                await insertDataListCities.ExecuteNonQueryAsync();

                // счетчик итерации цикла с последующим выводом процентов заполненых строк
                numberFilledLines++;
                PercentageCreateLines(100 * numberFilledLines / totalNumberLines);
            }

            #endregion

            #region заполнение таблицы Users

            #region получение колличества строк из таблицы ListCities

            int numberLinesListCities = 0;

            GetCountStringTable = new SqlCommand("SELECT COUNT(*) FROM [ListCities]", connection);

            try
            {
                // результат запроса "SELECT COUNT(*) FROM [testdata]" заносим в переменную DataReader
                DataReader = await GetCountStringTable.ExecuteReaderAsync();

                while (await DataReader.ReadAsync())
                {
                    // в переменную numberLinesListCities помещаем общее количество строк таблицы ListCities
                    numberLinesListCities = Convert.ToInt32(DataReader[0]);
                    // инициализирцем массив целых чисел на величену "колличества строк в таблице ListCities"
                    ArrayIdListCities = new int[numberLinesListCities];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (DataReader != null)
                    DataReader.Close();
            }

            #endregion

            #region внесение данных из столбца id таблицы ListCities в массив ArrayIdListCities[] 

            GetValuesId = new SqlCommand("SELECT id FROM [ListCities]", connection);

            try
            {
                // результат запроса "SELECT id FROM [ListCities]" заносим в переменную DataReader
                DataReader = await GetValuesId.ExecuteReaderAsync();

                while (await DataReader.ReadAsync())
                {
                    // заносим значения столбца "id" в массив ArrayIdListCities[] 
                    ArrayIdListCities[Increment] = Convert.ToInt32(DataReader["id"]);

                    Increment = Increment + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (DataReader != null)
                    DataReader.Close();
            }

            #endregion

            // команда INSERT без параматера middlename 
            insertDataUsersWithoutMiddleName = new SqlCommand("INSERT INTO [Users] (Category, LastName, FirstName, Age, CityID, [RegDate]) VALUES(@category, @lastname, @firstname, @age, @cityid, @regdate)", connection);

            // команда INSERT с параметром  middlename 
            insertDataUsers = new SqlCommand("INSERT INTO [Users] (Category, LastName, FirstName, MiddleName, Age, CityID, [RegDate]) VALUES(@category, @lastname, @firstname, @middlename, @age, @cityid, @regdate)", connection);

            // кололичество итераций цикла равно колличенству строк в таблице Users 
            for (int i = 0; i < countLinesUsers; i++)
            {
                if (cancelled)
                {
                    break;
                }

                // удаляет параметры - category, lastname, firstname, middlename, age, city, regdate 
                insertDataUsers.Parameters.Clear();

                insertDataUsersWithoutMiddleName.Parameters.Clear();

                // общее колличество женщин от общей массы зарегестрированных будет приблизельно 20%. 
                // потому-что частота выпадения числа 3 из чисел (1, 2, 3, 4, 5) будет равна приблизительно 20%.
                categorySelection = rand.Next(3, 6);

                // если categorySelection равно 3 то заполняем таблицу Users женской категорией
                if (categorySelection == 3)
                {
                    #region заполнение таблицы Users женской категорией

                    // общее колличество не записаных отчеств у женщин будет приблизительно 33%. 
                    // Вероятность частоты выпадения числа 2 будет состовлять приблизительно 33%.
                    nullAndNotNullSelectionWoman = rand.Next(1, 4);

                    // если nullAndNotNullSelectionWoman равно 2 то столбец middlename не заполняется женским отчеством
                    if (nullAndNotNullSelectionWoman == 2)
                    {
                        #region заполнение таблицы Users женской категорией без заполнения столбца middlename 

                        #region заполнение стобца category значением 0

                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("category", 0);

                        #endregion

                        #region заполнение столбца lastname женской фамилией

                        // случайный выбор индекса элемента массива ArrayWomanSurname
                        indexWomanLastName = rand.Next(0, 35);

                        stringWomanLastName = ArrayWomanLastName[indexWomanLastName];

                        // Помещаем фамилию в параметр "lastname"                
                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("lastname", stringWomanLastName);

                        #endregion

                        #region заполнение столбца firstname женским именем

                        // случайный выбор индекса элемента массива ArrayWomanName
                        indexWomanFirstName = rand.Next(0, 35);

                        stringWomanFirstName = ArrayWomanFirstName[indexWomanFirstName];

                        // Помещаем фамилию в параметр "firstname"                
                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("firstname", stringWomanFirstName);

                        #endregion

                        #region заполнение столбца age

                        randAge = rand.Next(17, 56);

                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("age", randAge);

                        #endregion

                        #region заполнение столбца cityid

                        // свойство Increment содержит длинну массива ArrayId
                        indexCityid = rand.Next(0, Increment);

                        valueCityid = ArrayIdListCities[indexCityid];

                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("cityid", valueCityid);

                        #endregion

                        #region заполнение столбца regdate

                        DateValueString = creatdatetime.GetRandomDateTime(2014, 2016);

                        // Помещаем значение даты в параметр "regdate"               
                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("regdate", DateValueString);

                        #endregion

                        // выполнение команды INSERT с полученными значениями в параметрах
                        await insertDataUsersWithoutMiddleName.ExecuteNonQueryAsync();

                        #endregion
                    }
                    else
                    {
                        #region заполнение таблицы Users женской категорией с заполнением столбца middlename

                        #region заполнение стобца category значением 0

                        insertDataUsers.Parameters.AddWithValue("category", 0);

                        #endregion

                        #region заполнение столбца lastname женской фамилией

                        // случайный выбор индекса элемента массива ArrayWomanSurname
                        indexWomanLastName = rand.Next(0, 35);

                        stringWomanLastName = ArrayWomanLastName[indexWomanLastName];

                        // Помещаем фамилию в параметр "surname"               
                        insertDataUsers.Parameters.AddWithValue("lastname", stringWomanLastName);

                        #endregion

                        #region заполнение столбца firstname женским именем

                        // случайный выбор индекса элемента массива ArrayWomanName
                        indexWomanFirstName = rand.Next(0, 35);

                        stringWomanFirstName = ArrayWomanFirstName[indexWomanFirstName];

                        // Помещаем фамилию в параметр "name"               
                        insertDataUsers.Parameters.AddWithValue("firstname", stringWomanFirstName);

                        #endregion

                        #region заполнение столбца middlename женским отчеством

                        indexWomanMiddleName = rand.Next(0, 35);

                        stringWomanMiddleName = ArrayWomanMiddleName[indexWomanMiddleName];

                        insertDataUsers.Parameters.AddWithValue("middlename", stringWomanMiddleName);

                        #endregion

                        #region заполнение столбца age

                        randAge = rand.Next(17, 56);

                        insertDataUsers.Parameters.AddWithValue("age", randAge);

                        #endregion

                        #region заполнение столбца cityid

                        // свойство Increment содержит длинну массива ArrayIdListCities 
                        indexCityid = rand.Next(0, Increment);

                        valueCityid = ArrayIdListCities[indexCityid];

                        insertDataUsers.Parameters.AddWithValue("cityid", valueCityid);

                        #endregion

                        #region заполнение столбца regdate

                        DateValueString = creatdatetime.GetRandomDateTime(2014, 2016);

                        // Помещаем значение даты в параметр "regdate"               
                        insertDataUsers.Parameters.AddWithValue("regdate", DateValueString);

                        #endregion

                        // выполнение команды INSERT с полученными значениями в параметрах
                        await insertDataUsers.ExecuteNonQueryAsync();

                        #endregion
                    }

                    #endregion                   
                }
                else
                {
                    #region заполнение таблицы Users мужской категорией

                    // общее колличество не записаных отчеств у мужчин будет приблизительно 14% 
                    nullAndNotNullSelectionMen = rand.Next(1, 8);

                    if (nullAndNotNullSelectionWoman == 4)
                    {
                        #region заполнение таблицы Users мужской категорией без заполнения столбца middlename 

                        #region заполнение стобца category значенитем 1

                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("category", 1);

                        #endregion

                        #region заполнение столбца lastname мужской фамилией

                        indexMenLastName = rand.Next(0, 35);

                        stringMenLastName = ArrayMenLastName[indexMenLastName];

                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("lastname", stringMenLastName);

                        #endregion

                        #region заполнение столбца firstname мужским именем

                        indexMenFirstName = rand.Next(0, 35);

                        stringMenFirstName = ArrayMenFirstName[indexMenFirstName];

                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("firstname", stringMenFirstName);

                        #endregion

                        #region заполнение столбца age

                        randAge = rand.Next(17, 56);

                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("age", randAge);

                        #endregion

                        #region заполнение столбца cityid

                        // свойство Increment содержит длинну массива ArrayId
                        indexCityid = rand.Next(0, Increment);

                        valueCityid = ArrayIdListCities[indexCityid];

                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("cityid", valueCityid);

                        #endregion

                        #region заполнение столбца regdate

                        DateValueString = creatdatetime.GetRandomDateTime(2014, 2016);

                        // Помещаем значение даты в параметр "regdate"               
                        insertDataUsersWithoutMiddleName.Parameters.AddWithValue("regdate", DateValueString);

                        #endregion

                        // выполнение команды INSERT с полученными значениями в параметрах
                        await insertDataUsersWithoutMiddleName.ExecuteNonQueryAsync();

                        #endregion
                    }
                    else
                    {
                        #region заполнение таблицы Users мужской категорией с заполнением столбца middlename

                        #region заполнение стобца category значенитем 1

                        insertDataUsers.Parameters.AddWithValue("category", 1);

                        #endregion

                        #region заполнение столбца lastname мужской фамилией

                        indexMenLastName = rand.Next(0, 35);

                        stringMenLastName = ArrayMenLastName[indexMenLastName];

                        insertDataUsers.Parameters.AddWithValue("lastname", stringMenLastName);

                        #endregion

                        #region заполнение столбца firstname мужским именем

                        indexMenFirstName = rand.Next(0, 35);

                        stringMenFirstName = ArrayMenFirstName[indexMenFirstName];

                        insertDataUsers.Parameters.AddWithValue("firstname", stringMenFirstName);

                        #endregion

                        #region заполнение столбца middlename мужским отчеством

                        indexMenMiddleName = rand.Next(0, 35);

                        stringMenMiddleName = ArrayMenMiddleName[indexMenMiddleName];

                        insertDataUsers.Parameters.AddWithValue("middlename", stringMenMiddleName);

                        #endregion

                        #region заполнение столбца age

                        randAge = rand.Next(17, 56);

                        insertDataUsers.Parameters.AddWithValue("age", randAge);

                        #endregion

                        #region заполнение столбца cityid

                        // свойство Increment содержит длинну массива ArrayId
                        indexCityid = rand.Next(0, Increment);

                        valueCityid = ArrayIdListCities[indexCityid];

                        insertDataUsers.Parameters.AddWithValue("cityid", valueCityid);

                        #endregion

                        #region заполнение столбца regdate

                        DateValueString = creatdatetime.GetRandomDateTime(2014, 2016);

                        // Помещаем значение даты в параметр "regdate"               
                        insertDataUsers.Parameters.AddWithValue("regdate", DateValueString);

                        #endregion

                        // выполнение команды INSERT с полученными значениями в параметрах
                        await insertDataUsers.ExecuteNonQueryAsync();

                        #endregion
                    }

                    #endregion              
                }

                numberFilledLines++;
                PercentageCreateLines(100 * numberFilledLines / totalNumberLines);
            }

            #endregion

            #region заполнение таблицы Resulttests

            #region получение колличества строк из таблицы Users 

            int numberLinesUsers = 0;

            GetCountStringTable = new SqlCommand("SELECT COUNT(*) FROM [Users]", connection);

            try
            {
                // результат запроса "SELECT COUNT(*) FROM [Users]" заносим в переменную DataReader 
                DataReader = await GetCountStringTable.ExecuteReaderAsync();

                while (await DataReader.ReadAsync())
                {
                    // в переменную numberLinesUsers помещаем общее количество строк таблицы Users
                    numberLinesUsers = Convert.ToInt32(DataReader[0]);
                    // инициализирцем массив целых чисел на величену "колличества строк в таблице Users" 
                    ArrayIdUsers = new int[numberLinesUsers];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (DataReader != null)
                    DataReader.Close();
            }

            #endregion

            #region внесение данных из столбца userid таблицы Users в массив ArrayIdUsers[] 

            GetValuesId = new SqlCommand("SELECT userid FROM [Users]", connection);

            // обнуляем счетчик
            Increment = 0;

            try
            {
                // результат запроса "SELECT userid FROM [Users]" заносим в переменную DataReader 
                DataReader = await GetValuesId.ExecuteReaderAsync();

                while (await DataReader.ReadAsync())
                {
                    // заносим значения столбца "userid" в массив ArrayIdUsers[]  
                    ArrayIdUsers[Increment] = Convert.ToInt32(DataReader["userid"]);

                    Increment = Increment + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (DataReader != null)
                    DataReader.Close();
            }

            #endregion

            // команда INSERT без параматера "End" 
            insertDataResulttestsWithoutEnd = new SqlCommand("INSERT INTO [Resulttests] (PolledID, SurveyID, Start, Status) VALUES(@polledId, @surveyId, @start, @status)", connection);

            // команда INSERT включая параметр "End" 
            insertDataResulttests = new SqlCommand("INSERT INTO [Resulttests] (PolledID, SurveyID, Start, [End], Status) VALUES(@polledId, @surveyId, @start, @end, @status)", connection);

            // кололичество итераций цикла равно колличенству строк в таблице Resulttests
            for (int i = 0; i < countLinesResulttests; i++)
            {
                if (cancelled)
                {
                    break;
                }

                insertDataResulttestsWithoutEnd.Parameters.Clear();

                insertDataResulttests.Parameters.Clear();

                // Колличество не пройденых до конца опросов будет состовлять приблизительно 20% от общей массы опросов.
                nullOrNotNullSelection = rand.Next(1, 6);

                // если true то поле "end" в строке не получает значение
                if (nullOrNotNullSelection == 3)
                {
                    #region заполнение столбца polledId

                    // свойство Increment содержит длинну массива ArrayIdUsers[] 
                    indexIdUsers = rand.Next(0, Increment);

                    valueUserid = ArrayIdUsers[indexIdUsers];

                    insertDataResulttestsWithoutEnd.Parameters.AddWithValue("polledId", valueUserid);

                    #endregion

                    #region заполнение столбца surveyId

                    valueSurveyId = rand.Next(1, 1151);

                    insertDataResulttestsWithoutEnd.Parameters.AddWithValue("surveyId", valueSurveyId);

                    #endregion

                    #region заполнение столбца start

                    DateValueString = creatdatetime.GetRandomDateTime(2017, 2018);

                    insertDataResulttestsWithoutEnd.Parameters.AddWithValue("start", DateValueString);

                    #endregion

                    #region заполнение столбца status

                    insertDataResulttestsWithoutEnd.Parameters.AddWithValue("status", 0);

                    #endregion

                    // выполнение команды INSERT с полученными значениями в параметрах
                    await insertDataResulttestsWithoutEnd.ExecuteNonQueryAsync();
                }
                else
                {
                    #region заполнение столбца polledId

                    indexIdUsers = rand.Next(0, Increment);

                    valueUserid = ArrayIdUsers[indexIdUsers];

                    insertDataResulttests.Parameters.AddWithValue("polledId", valueUserid);

                    #endregion

                    #region заполнение столбца surveyId

                    valueSurveyId = rand.Next(1, 1151);

                    insertDataResulttests.Parameters.AddWithValue("surveyId", valueSurveyId);

                    #endregion

                    #region заполнение столбца start

                    DateValueString = creatdatetime.GetRandomDateTime(2017, 2018, out setYear, out setMonth, out setMonthDay, out setHour, out setMinute, out setSeconds, out setMilliSeconds);

                    insertDataResulttests.Parameters.AddWithValue("start", DateValueString);

                    #endregion

                    #region заполнение столбца end

                    DateValueString = creatdatetime.GetLargerDateTime(setYear, setMonth, setMonthDay, setHour, setMinute, setSeconds, setMilliSeconds);

                    insertDataResulttests.Parameters.AddWithValue("end", DateValueString);

                    #endregion

                    #region заполнение столбца status

                    valueStatus = rand.Next(1, 21);

                    insertDataResulttests.Parameters.AddWithValue("status", valueStatus);

                    #endregion

                    // выполнение команды INSERT с полученными значениями в параметрах
                    await insertDataResulttests.ExecuteNonQueryAsync();
                }

                numberFilledLines++;
                PercentageCreateLines(100 * numberFilledLines / totalNumberLines);
            }

            #endregion


            if (cancelled != true)
            {
                MessageBox.Show("Заполнение данными завершенно");           
            }
            else
            {
                MessageBox.Show("Заполнение данных остановлено!");             
            }

            // Закрытие соединения с базой данных 
            CloseConnection();
        }

        // вызов метода устанавливает флаг отмены процесса в true, т.е. отменить процесс
        public void Cancel()
        {
            cancelled = true;
        }
        // Открыть соединение с базой данных
        private async void OpenConnection()
        {
            try
            {
                await connection.OpenAsync();

                MessageBox.Show($"Соединение с базой данных {connection.DataSource} открыто");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        // Закрытие соединения с базой данных
        public void CloseConnection()
        {
            try
            {
                if (cancelled != true)
                {
                    Cancel();
                }

                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }

                MessageBox.Show($"Соединение с базой данных {connection.DataSource} закрыто");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // показывает процент созданных строк 
        public event Action<float> PercentageCreateLines;
        // Включение и выключение режима кнопки
        public event Action<bool> ButtonMode;
    }
}
