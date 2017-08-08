CREATE TABLE [dbo].[SGW_WorkflowAccess] (
    [WorkflowAcessId] UNIQUEIDENTIFIER NOT NULL,
    [WorkflowId]      UNIQUEIDENTIFIER NOT NULL,
    [ParticipantId]   UNIQUEIDENTIFIER NOT NULL,
    [Entity]          VARCHAR (5)      NOT NULL,
    [ReadAccess]      BIT              NOT NULL,
    [WriteAccess]     BIT              NOT NULL,
    [AdminAccess]     BIT              NOT NULL,
    [CreatedOn]       DATETIME         NULL,
    [CreatedBy]       UNIQUEIDENTIFIER NULL,
    [UpdatedOn]       DATETIME         NULL,
    [UpdatedBy]       UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([WorkflowAcessId] ASC),
    FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[SGW_Workflow] ([WorkflowId])
);

