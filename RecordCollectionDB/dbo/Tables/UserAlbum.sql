CREATE TABLE [dbo].[UserAlbum] (
    [UserID]  INT NOT NULL,
    [AlbumID] INT NOT NULL,
    CONSTRAINT [FK_UserAlbum_Album] FOREIGN KEY ([AlbumID]) REFERENCES [dbo].[Album] ([AlbumID]),
    CONSTRAINT [FK_UserAlbum_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

