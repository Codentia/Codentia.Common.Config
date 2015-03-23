  IF EXISTS ( SELECT 1 FROM dbo.SysObjects WHERE id=OBJECT_ID('dbo.ConfigSetting_GetAll') AND OBJECTPROPERTY(id,'IsProcedure')=1)
	BEGIN
		DROP PROCEDURE dbo.ConfigSetting_GetAll
	END

GO

CREATE PROCEDURE dbo.ConfigSetting_GetAll
	@OverrideType				NVARCHAR(50)	= NULL,
	@OverrideReference			INT				= NULL
AS
BEGIN

	SET NOCOUNT ON

	SELECT	ConfigSettingCode, 
			[Value]
	FROM	dbo.fn_ConfigSetting(@OverrideType, @OverrideReference)
	
END

GO