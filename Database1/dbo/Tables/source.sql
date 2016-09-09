CREATE TABLE [dbo].[source]
(
    [report] NVARCHAR(50) NOT NULL, 
    [chapter] INT NOT NULL, 
    [source_path] NVARCHAR(MAX) NOT NULL, 
    PRIMARY KEY ([source_path], [report], [chapter])
)
