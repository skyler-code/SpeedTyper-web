print '' print '*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
	@UserName	varchar(20),
	@PasswordHash varchar(100)
	)
AS
	BEGIN
		SELECT COUNT(UserID)
		FROM Users
		WHERE UserName = @UserName
		AND PasswordHash = @PasswordHash
	END
GO

print '' print '*** Creating sp_retrieve_user_by_username'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_user_by_username]
	(
	@Username 	varchar(20)
	)
AS
	BEGIN
		SELECT UserID, UserName, DisplayName, RankID, Users.Level, CurrentXP, LevelInfo.RequiredXP
		FROM Users, LevelInfo
		WHERE Username = @Username
		AND Users.Level + 1 = LevelInfo.Level
	END
GO

print '' print '*** Creating sp_retrieve_user_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_user_by_id]
	(
	@UserID 	int
	)
AS
	BEGIN
		SELECT UserID, UserName, DisplayName, RankID, Users.Level, CurrentXP, LevelInfo.RequiredXP
		FROM Users, LevelInfo
		WHERE UserID = @UserID
		AND Users.Level + 1 = LevelInfo.Level
	END
GO

print '' print '*** Creating sp_create_user'
GO
CREATE PROCEDURE [dbo].[sp_create_user]
	(
	@UserName varchar(20),
	@DisplayName varchar(20),
	@PasswordHash varchar(100)
	)
AS
	BEGIN
		INSERT INTO Users
			(UserName, DisplayName, PasswordHash)
		VALUES
			(@UserName, @DisplayName, @PasswordHash)
	END
GO


print '' print '*** Creating sp_update_user'
GO
CREATE PROCEDURE [dbo].[sp_update_user]
	(
	@UserID				int,
	@OldPasswordHash	varchar(100),
	@OldDisplayName		varchar(20),
	@NewDisplayName		varchar(20),
	@NewPasswordHash	varchar(100)
	)
AS
	BEGIN
		UPDATE Users
			SET PasswordHash = @NewPasswordHash,
				DisplayName = @NewDisplayName
			WHERE UserID = @UserID
			AND PasswordHash = @OldPasswordHash
			AND DisplayName = @OldDisplayName
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_rank_names'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_rank_names]
AS
	BEGIN
		SELECT RankID, RankName
		FROM RankInfo
	END
GO


print '' print '*** Creating sp_retrieve_random_test'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_random_test]
AS
	BEGIN
		SELECT TOP 1 TestID, TestDataText, DataSource
		FROM TestData
		ORDER BY NEWID()
	END
GO

print '' print '*** Creating sp_insert_test_result'
GO
CREATE PROCEDURE [dbo].[sp_insert_test_result]
	(
	@UserID int,
	@TestID int,
	@WPM decimal(18,2),
	@SecondsElapsed int
	)
AS
	BEGIN
		INSERT INTO TestResults
			(UserID, TestID, WPM, SecondsElapsed, DateTaken)
		VALUES
			(@UserID, @TestID, @WPM, @SecondsElapsed, GETDATE())
		SELECT CONVERT(int, SCOPE_IDENTITY())
	END
GO

print '' print '*** Creating sp_retrieve_test_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_test_by_id]
	(
	@TestResultID int
	)
AS
	BEGIN
		SELECT TestResultID, UserID, WPM, SecondsElapsed, DateTaken
		FROM TestResults
		WHERE TestResultID = @TestResultID
	END
GO

print '' print '*** Creating sp_retrieve_top_10_test_scores'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_top_10_test_scores]
AS
	BEGIN
		SELECT TOP 10 Users.RankID, Users.DisplayName, WPM, DateTaken
		FROM Users, TestResults
		WHERE Users.UserID = TestResults.UserID
		ORDER BY WPM DESC
	END
GO

print '' print '*** Creating sp_retrieve_all_top_test_scores'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_top_test_scores]
AS
	BEGIN
		SELECT TOP 100 Users.RankID, Users.DisplayName, WPM, DateTaken
		FROM Users, TestResults
		WHERE Users.UserID = TestResults.UserID
		ORDER BY WPM DESC
	END
GO

print '' print '*** Creating sp_retrieve_last_90_days_test_scores'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_last_90_days_test_scores]
AS
	BEGIN
		SELECT TOP 100 Users.RankID, Users.DisplayName, WPM, DateTaken
		FROM Users, TestResults
		WHERE Users.UserID = TestResults.UserID
		AND DateTaken >= DATEADD(DAY, -90, GETDATE())
		ORDER BY WPM DESC
	END
