CREATE TABLE [dbo].[SGW_WorkFlowInstanceAssignment] (
    [WorkFlowInstanceAssignmentId] UNIQUEIDENTIFIER NOT NULL,
    [WorkflowInstanceId]           UNIQUEIDENTIFIER NOT NULL,
    [ParticipantId]                UNIQUEIDENTIFIER NOT NULL,
    [Entity]                       VARCHAR (5)      NOT NULL,
    [Mandatory]                    BIT              NOT NULL,
    [CreatedOn]                    DATETIME         NULL,
    [CreatedBy]                    UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([WorkFlowInstanceAssignmentId] ASC),
    FOREIGN KEY ([WorkflowInstanceId]) REFERENCES [dbo].[SGW_WorkFlowInstance] ([WorkflowInstanceId])
);

