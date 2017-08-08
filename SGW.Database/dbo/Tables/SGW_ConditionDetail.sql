CREATE TABLE [dbo].[SGW_ConditionDetail] (
    [ConditionId]       UNIQUEIDENTIFIER NOT NULL,
    [ConditionDetailId] UNIQUEIDENTIFIER NOT NULL,
    [GroupIdentifier]   VARCHAR (200)    NOT NULL,
    [Field]             VARCHAR (200)    NOT NULL,
    [Operator]          VARCHAR (20)     NOT NULL,
    [Value1]            VARCHAR (MAX)    NULL,
    [Value2]            VARCHAR (MAX)    NULL,
    [CreatedOn]         DATETIME         NULL,
    [CreatedBy]         UNIQUEIDENTIFIER NULL,
    [UpdatedOn]         DATETIME         NULL,
    [UpdatedBy]         UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([ConditionDetailId] ASC),
    FOREIGN KEY ([ConditionId]) REFERENCES [dbo].[SGW_Condition] ([ConditionId])
);

