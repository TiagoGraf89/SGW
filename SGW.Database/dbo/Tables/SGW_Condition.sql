CREATE TABLE [dbo].[SGW_Condition] (
    [ConditionId]     UNIQUEIDENTIFIER NOT NULL,
    [ConditionType]   VARCHAR (5)      NULL,
    [StoredProcedure] NVARCHAR (500)   NULL,
    [SQLCommand]      NVARCHAR (MAX)   NULL,
    [CreatedOn]       DATETIME         NULL,
    [CreatedBy]       UNIQUEIDENTIFIER NULL,
    [UpdatedOn]       DATETIME         NULL,
    [UpdatedBy]       UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([ConditionId] ASC)
);

