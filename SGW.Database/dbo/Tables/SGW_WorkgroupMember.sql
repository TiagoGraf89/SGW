CREATE TABLE [dbo].[SGW_WorkgroupMember] (
    [ResourceId]  UNIQUEIDENTIFIER NOT NULL,
    [WorkgroupId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         NULL,
    [CreatedBy]   UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([ResourceId] ASC, [WorkgroupId] ASC),
    FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[SGW_Resource] ([ResourceId]),
    FOREIGN KEY ([WorkgroupId]) REFERENCES [dbo].[SGW_Workgroup] ([WorkgroupId])
);

