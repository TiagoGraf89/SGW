CREATE TABLE [dbo].[SGW_Resource] (
    [ResourceId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]          VARCHAR (200)    NOT NULL,
    [EmailAddress]  VARCHAR (200)    NOT NULL,
    [LoginPassword] VARCHAR (200)    NOT NULL,
    [Active]        BIT              NOT NULL,
    [CreatedOn]     DATETIME         NULL,
    [CreatedBy]     UNIQUEIDENTIFIER NULL,
    [UpdatedOn]     DATETIME         NULL,
    [UpdatedBy]     UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([ResourceId] ASC)
);

