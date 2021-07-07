CREATE TABLE [dbo].[EntityTypes]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Guid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_EntityTypes_Guid] DEFAULT (newid()),
[Name] [varchar] (100) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EntityTypes] ADD CONSTRAINT [PK_EntityTypes] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EntityTypes] ADD CONSTRAINT [IX_EntityTypes_Unique_Guid] UNIQUE NONCLUSTERED  ([Guid]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EntityTypes] ADD CONSTRAINT [IX_EntityTypes_Unique_Name] UNIQUE NONCLUSTERED  ([Name]) ON [PRIMARY]
GO
