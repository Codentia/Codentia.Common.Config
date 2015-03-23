CREATE TABLE dbo.StringTemplate
(
	StringTemplateId		INT IDENTITY(1,1),
	StringTemplateCode		NVARCHAR(100),
	DefaultValue			NVARCHAR(MAX)
)