GO

print '' print '*** Creating sp_retrieve_last_30_days_test_scores'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_last_30_days_test_scores]
AS
	BEGIN
		SELECT TOP 100 Users.RankID, Users.DisplayName, WPM, DateTaken
		FROM Users, TestResults
		WHERE Users.UserID = TestResults.UserID
		AND DateTaken >= DATEADD(DAY, -30, GETDATE())
		ORDER BY WPM DESC
	END
GO

print '' print '*** Creating sp_retrieve_todays_test_scores'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_todays_test_scores]
AS
	BEGIN
		SELECT TOP 100 Users.RankID, Users.DisplayName, WPM, DateTaken
		FROM Users, TestResults
		WHERE Users.UserID = TestResults.UserID
		AND DateTaken >= DATEADD(hh, -24, GETDATE())
		ORDER BY WPM DESC
	END
GO


print '' print '*** Creating sp_retrieve_user_last_10_scores'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_user_last_10_scores]
	(
	@UserID int
	)
AS
	BEGIN
		SELECT TOP 10 WPM, SecondsElapsed, DateTaken
		FROM TestResults
		WHERE UserID = @UserID
		ORDER BY DateTaken DESC
	END
GO

print '' print '*** Creating sp_retrieve_highest_ranking_users'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_highest_ranking_users]
AS
	BEGIN
		SELECT TOP 100 DisplayName, RankID, CurrentXP
		FROM Users
		WHERE RankID > 0
		ORDER BY RankID DESC, CurrentXP DESC
	END
GO

print '' print '*** Creating sp_retrieve_wpm_xp_modifier'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_wpm_xp_modifier]
	(
	@WPM decimal(18,2)
	)
AS
	BEGIN
		SELECT MAX(ModifierValue)
		FROM XPModifierInfo
		WHERE @WPM >= RequiredValue
		AND ModifierType = "wpm"
	END
GO

print '' print '*** Creating sp_retrieve_time_xp_modifier'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_time_xp_modifier]
	(
	@SecondsElapsed decimal(18,2)
	)
AS
	BEGIN
		SELECT MAX(ModifierValue)
		FROM XPModifierInfo
		WHERE @SecondsElapsed <= RequiredValue
		AND ModifierType = "time"
	END
GO

print '' print '*** Creating sp_retrieve_required_xp_for_level'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_required_xp_for_level]
	(
	@Level int
	)
AS
	BEGIN
		SELECT RequiredXP
		FROM LevelInfo
		WHERE Level = @Level
	END
GO

print '' print '*** Creating sp_update_user_level_info'
GO
CREATE PROCEDURE [dbo].[sp_update_user_level_info]
	(
	@UserID int,
	@OldLevel int,
	@NewLevel int,
	@OldCurrentXP int,
	@NewCurrentXP int
	)
AS
	BEGIN
		UPDATE Users
		SET Level = @NewLevel,
			CurrentXP = @NewCurrentXP
		WHERE Level = @OldLevel
		AND CurrentXP = @OldCurrentXP
		AND UserID = @UserID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_king'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_king]
AS
	BEGIN
		SELECT UserID
		FROM Users
		WHERE RankID = (SELECT MAX(RankID) FROM RankInfo)
	END
GO

print '' print '*** Creating sp_succession'
GO
CREATE PROCEDURE [dbo].[sp_succession]
AS
	BEGIN
		UPDATE Users
		SET RankID = (SELECT MAX(RankID) FROM RankInfo)
		WHERE CurrentXP = (SELECT MAX(CurrentXP) From Users)
		AND Level = (SELECT MAX(Level)-1 FROM LevelInfo)
		
		UPDATE Users
		SET RankID = (SELECT MAX(RankID)-1 FROM RankInfo)
		WHERE CurrentXP < (SELECT MAX(CurrentXP) From Users)
		AND Level = (SELECT MAX(Level)-1 FROM LevelInfo)
		
		SELECT UserID
		FROM Users
		WHERE RankID = (SELECT MAX(RankID) FROM RankInfo)
	END
GO

print '' print '*** Creating sp_user_rank_up'
GO 
CREATE PROCEDURE [dbo].[sp_user_rank_up]
	(
	@UserID int
	)
AS
	BEGIN
		UPDATE Users
		SET RankID = RankID + 1
		WHERE UserID = @UserID
		AND RankID != (SELECT MAX(RankID) FROM RankInfo)
		RETURN @@ROWCOUNT
	END
GO