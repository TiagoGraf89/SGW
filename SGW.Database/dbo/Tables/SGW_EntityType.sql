CREATE TABLE [dbo].[SGW_EntityType] (
    [EntityTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Name]         VARCHAR (200)    NULL,
    [TriggerBy]    VARCHAR (200)    NULL,
    [CreatedOn]    DATETIME         NULL,
    [CreatedBy]    UNIQUEIDENTIFIER NULL,
    [UpdatedOn]    DATETIME         NULL,
    [UpdatedBy]    UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([EntityTypeId] ASC)
);

