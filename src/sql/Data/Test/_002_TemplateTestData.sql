-- test config settings
INSERT INTO dbo.StringTemplate
(
	StringTemplateCode,
	DefaultValue
)
SELECT	'ST1', '10'
UNION ALL
SELECT	'ST2', 'true'
UNION ALL
SELECT	'ST3', 'myconfig'

GO

-- set some overrides to test
INSERT INTO dbo.StringTemplateOverride
(
	StringTemplateId,
	OverrideReferenceType,
	OverrideReferenceId,
	[Value]
)
SELECT	StringTemplateId,
		'ORT1',
		2,
		'20'
FROM	dbo.StringTemplate
WHERE	StringTemplateCode = 'ST1'
		
	 