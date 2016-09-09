CREATE TABLE [dbo].[chapter]
(
    [report] NVARCHAR(50) NOT NULL, 
    [chapter] INT NOT NULL, 
    [chapter_name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_chapter] PRIMARY KEY ([chapter], [report])
)
