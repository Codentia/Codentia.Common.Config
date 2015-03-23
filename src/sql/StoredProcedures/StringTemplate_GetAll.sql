  IF EXISTS ( SELECT 1 FROM dbo.SysObjects WHERE id=OBJECT_ID('dbo.StringTemplate_GetAll') AND OBJECTPROPERTY(id,'IsProcedure')=1)
	BEGIN
		DROP PROCEDURE dbo.StringTemplate_GetAll
	END

GO

CREATE PROCEDURE dbo.StringTemplate_GetAll
	@OverrideType				NVARCHAR(50)	= NULL,
	@OverrideReference			INT				= NULL
AS
BEGIN

	SET NOCOUNT ON

	SELECT	StringTemplateCode, 
			[Value]
	FROM	dbo.fn_StringTemplate(@OverrideType, @OverrideReference)
	
END

GO