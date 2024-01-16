CREATE TABLE [dbo].[UserSystemRole] (
    [UserID]       INT NOT NULL,
    [SystemRoleID] INT NOT NULL,
    CONSTRAINT [FK_UserSystemRole_SystemRole] FOREIGN KEY ([SystemRoleID]) REFERENCES [dbo].[SystemRole] ([SystemRoleID]),
    CONSTRAINT [FK_UserSystemRole_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

