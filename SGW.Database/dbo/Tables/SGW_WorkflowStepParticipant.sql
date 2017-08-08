CREATE TABLE [dbo].[SGW_WorkflowStepParticipant] (
    [WorkflowStepParticipantId] UNIQUEIDENTIFIER NOT NULL,
    [StepId]                    UNIQUEIDENTIFIER NOT NULL,
    [ParticipantId]             UNIQUEIDENTIFIER NOT NULL,
    [Entity]                    VARCHAR (5)      NOT NULL,
    [Notify]                    BIT              NOT NULL,
    [CreatedOn]                 DATETIME         NULL,
    [CreatedBy]                 UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([WorkflowStepParticipantId] ASC),
    FOREIGN KEY ([StepId]) REFERENCES [dbo].[SGW_WorkFlowStep] ([StepId])
);

