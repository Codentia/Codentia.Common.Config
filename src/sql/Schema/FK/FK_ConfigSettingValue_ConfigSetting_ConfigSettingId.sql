ALTER TABLE dbo.ConfigSettingValue ADD CONSTRAINT FK_ConfigSettingValue_ConfigSetting_ConfigSettingId FOREIGN KEY (ConfigSettingId) REFERENCES dbo.ConfigSetting (ConfigSettingId)
GO
 