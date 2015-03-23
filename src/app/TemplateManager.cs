using System;
using System.Data;
using Codentia.Common.Data;
using Codentia.Common.Helper;

namespace Codentia.Common.Config
{
    /// <summary>
    /// Static class offering an interface to string templates
    /// </summary>
    public static class TemplateManager
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
        /// Gets the string.
        /// </summary>
        /// <param name="stringTemplateCode">The string template code.</param>
        /// <returns>string of template</returns>
        public static string GetString(string stringTemplateCode)
        {
            return TemplateManager.GetString(stringTemplateCode, null, 0);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="stringTemplateCode">The string template code.</param>
        /// <param name="overrideType">Type of the override.</param>
        /// <param name="overrideId">The override id.</param>
        /// <returns>string of template</returns>
        public static string GetString(string stringTemplateCode, string overrideType, int overrideId)
        {
            string value = string.Empty;

            if (!TemplateManager.StringTemplateExists(stringTemplateCode))
            {
                throw new Exception(string.Format("stringTemplateCode '{0}' does not exist", stringTemplateCode));
            }

            DbParameter[] spParams = new DbParameter[]
            {
                new DbParameter("@StringTemplateCode", DbType.String, 100, stringTemplateCode),
                new DbParameter("@OverrideType", DbType.String, 50, string.Empty),
                new DbParameter("@OverrideReference", DbType.Int32, 0),
                new DbParameter("@Value", DbType.String, -1, ParameterDirection.Output, string.Empty)
            };

            if (!string.IsNullOrEmpty(overrideType))
            {
                spParams[1].Value = overrideType;
                spParams[2].Value = overrideId;
            }

            DbInterface.ExecuteProcedureNoReturn(_dataSourceName, "dbo.StringTemplate_Get", spParams);

            return Convert.ToString(spParams[3].Value);
        }

        /// <summary>
        /// Gets all strings.
        /// </summary>
        /// <returns>DataTable of strings</returns>
        public static DataTable GetAllStrings()
        {
            return TemplateManager.GetAllStrings(null, 0);
        }

        /// <summary>
        /// Gets all settings.
        /// </summary>
        /// <param name="overrideType">Type of the override.</param>
        /// <param name="overrideId">The override id.</param>
        /// <returns>DataTable of strings</returns>
        public static DataTable GetAllStrings(string overrideType, int overrideId)
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

            return DbInterface.ExecuteProcedureDataTable(_dataSourceName, "dbo.StringTemplate_GetAll", spParams);
        }

        private static bool StringTemplateExists(string stringTemplateCode)
        {
            ParameterCheckHelper.CheckIsValidString(stringTemplateCode, "stringTemplateCode", false);

            DbParameter[] spParams = new DbParameter[]
            {
                new DbParameter("@StringTemplateCode", DbType.String, 100, stringTemplateCode),
                new DbParameter("@Exists", DbType.Boolean, ParameterDirection.Output, false)
            };

            DbInterface.ExecuteProcedureNoReturn(_dataSourceName, "dbo.StringTemplate_Exists", spParams);

            return Convert.ToBoolean(spParams[1].Value);
        }
    }
}
