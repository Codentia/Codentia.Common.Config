 IF EXISTS ( SELECT 1 FROM dbo.SysObjects WHERE id=OBJECT_ID('dbo.ConfigSetting_Get') AND OBJECTPROPERTY(id,'IsProcedure')=1)
	BEGIN
		DROP PROCEDURE dbo.ConfigSetting_Get
	END

GO

CREATE PROCEDURE dbo.ConfigSetting_Get
	@ConfigSettingCode			NVARCHAR(100),
	@OverrideType				NVARCHAR(50)	= NULL,
	@OverrideReference			INT				= NULL,
	@Value						NVARCHAR(250)	OUTPUT,
	@Exists						BIT  = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ConfigSettingId INT


	EXEC dbo.ConfigSetting_Exists
		@ConfigSettingCode			= @ConfigSettingCode,
		@Exists						= @Exists OUTPUT

	SELECT	@Value = [Value]
	FROM	dbo.fn_ConfigSetting(@OverrideType, @OverrideReference)
	WHERE	ConfigSettingCode = @ConfigSettingCode
END

GO