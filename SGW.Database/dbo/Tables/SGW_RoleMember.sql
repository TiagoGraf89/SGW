CREATE TABLE [dbo].[SGW_RoleMember] (
    [ResourceId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId]     UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]  DATETIME         NULL,
    [CreatedBy]  UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([ResourceId] ASC, [RoleId] ASC),
    FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[SGW_Resource] ([ResourceId]),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[SGW_Role] ([RoleId])
);

