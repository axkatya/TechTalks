USE master ; 
GO
CREATE DATABASE TalksDB
GO
USE TalksDB
GO
CREATE TABLE [Talks] (
	TalkId int NOT NULL IDENTITY(1,1),
	TalkDate date NOT NULL,
	Topic nvarchar(140) NOT NULL,
	AdditionalDetail nvarchar(280),
	Speaker int,
	Discipline int,
	PresentationLink nvarchar(max),
	Location nvarchar(100) NOT NULL,
  CONSTRAINT [PK_Talks_TalkId] PRIMARY KEY CLUSTERED
  (
  [talkId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Speakers] (
	SpeakerId int NOT NULL IDENTITY(1,1),
	FirstName nvarchar(20) NOT NULL,
	LastName nvarchar(20) NOT NULL,
	Email nvarchar(50),
	Position nvarchar(50) NOT NULL,
	Department nvarchar(50),
	Location nvarchar(100) NOT NULL,
  CONSTRAINT [PK_Speakers_SpeakerId] PRIMARY KEY CLUSTERED
  (
  [SpeakerId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Disciplines] (
	DisciplineId int NOT NULL IDENTITY(1,1),
	DisciplineName nvarchar(50) NOT NULL,
  CONSTRAINT [PK_Disciplines_DisciplineId] PRIMARY KEY CLUSTERED
  (
  [DisciplineId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
ALTER TABLE [Talks] WITH CHECK ADD CONSTRAINT [FK_Speakers_Speaker] FOREIGN KEY ([Speaker]) REFERENCES [Speakers]([speakerId])
ON UPDATE CASCADE
GO
ALTER TABLE [Talks] CHECK CONSTRAINT [FK_Speakers_Speaker]
GO
ALTER TABLE [Talks] WITH CHECK ADD CONSTRAINT [FK_Disciplines_Discipline] FOREIGN KEY ([Discipline]) REFERENCES [Disciplines]([disciplineId])
ON UPDATE CASCADE
GO
ALTER TABLE [Talks] CHECK CONSTRAINT [FK_Disciplines_Discipline]
GO
GO


