-- test config settings
INSERT INTO dbo.ConfigSetting
(
	ConfigSettingCode,
	Title,
	DefaultValue
)
SELECT	'CONFIG1', 'Configuration Setting 1', '10'
UNION ALL
SELECT	'CONFIG2', 'Configuration Setting 2', 'true'
UNION ALL
SELECT	'CONFIG3', 'Configuration Setting 3', 'myconfig'

GO

-- set some overrides to test
INSERT INTO dbo.ConfigSettingOverride
(
	ConfigSettingId,
	OverrideReferenceType,
	OverrideReferenceId,
	[Value]
)
SELECT	ConfigSettingId,
		'ORT1',
		2,
		'20'
FROM	dbo.ConfigSetting
WHERE	ConfigSettingCode = 'CONFIG1'
		
	