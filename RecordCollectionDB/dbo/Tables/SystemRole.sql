CREATE TABLE [dbo].[SystemRole] (
    [SystemRoleID]   INT           IDENTITY (1, 1) NOT NULL,
    [SystemRoleName] VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_SystemRole] PRIMARY KEY CLUSTERED ([SystemRoleID] ASC)
);

