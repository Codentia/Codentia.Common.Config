CREATE TABLE dbo.ConfigSettingValue
(
	ConfigSettingValueId	INT IDENTITY(1,1),
	ConfigSettingId			INT NOT NULL,
	[Value]					NVARCHAR(250)
)