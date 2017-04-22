print '' print '*** Inserting LevelInfo Records'
GO
INSERT INTO [dbo].[LevelInfo]
	([Level], [RequiredXP])
	VALUES
	-- XP Formula found in Documentation/XP Formula.xlsx
		(0, 0),
		(1, 200),
		(2, 800),
		(3, 1800),
		(4, 3200),
		(5, 5000),
		(6, 7200),
		(7, 9800),
		(8, 12800),
		(9, 16200),
		(10, 20000),
		(11, 24200),
		(12, 28800),
		(13, 33800),
		(14, 39200),
		(15, 45000), -- Max Level
		(16, -1) -- For Future Use
GO

print '' print '*** Inserting RankInfo Records'
GO
INSERT INTO [dbo].[RankInfo]
	([RankID], [RankName])
	VALUES
	-- These are the Alliance honor ranks from vanilla WoW. Sadly, I knew this without looking it up.
	(0, "Citizen"),
	(1, "Private"),
	(2, "Corporal"),
	(3, "Sergeant"),
	(4, "Master Sergeant"),
	(5, "Sergeant Major"),
	(6, "Knight"),
	(7, "Knight-Lieutenant"),
	(8, "Knight-Captain"),
	(9, "Knight-Champion"),
	(10, "Lieutenant Commander"),
	(11, "Commander"),
	(12, "Marshal"),
	(13, "Field Marshal"),
	(14, "Grand Marshal"),
	(15, "King")
GO

print '' print '*** Inserting XPModifierInfo Records'
GO
INSERT INTO [dbo].[XPModifierInfo]
	([ModifierType], [RequiredValue], [ModifierValue])
	VALUES
	('wpm', 0.0, 1.0),
	('wpm', 30.0, 1.5),
	('wpm', 50.0, 2.0),
	('wpm', 65.0, 2.5),
	('wpm', 80.0, 3.0),
	('wpm', 115.0, 4.0),
	('time', 15.0, 3.0),
	('time', 30.0, 2.0),
	('time', 45.0, 1.5),
	('time', 9999.0, 1.0)
GO

		

