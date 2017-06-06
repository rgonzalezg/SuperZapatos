CREATE TABLE [dbo].[Article]
(
	[ArticleId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NULL, 
    [Description] VARCHAR(150) NULL, 
    [Price] DECIMAL(19, 2) NULL, 
    [TotalInShelf] INT NULL, 
    [TotalInVault] INT NULL, 
    [StoreId] INT NULL, 
    CONSTRAINT [FK_Article_Store] FOREIGN KEY ([StoreId]) REFERENCES [Store]([StoreId])
)
