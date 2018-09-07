# TestData
Заполнение базы данных тестовыми данными

//////////////////// DDL код заполняемой тестовыми данными базы данных /////////////////////

USE DbInterview

GO

-- таблица списка городов 

CREATE TABLE ListCities(

[ID]   INT IDENTITY(1,1) NOT NULL,      -- ID города 

[CityName] NVARCHAR(30)  NOT NULL,      -- название города

CONSTRAINT DbInterview_ListCities_CityID_PK PRIMARY KEY CLUSTERED (ID)

)

GO

-- таблица данных о пользователе

CREATE TABLE Users(

[UserID]      INT IDENTITY(1,1) NOT NULL,  -- ID пользователя

[Category]    BIT               NOT NULL,  -- значение 1 означает мужчина, а значение 0 означает женщина

[LastName]    NVARCHAR(30)      NOT NULL,  -- фамилия 

[FirstName]   NVARCHAR(30)      NOT NULL,  -- имя 

[MiddleName]  NVARCHAR(30)          NULL,  -- отчество (на сайтах отчество писать не всегда обязательно, поэтому разрешается NULL)

[Age]         SMALLINT          NOT NULL,  -- возраст пользователя

[CityID]      INT               NOT NULL,  -- ID города где живет пользователь

[RegDate]     DATETIME          NOT NULL   -- дата регистрации пользователя  

CONSTRAINT DbInterview_Users_RegDate_DEFAULT DEFAULT SYSDATETIME(), 

CONSTRAINT DbInterview_Users_UserID_PK PRIMARY KEY CLUSTERED (UserID)

)

GO

ALTER TABLE Users 

WITH CHECK ADD CONSTRAINT DbInterview_Users_CityID_FK FOREIGN KEY (CityID)

REFERENCES ListCities(ID) 

ON UPDATE CASCADE

ON DELETE CASCADE

GO


-- таблица результов тестов(опросов) пользователей сайта

CREATE TABLE Resulttests( 

[PolledID]  INT      NOT NULL,  -- ID пользователя из таблицы Users

[SurveyID]  INT      NOT NULL,  -- ID опроса (при заполнении данных опросов будет 1150) 

[Start]     DATETIME NOT NULL,  -- время начала опроса 

[End]       DATETIME NULL,      -- время окончания опроса (если NULL то опрос не пройден до конца).

                                -- Время окончания опроса доложно быть больше или равно времени начала опроса.

[Status]    SMALLINT NOT NULL   -- статус опроса (если равно 0 то опрос не пройден до конца) статусов будет от 0 до 20-ти

)

GO 

