ALTER TABLE dbo.StringTemplateValue ADD CONSTRAINT FK_StringTemplateValue_StringTemplate_StringTemplateId FOREIGN KEY (StringTemplateId) REFERENCES dbo.StringTemplate (StringTemplateId)
GO
  