print '' print '*** Inserting TestData Records'
GO
INSERT INTO [dbo].[TestData]
		([TestDataText], [DataSource])
	VALUES
	-- Thanks to http://www.seanwrona.com/typeracer/texts.php
		('I know a bloke who knows a bloke who knows a bloke. Now, I know you know this bloke. This is a bloke you know.', '- from Sexy Beast, a movie directed by Jonathan Glazer'),
		('Good games offer players a set of challenging problems and then let them practice these until they have routinized their mastery.', '- from What Video Games Have to Teach Us About Learning and Literacy, a book by James Paul Gee'),
		('Remember that humor is written backwards. That means you first find the cliche you want to work on, then build a story around it.', '- from Comedy Writing Secrets, a book by Mel Helitzer and Mark Shatz'),
		('Ever since I was a child, folks have thought they had me pegged, because of the way I am, the way I talk. And they''re always wrong.', '- from Capote, a movie directed by Bennett Miller'),
		('For centuries, the battle of morality was fought between those who claimed that your life belongs to God and those who claimed that it belongs to your neighbors. And no one came to say that your life belongs to you and that the good is to live it.', '- from Atlas Shrugged, a book by Ayn Rand'),
		('You''ve got a way to keep me on your side. You give me cause for love that I can''t hide. For you I know I''d even try to turn the tide. Because you''re mine, I walk the line.', '- from I Walk the Line, a song by Johnny Cash'),
		('There''s a piece of Maria in every song that I sing. And the price of a memory is the memory of the sorrow it brings. And there is always one last light to turn out and one last bell to ring. And the last one out of the circus has to lock up everything.', '- from Mrs. Potter''s Lullaby, a song by Counting Crows'),
		('That''s Tommy. He tells people he was named after a gun, but I know he was really named after a famous 19th century ballet dancer.', '- from Snatch, a movie directed by Guy Ritchie'),
		('The most precious treasures we have in life are the images we store in the memory banks of our brains. The sum of these stored experiences is responsible for our sense of personal identity and our sense of connectedness to those around us.', '- from Change Your Brain, Change Your Life, a book by Daniel G. Amen'),
		('Ticking away the moments that make up a dull day, you fritter and waste the hours in an off-hand way. Kicking around on a piece of ground in your home town, waiting for someone or something to show you the way.', '- from Time, a song by Pink Floyd'),
		('In the casino, the cardinal rule is to keep them playing and to keep them coming back. The longer they play, the more they lose, and in the end, we get it all. Running a casino is like robbing a bank with no cops around.', '- from Casino, a movie directed by Martin Scorsese'),
		('A computer needs a manager to administer its operations, just as a company needs a manager. And that is what DOS is. A manager. It manages the operations in your computer.', '- from Mastering Computer Typing (1995), a book by Sheryl Lindsell-Roberts'),
		('I''ve been looking so long at these pictures of you that I almost believe that they''re real. I''ve been living so long with my pictures of you that I almost believe that the pictures are all I can feel.', '- from Pictures of You, a song by The Cure'),
		('I went to the woods because I wished to live deliberately, to front only the essential facts of life, and see if I could not learn what it had to teach, and not, when I came to die, discover that I had not lived.', '- from Walden, a book by Henry David Thoreau'),
		('Your face has fallen sad now, for you know the time is nigh. But when I must remove your wings and you, you must try to fly. Come sail your ships around me and burn your bridges down. We make a little history, baby, every time you come around.', '- from The Ship Song, a song by Nick Cave & The Bad Seeds'),
		('Let''s sort the buyers from the spiers, the needy from the greedy, and those who trust me from the ones who don''t, because if you can''t see value here today, you''re not up here shopping. You''re up here shoplifting. Anyone like jewelry?', '- from Lock, Stock, and Two Smoking Barrels, a movie directed by Guy Ritchie'),
		('You have a class of young strong men and women, and they want to give their lives to something. Advertising has these people chasing cars and clothes they don''t need. Generations have been working in jobs they hate, just so they can buy what they don''t really need.', '- from Fight Club, a book by Chuck Palahniuk'),
		('There''s a hole in the world like a great black pit. And the vermin of the world inhabit it. And its morals aren''t worth what a pig could spit. And it goes by the name of London.', '- from Sweeney Todd: The Demon Barber, a movie directed by Tim Burton'),
		('The alternative to binge travel - the mini retirement - entails relocating to one place for one to six months before going home or moving to another locale. Rather than seeking to see the world, we aim to experience it at a speed that lets it change us.', '- from The 4-Hour Workweek, a book by Timothy Ferriss'),
		('Now I want you to think and stop being a smart aleck. A man''s attitude goes some ways - the way his life will be. Is that something you agree with? So since you agree, you must be someone who does not care about the good life.', '- from Mulholland Drive, a movie directed by David Lynch'),
		('Being able to quit things that don''t work is integral to being a winner. Going into a project or job without defining when worthwhile becomes wasteful is like going into a casino without a cap on what you will gamble: dangerous and foolish.', '- from The 4-Hour Workweek, a book by Timothy Ferriss'),
		('All time is all time. It does not change. It does not lend itself to warnings or explanations. It simply is. Take it moment by moment, and you will find that we are all, as I''ve said before, bugs in amber.', '- from Slaughterhouse Five, a book by Kurt Vonnegut'),
		('No matter how bad things are, you can always make things worse. At the same time, it is often within your power to make them better. I learned this lesson well on New Year''s Eve, 2001.', '- from The Last Lecture, a book by Randy Pausch'),
		('When the light of life has gone, no change for the meter. Then the king of spivs will come, selling blood by the litre. When nothing''s sacred anymore, when the demon''s knocking on your door, you''ll still be staring down at the floor.', '- from Swamp Thing, a song by The Chameleons'),
		('If I''m curt with you it''s because time is a factor. I think fast, I talk fast and I need you guys to act fast if you wanna get out of this. So, pretty please with sugar on top. Clean the car!', '- from Pulp Fiction, a movie directed by Quentin Tarantino'),
		('Rhymes trap you into saying things you don''t want to say. A word like ''fire'' is a good example. Before you know it you''re reaching for desire, or to get higher, or calling someone a liar, or putting them on a pyre, even if that wasn''t what you were going to say.', '- from Lyrics: Writing Better Words for Your Songs, a book by Rikky Rooksby'),
		('At first they had tried to keep the finding quiet. After all, they were not absolutely sure it was an extraterrestrial message. A premature or mistaken announcement would be a public relations disaster. But worse than that, it would interfere with the data analysis. If the press descended, the science would surely suffer.', '- from Contact, a book by Carl Sagan'),
		('Is this the real life? Is this just fantasy? Caught in a landslide, no escape from reality. Open your eyes, look up to the skies and see. I''m just a poor boy, I need no sympathy. Because I''m easy come, easy go, little high, little low. Any way the wind blows doesn''t really matter to me.', '- from Bohemian Rhapsody, a song by Queen'),
		('That day, for no particular reason, I decided to go for a little run. So I ran to the end of the road. And when I got there, I thought maybe I''d run to the end of town. And when I got there, I thought maybe I''d just run across Greenbow County. And I figured, since I run this far, maybe I''d just run across the great state of Alabama.', '- from Forrest Gump, a movie directed by Robert Zemeckis'),
		('The world of typing has changed. In the 1970s, every business had rooms full of secretaries whose job it was to type letters that had been hand-written. They were copying the writing into a more readable format. In the early 1980s, the personal computer became a common office machine.', '- from Keyboarding Made Simple (1985), a book by Leigh E. Zeitz, Ph.D.')
GO			