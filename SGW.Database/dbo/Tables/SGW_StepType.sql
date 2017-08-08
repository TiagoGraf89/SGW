CREATE TABLE [dbo].[SGW_StepType] (
    [StepTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Name]       VARCHAR (200)    NOT NULL,
    [CreatedOn]  DATETIME         NULL,
    [CreatedBy]  UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([StepTypeId] ASC)
);

