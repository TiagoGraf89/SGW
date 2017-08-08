CREATE TABLE [dbo].[SGW_WorkFlowStep] (
    [WorkflowId]   UNIQUEIDENTIFIER NOT NULL,
    [StepId]       UNIQUEIDENTIFIER NOT NULL,
    [StepTypeId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]         VARCHAR (200)    NOT NULL,
    [JoinDecision] VARCHAR (5)      NOT NULL,
    [CreatedOn]    DATETIME         NULL,
    [CreatedBy]    UNIQUEIDENTIFIER NULL,
    [UpdatedOn]    DATETIME         NULL,
    [UpdatedBy]    UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([StepId] ASC),
    FOREIGN KEY ([StepTypeId]) REFERENCES [dbo].[SGW_StepType] ([StepTypeId]),
    FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[SGW_Workflow] ([WorkflowId])
);

