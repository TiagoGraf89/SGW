CREATE TABLE [dbo].[SGW_Entity] (
    [EntityId]      UNIQUEIDENTIFIER NOT NULL,
    [EntityTypeId]  UNIQUEIDENTIFIER NOT NULL,
    [DataValue]     NVARCHAR (MAX)   NOT NULL,
    [CurrentStatus] VARCHAR (5)      NULL,
    [CreatedOn]     DATETIME         NULL,
    [CreatedBy]     UNIQUEIDENTIFIER NULL,
    [UpdatedOn]     DATETIME         NULL,
    [UpdatedBy]     UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([EntityId] ASC),
    FOREIGN KEY ([EntityTypeId]) REFERENCES [dbo].[SGW_EntityType] ([EntityTypeId])
);

