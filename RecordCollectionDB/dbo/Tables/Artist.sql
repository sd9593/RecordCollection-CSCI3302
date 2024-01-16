CREATE TABLE [dbo].[Artist] (
    [ArtistID]       INT            IDENTITY (1, 1) NOT NULL,
    [ArtistName]     VARCHAR (100)  NOT NULL,
    [ArtistBio]      VARCHAR (2000) NOT NULL,
    [ArtistImageURL] VARCHAR (6000) NULL,
    [ArtistWebsite]  VARCHAR (6000) NULL,
    CONSTRAINT [PK_Artist] PRIMARY KEY CLUSTERED ([ArtistID] ASC)
);



