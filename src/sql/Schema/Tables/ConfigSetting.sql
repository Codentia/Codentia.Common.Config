CREATE TABLE dbo.ConfigSetting
(
	ConfigSettingId			INT IDENTITY(1,1),
	ConfigSettingCode		NVARCHAR(100),
	Title					NVARCHAR(200),
	DefaultValue			NVARCHAR(250)
)