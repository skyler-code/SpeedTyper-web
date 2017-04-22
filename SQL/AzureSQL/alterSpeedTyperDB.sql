print '' print '*** Creating Users RankID foreign key'
GO
ALTER TABLE [dbo].[Users]  WITH NOCHECK 
	ADD CONSTRAINT [FK_RankID] FOREIGN KEY([RankID])
	REFERENCES [dbo].[RankInfo] ([RankID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Users Level foreign key'
GO
ALTER TABLE [dbo].[Users]  WITH NOCHECK 
	ADD CONSTRAINT [FK_Level] FOREIGN KEY([Level])
	REFERENCES [dbo].[LevelInfo] ([Level])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TestResults UserID foreign key'
GO
ALTER TABLE [dbo].[TestResults]  WITH NOCHECK 
	ADD CONSTRAINT [FK_UserID] FOREIGN KEY([UserID])
	REFERENCES [dbo].[Users] ([UserID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TestResults TestID foreign key'
GO
ALTER TABLE [dbo].[TestResults]  WITH NOCHECK 
	ADD CONSTRAINT [FK_TestID] FOREIGN KEY([TestID])
	REFERENCES [dbo].[TestData] ([TestID])
	ON UPDATE CASCADE
GO