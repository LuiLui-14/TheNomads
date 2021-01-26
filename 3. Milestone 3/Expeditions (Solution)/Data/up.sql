-- UP script for SQL Server

CREATE TABLE [Peak] (
  [ID]              INT PRIMARY KEY IDENTITY(1, 1),
  [Name]            NVARCHAR(30) NOT NULL,
  [Height]          INT NOT NULL,
  [ClimbingStatus]  BIT NOT NULL,
  [FirstAscentYear] INT
)
GO

CREATE TABLE [Expedition] (
  [ID]                INT PRIMARY KEY IDENTITY(1, 1),
  [Season]            NVARCHAR(10),
  [Year]              INT,
  [StartDate]         DATE,
  [TerminationReason] NVARCHAR(80),
  [OxygenUsed]        BIT,
  [PeakID]            INT,
  [TrekkingAgencyID]  INT
)
GO

ALTER TABLE [Expedition]
  ADD [InjurySustained] BIT
  DEFAULT 0
GO

CREATE TABLE [TrekkingAgency] (
  [ID]    INT PRIMARY KEY IDENTITY(1, 1),
  [Name]  NVARCHAR(100)
)
GO

CREATE TABLE [TeamMember] (
  [ID] INT PRIMARY KEY IDENTITY(1, 1),
  [FirstName] NVARCHAR(30),
  [LastName] NVARCHAR(30),
  [Age]		 INT
  )
GO

CREATE TABLE [Nation](
  [ID] INT PRIMARY KEY IDENTITY(1, 1),
  [Name]	NVARCHAR(30)
)
GO

ALTER TABLE [Expedition] ADD CONSTRAINT [Expedition_FK_Peak] FOREIGN KEY ([PeakID]) REFERENCES [Peak] ([ID])
ALTER TABLE [Expedition] ADD CONSTRAINT [Expedition_FK_TrekkingAgency] FOREIGN KEY ([TrekkingAgencyID]) REFERENCES [TrekkingAgency] ([ID])
GO
