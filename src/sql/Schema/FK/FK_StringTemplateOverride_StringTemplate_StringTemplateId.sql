 ALTER TABLE dbo.StringTemplateOverride ADD CONSTRAINT FK_StringTemplateOverride_StringTemplate_StringTemplateId FOREIGN KEY (StringTemplateId) REFERENCES dbo.StringTemplate (StringTemplateId)
GO
  
