 IF EXISTS ( SELECT 1 FROM dbo.SysObjects WHERE id=OBJECT_ID('dbo.ConfigSetting_Exists') AND OBJECTPROPERTY(id,'IsProcedure')=1)
	BEGIN
		DROP PROCEDURE dbo.ConfigSetting_Get
	END

GO

CREATE PROCEDURE dbo.ConfigSetting_Exists
	@ConfigSettingCode			NVARCHAR(100),
	@Exists						BIT	OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	IF EXISTS ( SELECT 1 FROM dbo.ConfigSetting WHERE ConfigSettingCode = @ConfigSettingCode )
		BEGIN
			SET @Exists = 1
		END
	ELSE
		BEGIN
			SET @Exists = 0
		END
END

GO 