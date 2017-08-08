CREATE TABLE [dbo].[SGW_Workflow] (
    [WorkflowId]         UNIQUEIDENTIFIER NOT NULL,
    [Name]               VARCHAR (200)    NOT NULL,
    [Active]             BIT              NOT NULL,
    [TriggerConditionId] UNIQUEIDENTIFIER NOT NULL,
    [EntityTypeId]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]          DATETIME         NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NULL,
    [UpdatedOn]          DATETIME         NULL,
    [UpdatedBy]          UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([WorkflowId] ASC),
    FOREIGN KEY ([EntityTypeId]) REFERENCES [dbo].[SGW_EntityType] ([EntityTypeId]),
    FOREIGN KEY ([TriggerConditionId]) REFERENCES [dbo].[SGW_Condition] ([ConditionId])
);

