CREATE TABLE [dbo].[Album] (
    [AlbumID]            INT            IDENTITY (1, 1) NOT NULL,
    [AlbumTitle]         VARCHAR (200)  NOT NULL,
    [ArtistID]           INT            NOT NULL,
    [NumberOfTracks]     INT            NOT NULL,
    [ReleaseYear]        INT            NOT NULL,
    [Price]              DECIMAL (5, 2) NOT NULL,
    [AlbumCoverImageURL] VARCHAR (6000) NOT NULL,
    CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED ([AlbumID] ASC),
    CONSTRAINT [ArtistAlbum] FOREIGN KEY ([ArtistID]) REFERENCES [dbo].[Artist] ([ArtistID])
);







