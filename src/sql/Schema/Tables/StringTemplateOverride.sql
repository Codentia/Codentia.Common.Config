CREATE TABLE dbo.StringTemplateOverride
(
	StringTemplateOverrideId	INT IDENTITY(1,1),
	StringTemplateId			INT NOT NULL,
	OverrideReferenceType		NVARCHAR(50),
	OverrideReferenceId			INT NOT NULL,
	[Value]						NVARCHAR(MAX)
) 