CREATE TABLE [dbo].[main]
(
    [report] NVARCHAR(50) NOT NULL, 
    [active] BIT NOT NULL, 
    [period] INT NULL, 
    [start] DATE NULL, 
    [presentation_title] NVARCHAR(50) NOT NULL, 
    [save_to_path] NVARCHAR(MAX) NOT NULL, 
    [design_template_path] NVARCHAR(MAX) NULL, 
    PRIMARY KEY ([report])
)
