CREATE TABLE dbo.ConfigSettingOverride
(
	ConfigSettingOverrideId		INT IDENTITY(1,1),
	ConfigSettingId				INT NOT NULL,
	OverrideReferenceType		NVARCHAR(50),
	OverrideReferenceId			INT NOT NULL,
	[Value]						NVARCHAR(250)
)