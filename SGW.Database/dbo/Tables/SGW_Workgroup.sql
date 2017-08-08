CREATE TABLE [dbo].[SGW_Workgroup] (
    [WorkgroupId]       UNIQUEIDENTIFIER NOT NULL,
    [ParentWorkgroupId] UNIQUEIDENTIFIER NOT NULL,
    [Name]              VARCHAR (200)    NOT NULL,
    [CreatedOn]         DATETIME         NULL,
    [CreatedBy]         UNIQUEIDENTIFIER NULL,
    [UpdatedOn]         DATETIME         NULL,
    [UpdatedBy]         UNIQUEIDENTIFIER NULL,
    [Path] VARCHAR(MAX) NULL, 
    PRIMARY KEY CLUSTERED ([WorkgroupId] ASC)
);

