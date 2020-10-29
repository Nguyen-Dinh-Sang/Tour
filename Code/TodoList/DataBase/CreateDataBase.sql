USE master
GO

CREATE DATABASE TodoListDB
GO

USE TodoListDB
GO

--Quyền
CREATE TABLE Role(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Role NVARCHAR(50),
	DateCreated DATE DEFAULT GETDATE()
)
GO

--Nhân viên
CREATE TABLE Employee(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Email NVARCHAR(100),
	Password VARCHAR(100),
	FullName NVARCHAR(100),
	PhoneNumber VARCHAR(10),
	IdRole INT DEFAULT 1,
	DateCreated DATE DEFAULT GETDATE(),
	CONSTRAINT User_Role
	FOREIGN KEY(IdRole)
	REFERENCES dbo.Role(Id)
	ON DELETE CASCADE
)
GO

--Trạng thái công việc
CREATE TABLE WorkStatus(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	WorkStatus NVARCHAR(50),
	DateCreated DATE DEFAULT GETDATE()
)
GO

--Trạng thái danh sách công việc
CREATE TABLE WorkListStatus(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	WorkListStatus VARCHAR(100),
	DateCreated DATE DEFAULT GETDATE()
)
GO

--Danh sách công việc
CREATE TABLE WorkList(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	WorkListName NVARCHAR(100),
	IdWorkListStatus INT DEFAULT 1,
	DateCreated DATE DEFAULT GETDATE(),
	CONSTRAINT WorkList_WorkListStatus
	FOREIGN KEY(IdWorkListStatus)
	REFERENCES dbo.WorkListStatus(Id)
	ON DELETE CASCADE
)
GO

--Danh sách công việc và nhân viên
CREATE TABLE WorkList_Employee(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdEmployee INT,
	IdWorkList INT,
	DateCreated DATE DEFAULT GETDATE(),
	CONSTRAINT WorkListEmployee_Employee
	FOREIGN KEY(IdEmployee)
	REFERENCES dbo.Employee(Id)
	ON DELETE CASCADE,
	CONSTRAINT WorkListEmployee_WorkList
	FOREIGN KEY(IdWorkList)
	REFERENCES dbo.WorkList(Id)
	ON DELETE CASCADE
)
GO

--Công việc
CREATE TABLE Work(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdWorkList INT,
	IdWorkStatus INT DEFAULT 1,
	WorkContent NVARCHAR(500),
	WorkName NVARCHAR(200),
	DateCreated DATE DEFAULT GETDATE(),
	StartDate DATE DEFAULT GETDATE(),
	EndDate DATE DEFAULT GETDATE(),
	CONSTRAINT Work_WorkList
	FOREIGN KEY(IdWorkList)
	REFERENCES dbo.WorkList(Id)
	ON DELETE CASCADE,
	CONSTRAINT Work_WorkStatus
	FOREIGN KEY(IdWorkStatus)
	REFERENCES dbo.WorkStatus
	ON DELETE CASCADE
)
GO

--Công việc và nhân viên
CREATE TABLE Work_Employee(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdEmployee INT,
	IdWork INT,
	DateCreated DATE DEFAULT GETDATE(),
	CONSTRAINT WorkEmployee_Employee
	FOREIGN KEY(IdEmployee)
	REFERENCES dbo.Employee(Id)
	ON DELETE CASCADE,
	CONSTRAINT WorkEmployee_Work
	FOREIGN KEY(IdWork)
	REFERENCES dbo.Work(Id)
	ON DELETE CASCADE
)
GO

--Bình luận
CREATE TABLE Comment(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdEmployee INT,
	IdWork INT,
	CommentContent NVARCHAR(200),
	DateCreated DATE DEFAULT GETDATE(),
	CONSTRAINT Comment_Employee
	FOREIGN KEY(IdEmployee)
	REFERENCES dbo.Employee(Id)
	ON DELETE CASCADE,
	CONSTRAINT Comment_Work
	FOREIGN KEY(IdWork)
	REFERENCES dbo.Work(Id)
	ON DELETE CASCADE
)
GO

--Kiểm tra
SELECT *
FROM Work

SELECT *
FROM Role

SELECT *
FROM Employee

SELECT *
FROM WorkStatus

SELECT *
FROM WorkListStatus

SELECT *
FROM WorkList

SELECT *
FROM Comment

SELECT *
FROM WorkList_Employee

SELECT *
FROM Work_Employee
GO
