CREATE TABLE [dbo].[SGW_WorkFlowInstance] (
    [WorkflowInstanceId] UNIQUEIDENTIFIER NOT NULL,
    [WorkflowId]         UNIQUEIDENTIFIER NOT NULL,
    [StepId]             UNIQUEIDENTIFIER NOT NULL,
    [EntityId]           UNIQUEIDENTIFIER NOT NULL,
    [CompletedBy]        UNIQUEIDENTIFIER NULL,
    [Completed]          BIT              NOT NULL,
    [CreatedOn]          DATETIME         NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NULL,
    [UpdatedOn]          DATETIME         NULL,
    [UpdatedBy]          UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([WorkflowInstanceId] ASC),
    FOREIGN KEY ([EntityId]) REFERENCES [dbo].[SGW_Entity] ([EntityId]),
    FOREIGN KEY ([StepId]) REFERENCES [dbo].[SGW_WorkFlowStep] ([StepId]),
    FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[SGW_Workflow] ([WorkflowId])
);

