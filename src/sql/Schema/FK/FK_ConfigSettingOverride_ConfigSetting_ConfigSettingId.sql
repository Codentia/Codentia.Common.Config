ALTER TABLE dbo.ConfigSettingOverride ADD CONSTRAINT FK_ConfigSettingOverride_ConfigSetting_ConfigSettingId FOREIGN KEY (ConfigSettingId) REFERENCES dbo.ConfigSetting (ConfigSettingId)
GO
  
