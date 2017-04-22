IF OBJECT_ID(N'dbo.Users', N'U') IS NOT NULL
BEGIN
	DROP TABLE Users
	print '' print '*** dropping Users table'
END
GO

print '' print '*** Creating Users Table'
GO
CREATE TABLE [dbo].[Users](
	[UserID] 		[int] IDENTITY (1,1)	NOT NULL,
	[UserName]		[varchar](20)			NOT NULL,
	[DisplayName]	[varchar](20)			NOT NULL,
	[PasswordHash]	[varchar](100)			NOT NULL DEFAULT 'f153683d3b1aafadd5ecb6bfb96fa0d6557dddc87529d1db5e2057da173d07a4', -- testuser1 (only sample accounts will use this)
	[RankID]		[int]					NOT NULL DEFAULT 0,
	[Level]			[int]					NOT NULL DEFAULT 0,
	[CurrentXP]		[int]					NOT NULL DEFAULT 0,

	CONSTRAINT [pk_UserID] PRIMARY KEY([UserID] ASC),
	CONSTRAINT [ak_Username] UNIQUE ([Username] ASC)
)
GO

IF OBJECT_ID(N'dbo.TestResults', N'U') IS NOT NULL
BEGIN
	DROP TABLE TestResults
	print '' print '*** dropping TestResults table'
END
GO

print '' print '*** Creating TestResults Table'
GO

CREATE TABLE [dbo].[TestResults](
	[TestResultID]	[int] IDENTITY(1,1) NOT NULL,
	[UserID]		[int] 				NOT NULL,
	[TestID]		[int]				NOT NULL,
	[WPM]			[decimal](18,2)		NOT NULL,
	[SecondsElapsed][int]				NOT NULL,
	[DateTaken]		[datetime]			NOT NULL,
	
	CONSTRAINT [pk_TestResultID] PRIMARY KEY([TestResultID] ASC)
)
GO

IF OBJECT_ID(N'dbo.TestData', N'U') IS NOT NULL
BEGIN
	DROP TABLE TestData
	print '' print '*** dropping TestData table'
END
GO

print '' print '*** Creating TestData Table'
GO

CREATE TABLE [dbo].[TestData](
	[TestID]		[int]IDENTITY(1,1)	NOT NULL,
	[TestDataText]	[varchar](1000)		NOT NULL,
	[DataSource]	[varchar](500)		NOT NULL,
	CONSTRAINT [pk_TestID] PRIMARY KEY([TestID] ASC)
)
GO

IF OBJECT_ID(N'dbo.LevelInfo', N'U') IS NOT NULL
BEGIN
	DROP TABLE LevelInfo
	print '' print '*** dropping LevelInfo table'
END
GO

print '' print '*** Creating LevelInfo Table'
GO
CREATE TABLE [dbo].[LevelInfo](
	[Level]			[int]			NOT NULL,
	[RequiredXP]	[int]			NOT NULL,
	CONSTRAINT [pk_Level] PRIMARY KEY([Level] ASC)
)
GO

IF OBJECT_ID(N'dbo.XPModifierInfo', N'U') IS NOT NULL
BEGIN
	DROP TABLE XPModifierInfo
	print '' print '*** dropping XPModifierInfo table'
END
GO

print '' print '*** Creating XPModifierInfo Table'
GO
CREATE TABLE [dbo].[XPModifierInfo](
	[ModifierType]	[varchar](10)		NOT NULL,
	[RequiredValue]	[decimal](18,2)		NOT NULL,
	[ModifierValue]	[decimal](18,2)		NOT NULL
)

IF OBJECT_ID(N'dbo.RankInfo', N'U') IS NOT NULL
BEGIN
	DROP TABLE RankInfo
	print '' print '*** dropping RankInfo table'
END
GO

print '' print '*** Creating RankInfo Table'
GO
CREATE TABLE [dbo].[RankInfo](
	[RankID]		[int]			NOT NULL,
	[RankName]		[varchar](30)	NOT NULL,
	CONSTRAINT [pk_RankID] PRIMARY KEY([RankID] ASC)
)
GO