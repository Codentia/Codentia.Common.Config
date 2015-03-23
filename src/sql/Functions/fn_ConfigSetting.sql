IF EXISTS ( SELECT 1 FROM dbo.SysObjects WHERE id=OBJECT_ID('dbo.fn_ConfigSetting') AND OBJECTPROPERTY(id,'IsTableFunction')=1)
	BEGIN
		DROP FUNCTION dbo.fn_ConfigSetting
	END
GO

CREATE FUNCTION dbo.fn_ConfigSetting
	(		
		@OverrideType			NVARCHAR(50),
		@OverrideReference		INT
	)
	RETURNS TABLE
AS
	RETURN
	(
		SELECT	D.ConfigSettingCode, 
				COALESCE(O.Value, V.Value, D.DefaultValue) AS [Value]
		FROM	dbo.ConfigSetting D
		LEFT JOIN	dbo.ConfigSettingValue V
			ON V.ConfigSettingId = D.ConfigSettingId
		LEFT JOIN	dbo.ConfigSettingOverride O
			ON O.ConfigSettingId = D.ConfigSettingId
			AND O.OverrideReferenceType = @OverrideType
			AND O.OverrideReferenceId = @OverrideReference 
	)
GO

 
 
