CREATE TABLE [dbo].[script]
(
    [report] NVARCHAR(50) NOT NULL, 
    [chapter] INT NOT NULL, 
    [source] INT NOT NULL, 
	[source_path] NVARCHAR(MAX) NOT NULL, 
    PRIMARY KEY ([source], [report], [chapter])
)
