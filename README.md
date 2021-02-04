# CRUDAppJS
ASP.NET MVC &amp; AngulaJS CRUD Application (Database First)

## Use Technology:
1.	Front End: Angular Js, Bootstrap 
2.	Backend: ASP.Net MVC, Entity Framework, LINQ
3.	Design Pattern:  Database First
4.	Database: SQL SERVER 2017

## Sql Script

```
CREATE DATABASE EmployeeDB
GO
USE EmployeeDB
GO
CREATE SCHEMA Employee
GO
CREATE TABLE [Employee].[EmpBasicInfo](
	[EmpID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [VARCHAR](250) NOT NULL,
	[Email] [VARCHAR](250) NOT NULL,
	[Address] [VARCHAR](250)NOT NULL,
	[Phone] [VARCHAR](30) NOT NULL
 CONSTRAINT [PK_Employee.Employees] PRIMARY KEY CLUSTERED ([EmpID] ASC)
)
```
