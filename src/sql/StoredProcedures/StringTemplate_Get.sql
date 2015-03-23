 IF EXISTS ( SELECT 1 FROM dbo.SysObjects WHERE id=OBJECT_ID('dbo.StringTemplate_Get') AND OBJECTPROPERTY(id,'IsProcedure')=1)
	BEGIN
		DROP PROCEDURE dbo.StringTemplate_Get
	END

GO

CREATE PROCEDURE dbo.StringTemplate_Get
	@StringTemplateCode			NVARCHAR(100),
	@OverrideType				NVARCHAR(50)	= NULL,
	@OverrideReference			INT				= NULL,
	@Value						NVARCHAR(MAX)	OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @StringTemplateId INT

	SELECT	@Value = [Value]
	FROM	dbo.fn_StringTemplate(@OverrideType, @OverrideReference)
	WHERE	StringTemplateCode = @StringTemplateCode
END

GO