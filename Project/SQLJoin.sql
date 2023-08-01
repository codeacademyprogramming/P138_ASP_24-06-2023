Create Database Shams

Use Shams

--Employees (Id,FullName,Salary,DepartmentId, Email) ve Departments(Id, Name) table-lariniz olsun ve aralarinda one to many bir relation qurun.
Create Table Departments
(
Id int identity primary key,
Name nvarchar(30) NOT NULL Check(LEN(Name)>2)
)

Create Table Employees
(
Id int identity primary key,
FullName nvarchar(100) NOT NULL Check(LEN(FullName)>3),
Salary money Check(Salary>0),
Email nvarchar(100) NOT NULL Unique,
DepartmentId int Foreign Key References Departments(Id)
)

 -- Employee Salary- 0 -dan kicik ola bilmez
 -- Employee FullName - NULL OLA BILMEZ ,  uzunlugu 3-den boyuk olmaldir
 -- Department Name - uzunlugu 2-den boyuk olamlidir, null ola bilmez
 -- Email Null Ola Bilmez Ve Tekrar Olunmasin

 Insert Into Departments
 Values
 ('HR-C'),
 ('Maliyye'),
 ('Muhasibatliq'),
 ('IT-C'),
 ('Marketing')

 Insert Into Employees(FullName,Salary, Email, DepartmentId)
 Values
 ('Vusal Dadashov',5000,'vusal@code.edu.az',6),
 ('Kenan Hesenov',5500,'Kenan@code.edu.az',6),
 ('Shems Shirali',50,'shams@code.edu.az',2),
 ('Hesen Huseynli',45000,'hesen@mail.ru',2),
 ('Nazim Ibrahimov',3000,'namzim@gmail.com',2),
 ('Fayaz Ibrahimov',5500,'fayaz@inbox.ru',4),
 ('Nezrin Agayeva',5000,'nezrin@yahoo.com',4),
 ('Ali Qafarov',50000,'ali@yahoo.com',5),
 ('Rakif Sherifov',35000,'rakif@yahoo.com',6),
('Murad Mustafayev',35000,'Murad@yahoo.com',null),
('Hamid Mammadov',35000,'Hamid@yahoo.com',null)

Select * From Employees
Inner Join Departments
On Employees.DepartmentId = Departments.Id

Select * From Employees
Join Departments
On Employees.DepartmentId = Departments.Id

Select Employees.Id, FullName,Name,Salary From Departments
Inner Join Employees
On Employees.DepartmentId = Departments.Id


Select * From Departments Left Join Employees On Employees.DepartmentId = Departments.Id
Select * From Employees Left Outer Join Departments On Employees.DepartmentId = Departments.Id

Select *From Departments Right Outer Join Employees On Employees.DepartmentId = Departments.Id
Select *From Departments Right Join Employees On Employees.DepartmentId = Departments.Id

Select *From Departments Full Outer Join Employees On Employees.DepartmentId = Departments.Id
Select Employees.Id, ISNULL(Employees.FullName,'Data Yoxdur') From Departments Full Join Employees On Employees.DepartmentId = Departments.Id

Create Table Positions
(
	Id int identity primary key,
	Name nvarchar(30)
)

Alter Table Employees
Add PositionId int Constraint FK_Emp_Pos_PositionId Foreign Key references Positions(Id)

Insert Into Positions
Values
('CEO'),
('CTO'),
('Mudur'),
('Mudur Muavini'),
('Mudur Muavinin Muavid')

Select FullName,d.Name,p.Name From Employees e
Join Departments d
On e.DepartmentId = d.Id
Join Positions p
On e.PositionId = p.Id

Create Table SalaryCategories
(
	Id int identity primary key,
	Name nvarchar(30),
	Min int,
	Max int
)

Insert Into SalaryCategories(Name,Min,Max)
Values
('Aclar',0,3000),
('normal aclar',3001, 5000),
('normal',5001,30000),
('SELLENENLER',30001,100000)

--Non Equal Join
Select FullName,Name From Employees
Join SalaryCategories
On Employees.Salary Between SalaryCategories.Min And SalaryCategories.Max

Create Table Categories
(
	Id int identity primary key,
	Name nvarchar(30),
	ParentId int Foreign Key References Categories(Id)
)

Insert Into Categories(Name,ParentId)
Values
('Un Memulat',null),
('Corek',1),
('Lavas',1),
('Pehriz',2),
('Su',null),
('Qzli',5),
('Qzsiz',5),
('Portagal Siresi',5)

Select c.Name, ISNULL(p.Name,'-') 'Parent' From Categories c
Left Join Categories p
On c.ParentId = p.Id

Create Table Products
(
	Id int identity primary key,
	Name nvarchar(30)
)

Insert Into Products
Values
('Jeans'),
('T-Shirt'),
('Jacket')

Insert Into Sizes
Values
('XL'),
('L'),
('XXL')

Create Table Sizes
(
	Id int identity primary key,
	Name nvarchar(30)
)

Select * From Products
cross Join Sizes


--View
--Create
Create View usv_GetCategories
As
Select c.Name, ISNULL(p.Name,'-') 'Parent' From Categories c
Left Join Categories p
On c.ParentId = p.Id

--Edit
Alter View usv_GetCategories
As
Select c.Id, c.Name 'Ad', ISNULL(p.Name,'-') 'Parent' From Categories c
Left Join Categories p
On c.ParentId = p.Id
where c.Name Like '%a%'

Create View usv_GetCategoryFilter
As
Select *From usv_GetCategories where Name Like '%a%'

Select Ad From usv_GetCategories


--Procedure
--Create
Create Procedure usp_GetEmployessBySalary
@salary money
As
Begin
	Select * From Employees where Salary < @salary
End

--Edit
Alter Procedure usp_GetEmployessBySalary
@salary money
As
Begin
	Select FullName,Salary From Employees where Salary < @salary
End

Alter Procedure usp_GetEmployessBySalary
@salary money, @search nvarchar(1000)
As
Begin
	Select FullName,Salary From Employees where Salary < @salary And FullName Like '%'+@search+'%'
End

exec usp_GetEmployessBySalary 10000,'as'


--Function
Create Function usf_GetEmployessBySalaryCount
(@salary money)
returns int
As
Begin
	declare @count int

	Select @count= Count(*) From Employees where Salary > @salary

	return @count
End

Alter Function usf_GetEmployessBySalaryCount
(@salary money)
returns int
As
Begin
	declare @count int

	Select @count= Count(*) From Employees where Salary > @salary

	return @count
End

select dbo.usf_GetEmployessBySalaryCount(10000)

Create Table EmployeeArchives
(
	Id int ,
FullName nvarchar(100) ,
Salary money ,
Email nvarchar(100) ,
DepartmentId int ,
 PositionId int,
 Date datetime2,
 Type nvarchar(100)
)

Create trigger EmployeeUpdateTrigger
on Employees
after update
as
Begin

	declare @id int
	declare @fullName nvarchar(100)
	declare @salary money
	declare @depId int
	declare @posId int
	declare @email nvarchar(100)


	Select @id = emp.Id From inserted emp
	Select @fullName = emp.FullName From inserted emp
	Select @salary = emp.Salary From inserted emp
	Select @depId = emp.DepartmentId From inserted emp
	Select @posId = emp.PositionId From inserted emp
	Select @email = emp.Email From inserted emp


	Insert Into EmployeeArchives(Id, FullName,Salary,Email,DepartmentId,PositionId,Date,Type)
	Values
	(@id,@fullName,@salary,@email,@depId,@posId,GETDATE(),'Update')
End