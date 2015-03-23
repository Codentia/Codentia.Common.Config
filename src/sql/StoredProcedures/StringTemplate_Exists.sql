 IF EXISTS ( SELECT 1 FROM dbo.SysObjects WHERE id=OBJECT_ID('dbo.StringTemplate_Exists') AND OBJECTPROPERTY(id,'IsProcedure')=1)
	BEGIN
		DROP PROCEDURE dbo.StringTemplate_Get
	END

GO

CREATE PROCEDURE dbo.StringTemplate_Exists
	@StringTemplateCode			NVARCHAR(100),
	@Exists						BIT	OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	IF EXISTS ( SELECT 1 FROM dbo.StringTemplate WHERE StringTemplateCode = @StringTemplateCode )
		BEGIN
			SET @Exists = 1
		END
	ELSE
		BEGIN
			SET @Exists = 0
		END
END

GO  