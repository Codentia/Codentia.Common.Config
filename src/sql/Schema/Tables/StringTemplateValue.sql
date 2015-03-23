CREATE TABLE dbo.StringTemplateValue
(
	StringTemplateValueId	INT IDENTITY(1,1),
	StringTemplateId		INT NOT NULL,
	[Value]					NVARCHAR(MAX)
) 