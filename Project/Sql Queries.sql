--DDL Queries(Data Definition Language)
--Create A Database
Create Database P138FirstSqlQuery

--Use A Created Database
Use P138FirstSqlQuery

--Create A Table
Create Table Groups(Id int,Name nvarchar(10))

--Delete A Table
Drop Table Groups

--Edit Table Add a new Column
Alter Table Groups Add Count TinyInt

--Edit Table Delete a column
Alter Table Groups Drop Column Count

--Edit Table Edit a Column
Alter Table Groups Alter Column Name nvarchar(100)

--DML Queries (Data Manipulation Language)
Insert into Groups Values(1,'P138',16)

Insert into Groups(Name,Count,Id) Values('P133',15,2)

Insert into Groups(Id,Count,Name) Values(3,17,'P229'),(4,15,'P217'),(5,12,'P235')

Update Groups Set Count = 16 Where Id = 2 Or Id=4

Update Groups Set Name='P777' Where Id = 2

Update Groups Set Name='P999',Count=99 Where Id = 6

Delete Groups Where Id = 4 Or Id = 3

Select Name,Count from Groups

Select Count,Name from Groups

Select * From Groups

Create Table Students
(
	Id int,
	Name nvarchar(100),
	SurName nvarchar(100),
	Age TinyInt,
	Grade TinyInt
)

Insert Into Students(Id,Age,Grade,Name,SurName)
Values
(1,19,90,N'Vüsal',N'Dadaşov'),
(2,19,76,N'Shəms',N'Şirəli'),
(3,20,78,'Kenan','Hesenov'),
(4,17,65,'Nazim','Ibrahimov'),
(5,21,65,'Hasan','Huseynli'),
(6,25,70,'Fayaz','Ibrahimov'),
(7,21,10,'Ali','Qafarov')

Select * From Students

Select Name, SurName, Grade from Students

Select * From Students Where Grade > 70

Select * From Students Where Age <= 25 And Age >= 17

Select * From Students Where Age > 25 And Age < 17

Select * From Students where Age Between 17 And 25

Select * From Students Where Age = 17 Or Age = 19 Or Age = 20

Select * From Students Where Age In(17,19,20)

Select Name as [Ad],SurName as [Soyad] from Students

Select (Name+' '+SurName) as [Ad SoyAd], Age as [Yas], Grade as [Qiymet] From Students

Select Name 'Ad' From Students

Select Name as 'Ad' From Students

Select Name [Ad] From Students

Select MAX(Age), MAX(Grade) From Students

Select Grade From Students Where Age = (Select MAX(Age) from Students)

Select Min(Age) From Students 

Select Avg(Age) From Students

Select LEN(Name) from Students

Select MAX(LEN(Name)) from Students

Select SUBSTRING(Name,1,3) From Students

Select REPLACE(Name,'a','Code') From Students

Select CHARINDEX('a',Name) as test From Students

Select COUNT(*) From Students

Select * From Students Where LEN(Name) > 4

Alter table Students add email nvarchar(100)