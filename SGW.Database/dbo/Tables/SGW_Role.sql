CREATE TABLE [dbo].[SGW_Role] (
    [RoleId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]      VARCHAR (200)    NOT NULL,
    [CreatedOn] DATETIME         NULL,
    [CreatedBy] UNIQUEIDENTIFIER NULL,
    [UpdatedOn] DATETIME         NULL,
    [UpdatedBy] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

