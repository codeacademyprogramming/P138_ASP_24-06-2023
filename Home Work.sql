Create Database Academy

Use Academy

Create Table Students
(
	Id int,
	Name nvarchar(100),
	SurName nvarchar(100),
	Email nvarchar(100)
)

Insert Into Students(Id, Name, SurName, Email)
Values
(1,'Vusal','Dadashov','vusalasd@mail.ru'),
(2,'Vusal','Dadashov','vusal@gmail.com'),
(3,'Vusal','Dadashov','vusaasasda@code.edu.az'),
(4,'Vusal','Dadashov','vusal@yahoo.com'),
(5,'Vusal','Dadashov','vusaasdasdl@outlock.com'),
(6,'Vusal','Dadashov','vuasdsal@box.ru')

Select LEN(SUBSTRING(Email,CHARINDEX('@',Email)+1,LEN(Email))) From Students

--Company database-i yaradin(istənilən ad vəre bilərsiz db-ye).
--Employees table-i olsun. Employees  -in Name, SurName, Position, Salary Column-lari olsun. 
Create Database Company

Use Company

Create Table Employees
(
	Id int,
	Name nvarchar(100),
	SurName nvarchar(100),
	Position nvarchar(100),
	Salary money
)

--Aşağıdakı query-ləri yazmalısınız:

--1.Ortalama maaşı çıxarmalısınız
Select AVG(Salary) From Employees
--2.Ortalama maaşdan yuxarı maaş alan işçilərin ad soyadını və maaşını yazdırmalısız
Select * From Employees Where Salary > (Select AVG(Salary) From Employees)
--3.Max, Min maaşları çıxarmalı
Select MIN(Salary), MAX(Salary) From Employees


--Market adli Database yaradin
Create Database Market

Use Market
--Icinde Products Table-i yaradin.Product table-inda Id,Name,Price columnlari olsun
Create Table Products
(
	Id int,
	Name nvarchar(100),
	Price money
)
--Products table-na yeni bir Brand columnu elave edin
Alter Table Products
Add Brand nvarchar(100)

--Products table-a value-lar insert edin (10-15 dene product datasi kifayetdir)
 
--Qiymeti Productlarin price-larinin average-den kicik olan Products datalarinin siyahisini getiren query yazin
Select * From Products Where Price < (Select AVG(Price) From Products)
--Qiymeti 10-dan yuxari olan Product datalarinin siyahisini getiren query yazin
Select * From Products Where Price > 10
--Brand uzunlugu 5-den boyuk olan Productlarin siyahisini getiren query.Gelen datalarda Mehsulun adi ve 
--Brand adi 1 columnda gorsensin ve Column adi ProductInfo olsun.
Select (Name+' '+Brand) [Product Info] From Products Where LEN(Brand) > 5