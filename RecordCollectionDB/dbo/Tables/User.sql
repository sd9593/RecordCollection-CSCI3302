CREATE TABLE [dbo].[User] (
    [UserID]        INT           IDENTITY (1, 1) NOT NULL,
    [Email]         VARCHAR (50)  NOT NULL,
    [FirstName]     VARCHAR (50)  NOT NULL,
    [LastName]      VARCHAR (50)  NOT NULL,
    [AvatarURL]     VARCHAR (150) NOT NULL,
    [Phone]         VARCHAR (50)  NOT NULL,
    [DateJoined]    DATETIME      NOT NULL,
    [Salt]          VARCHAR (25)  NOT NULL,
    [PasswordHash]  VARCHAR (300) NOT NULL,
    [LastLoginTime] DATETIME      NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

