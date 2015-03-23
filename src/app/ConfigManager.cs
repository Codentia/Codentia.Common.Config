using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Configuration;
using Codentia.Common.Data;
using Codentia.Common.Helper;

namespace Codentia.Common.Config
{
    /// <summary>
    /// Static class offering an interface to common configuration
    /// </summary>
    public static class ConfigManager
    {
        private static string _dataSourceName = "config";

        /// <summary>
        /// Gets or sets the name of the data source.
        /// </summary>
        /// <value>The name of the data source.</value>
        public static string DataSourceName
        {
            get
            {
                return _dataSourceName;
            }

            set
            {
                _dataSourceName = value;
            }
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="configSettingCode">The setting code.</param>
        /// <returns>TValue of setting</returns>
        public static TValue GetSetting<TValue>(string configSettingCode)
        {
            return ConfigManager.GetSetting<TValue>(configSettingCode, null, 0);
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="configSettingCode">The setting code.</param>
        /// <param name="overrideType">Type of the override.</param>
        /// <param name="overrideId">The override id.</param>
        /// <returns>TValue of Setting</returns>
        public static TValue GetSetting<TValue>(string configSettingCode, string overrideType, int overrideId)
        {
            ParameterCheckHelper.CheckIsValidString(configSettingCode, "configSettingCode", false);

            TValue configValue = default(TValue);

            DbParameter[] spParams = new DbParameter[]
            {
                new DbParameter("@ConfigSettingCode", DbType.String, 100, configSettingCode),
                new DbParameter("@OverrideType", DbType.String, 50, string.Empty),
                new DbParameter("@OverrideReference", DbType.Int32, 0),
                new DbParameter("@Value", DbType.String, 250, ParameterDirection.Output, string.Empty),
                new DbParameter("@Exists", DbType.Boolean, ParameterDirection.Output, false)
            };

            if (!string.IsNullOrEmpty(overrideType))
            {
                spParams[1].Value = overrideType;
                spParams[2].Value = overrideId;
            }

            DbInterface.ExecuteProcedureNoReturn(_dataSourceName, "dbo.ConfigSetting_Get", spParams);

            if (!Convert.ToBoolean(spParams[4].Value))
            {
                throw new Exception(string.Format("configSettingCode '{0}' does not exist", configSettingCode));
            }

            try
            {
                configValue = (TValue)Convert.ChangeType(spParams[3].Value, typeof(TValue));
                /*
                switch (typeof(TValue).ToString())
                {
                    case "System.Int32":
                        int intValue = Convert.ToInt32(spParams[3].Value);
                        configValue = (TValue)Convert.ChangeType(intValue, typeof(TValue));
                        break;
                    default:
                        throw System.NotImplementedException(string.Format("Type '{0}' not handled", typeof(TValue).ToString()));
                }*/
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("configSettingCode '{0}' cannot be cast as type '{1}'", configSettingCode, typeof(TValue).ToString()), ex);
            }

            return configValue;
        }

        /// <summary>
        /// Gets all settings.
        /// </summary>
        /// <returns>DataTable of settings</returns>
        public static DataTable GetAllSettings()
        {
            return ConfigManager.GetAllSettings(null, 0);
        }

        /// <summary>
        /// Gets all settings.
        /// </summary>
        /// <param name="overrideType">Type of the override.</param>
        /// <param name="overrideId">The override id.</param>
        /// <returns>DataTable of settings</returns>
        public static DataTable GetAllSettings(string overrideType, int overrideId)
        {
            DbParameter[] spParams = new DbParameter[]
            {
                new DbParameter("@OverrideType", DbType.String, 50, string.Empty),
                new DbParameter("@OverrideReference", DbType.Int32, 0)
            };

            if (!string.IsNullOrEmpty(overrideType))
            {
                spParams[0].Value = overrideType;
                spParams[1].Value = overrideId;
            }

            return DbInterface.ExecuteProcedureDataTable(_dataSourceName, "dbo.ConfigSetting_GetAll", spParams);
        }

        /// <summary>
        /// Gets the app config.
        /// </summary>
        /// <returns>Configuration object</returns>
        public static Configuration GetAppConfig()
        {
            if (HttpContext.Current != null)
            {
                return WebConfigurationManager.OpenWebConfiguration(HttpRuntime.AppDomainAppVirtualPath);
            }
            else
            {
                return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
        }
    }
}
