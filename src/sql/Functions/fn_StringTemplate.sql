IF EXISTS ( SELECT 1 FROM dbo.SysObjects WHERE id=OBJECT_ID('dbo.fn_StringTemplate') AND OBJECTPROPERTY(id,'IsTableFunction')=1)
	BEGIN
		DROP FUNCTION dbo.fn_StringTemplate
	END
GO

CREATE FUNCTION dbo.fn_StringTemplate
	(		
		@OverrideType			NVARCHAR(50),
		@OverrideReference		INT
	)
	RETURNS TABLE
AS
	RETURN
	(
		SELECT	D.StringTemplateCode, 
				COALESCE(O.Value, V.Value, D.DefaultValue) AS [Value]
		FROM	dbo.StringTemplate D
		LEFT JOIN	dbo.StringTemplateValue V
			ON V.StringTemplateId = D.StringTemplateId
		LEFT JOIN	dbo.StringTemplateOverride O
			ON O.StringTemplateId = D.StringTemplateId
			AND O.OverrideReferenceType = @OverrideType
			AND O.OverrideReferenceId = @OverrideReference 
	)
GO

 
 
 