CREATE TABLE [dbo].[SGW_WorkFlowStepTransition] (
    [WorkflowId]   UNIQUEIDENTIFIER NOT NULL,
    [FromStepId]   UNIQUEIDENTIFIER NOT NULL,
    [ToStepId]     UNIQUEIDENTIFIER NOT NULL,
    [TransitionId] UNIQUEIDENTIFIER NOT NULL,
    [Name]         VARCHAR (200)    NOT NULL,
    [CreatedOn]    DATETIME         NULL,
    [CreatedBy]    UNIQUEIDENTIFIER NULL,
    [UpdatedOn]    DATETIME         NULL,
    [UpdatedBy]    UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([TransitionId] ASC),
    FOREIGN KEY ([FromStepId]) REFERENCES [dbo].[SGW_WorkFlowStep] ([StepId]),
    FOREIGN KEY ([ToStepId]) REFERENCES [dbo].[SGW_WorkFlowStep] ([StepId]),
    FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[SGW_Workflow] ([WorkflowId])
);

