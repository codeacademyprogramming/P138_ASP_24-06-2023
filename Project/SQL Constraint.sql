Create Database P138Constraint

Use P138Constraint

Create Table Students
(
	Id int identity,
	Name nvarchar(100)  Constraint CK_Students_Name_Len Check(Len(Name) >= 3),
	SurName nvarchar(100) Check(Charindex('a',SurName)=0),
	Age tinyint Check(Age Between 13 And 50),
	Email nvarchar(100) Not Null Unique,
	GroupId int 
)

Create Table Groups
(
	Id int identity primary key,
	Name nvarchar(100) Unique Not NUll Check(Len(Name) >= 4)
)

Create Table Employees
(
	Id int identity primary key,
	Name nvarchar(100)  ,
	SurName nvarchar(100) ,
	Age tinyint,
)

Alter Table Students
Add Constraint PK_Students_Id primary key(Id)

Alter Table Students
Drop Constraint PK_Students_Id

Alter Table Students
add Constraint FK_Students_Groups_Id Foreign Key(GroupId) References Groups(Id)

Drop Table Students

Truncate Table Students

Select CHARINDEX('z',Name) From Students

Insert Into Employees(Name, SurName, Age)
Values
('Hamid','Memmedov',33),
('Hamid-e','Ehmedov',33),
('Vusal-e','Dedeshov',33),
('Shams-e','Shireli',33),
('Vusal-e','Memmedov',33)

Select Count(distinct Name) From Students

Select Name, SurName From Students
Union
Select Name, SurName From Employees

Select Name, SurName From Students
Union all
Select Name, SurName From Employees

Select Count(*) From 
(
Select Name, SurName From Students
Union
Select Name, SurName From Employees
) ut

Select sdad.Name From  
(
Select Name, SurName From Students
Union
Select Name, SurName From Employees
) sdad

Select * From Students
Order By Name Asc

Select * From Students
Order By Name Desc

Select Name, Count(*) From Students
Group By Name
Having Count(Name) >= 2

Select sdad.Name, Count(sdad.Name) From  
(
Select Name, SurName From Students
Union
Select Name, SurName From Employees
) sdad
Group By sdad.Name
--Where Len(sdad.Name) > 5