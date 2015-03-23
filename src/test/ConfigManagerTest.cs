using System;
using System.Configuration;
using System.Data;
using System.Web;
using Codentia.Test.Helper;
using NUnit.Framework;

namespace Codentia.Common.Config.Test
{
    /// <summary>
    /// Unit testing framework for ConfigManager
    /// </summary>
    [TestFixture]
    public class ConfigManagerTest
    {
        /// <summary>
        /// Prepare for tests to execute
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // check initial value - if wrong, fail as something bad has happened
            ////Assert.That(ConfigManager.DataSourceName, Is.EqualTo("config"));

            // now set to test value
            ConfigManager.DataSourceName = "config_test";
            Assert.That(ConfigManager.DataSourceName, Is.EqualTo("config_test"));
        }

        /// <summary>
        /// Scenario: Call method with invalid argument
        /// Expected: Exception
        /// </summary>
        [Test]
        public void _001_GetSetting_InvalidCode()
        {
            Assert.That(delegate { ConfigManager.GetSetting<int>(null); }, Throws.Exception.With.Message.EqualTo("configSettingCode is not specified"));
            Assert.That(delegate { ConfigManager.GetSetting<int>(string.Empty); }, Throws.Exception.With.Message.EqualTo("configSettingCode is not specified"));
            Assert.That(delegate { ConfigManager.GetSetting<int>("NONEXISTANT1"); }, Throws.Exception.With.Message.EqualTo("configSettingCode 'NONEXISTANT1' does not exist"));
        }

        /// <summary>
        /// Scenario: Call method with invalid data type for the setting being retrieved
        /// Expected: Exception
        /// </summary>
        [Test]
        public void _002_GetSetting_ValidCode_InvalidDataType()
        {
            Assert.That(delegate { ConfigManager.GetSetting<int>("CONFIG3"); }, Throws.Exception.With.Message.EqualTo("configSettingCode 'CONFIG3' cannot be cast as type 'System.Int32'"));
        }

        /// <summary>
        /// Scenario: Call method with valid argument and data type
        /// Expected: Appropriate value returned
        /// </summary>
        [Test]
        public void _003_GetSetting_ValidCode_ValidDataType()
        {
            int config1 = ConfigManager.GetSetting<int>("CONFIG1");
            Assert.That(config1, Is.EqualTo(10));

            bool config2 = ConfigManager.GetSetting<bool>("CONFIG2");
            Assert.That(config2, Is.True);

            string config3 = ConfigManager.GetSetting<string>("CONFIG3");
            Assert.That(config3, Is.EqualTo("myconfig"));
        }

        /// <summary>
        /// Scenario: Call method with override parameters where none exists
        /// Expected: Default value returned
        /// </summary>
        [Test]
        public void _004_GetSetting_Override_NoneExists()
        {
            string config3 = ConfigManager.GetSetting<string>("CONFIG3", "TEST1", 10);
            Assert.That(config3, Is.EqualTo("myconfig"));
        }

        /// <summary>
        /// Scenario: Call method with override parameters where value exists
        /// Expected: Override value returned
        /// </summary>
        [Test]
        public void _005_GetSetting_Override_Exists()
        {
            int config1 = ConfigManager.GetSetting<int>("CONFIG1", "ORT1", 2);
            Assert.That(config1, Is.EqualTo(20));
        }

        /// <summary>
        /// Scenario: Call method
        /// Expected: Returns correct set of settings
        /// </summary>
        [Test]
        public void _006_GetSettings()
        {
            DataTable dtMethod = ConfigManager.GetAllSettings();
            Assert.That(dtMethod.Rows.Count, Is.EqualTo(3));

            Assert.That(dtMethod.Rows[0]["ConfigSettingCode"], Is.EqualTo("CONFIG1"));
            Assert.That(dtMethod.Rows[0]["Value"], Is.EqualTo("10"));

            Assert.That(dtMethod.Rows[1]["ConfigSettingCode"], Is.EqualTo("CONFIG2"));
            Assert.That(Convert.ToString(dtMethod.Rows[1]["Value"]).ToLower(), Is.EqualTo("true"));

            Assert.That(dtMethod.Rows[2]["ConfigSettingCode"], Is.EqualTo("CONFIG3"));
            Assert.That(dtMethod.Rows[2]["Value"], Is.EqualTo("myconfig"));
        }

        /// <summary>
        /// Scenario: Call method with override parameters (no override records exist)
        /// Expected: Returns correct set of settings
        /// </summary>
        [Test]
        public void _007_GetSettings_Overrides_NoneFound()
        {
            DataTable dtMethod = ConfigManager.GetAllSettings("OVERRIDE_DOESNT_EXIST", 10);
            Assert.That(dtMethod.Rows.Count, Is.EqualTo(3));

            Assert.That(dtMethod.Rows[0]["ConfigSettingCode"], Is.EqualTo("CONFIG1"));
            Assert.That(dtMethod.Rows[0]["Value"], Is.EqualTo("10"));

            Assert.That(dtMethod.Rows[1]["ConfigSettingCode"], Is.EqualTo("CONFIG2"));
            Assert.That(Convert.ToString(dtMethod.Rows[1]["Value"]).ToLower(), Is.EqualTo("true"));

            Assert.That(dtMethod.Rows[2]["ConfigSettingCode"], Is.EqualTo("CONFIG3"));
            Assert.That(dtMethod.Rows[2]["Value"], Is.EqualTo("myconfig"));
        }

        /// <summary>
        /// Scenario: Call method with override parameters (override records exist)
        /// Expected: Returns correct set of settings
        /// </summary>
        [Test]
        public void _007_GetSettings_Overrides_Found()
        {
            DataTable dtMethod = ConfigManager.GetAllSettings("ORT1", 2);
            Assert.That(dtMethod.Rows.Count, Is.EqualTo(3));

            Assert.That(dtMethod.Rows[0]["ConfigSettingCode"], Is.EqualTo("CONFIG1"));
            Assert.That(dtMethod.Rows[0]["Value"], Is.EqualTo("20"));

            Assert.That(dtMethod.Rows[1]["ConfigSettingCode"], Is.EqualTo("CONFIG2"));
            Assert.That(Convert.ToString(dtMethod.Rows[1]["Value"]).ToLower(), Is.EqualTo("true"));

            Assert.That(dtMethod.Rows[2]["ConfigSettingCode"], Is.EqualTo("CONFIG3"));
            Assert.That(dtMethod.Rows[2]["Value"], Is.EqualTo("myconfig"));
        }

        /// <summary>
        /// Scenario: Call GetAppConfig to get a non Web Configuration
        /// Expected: Returns Configuration
        /// </summary>
        [Test]
        public void _008_GetAppConfig_NonWebConfiguration()
        {
            Configuration cfg = ConfigManager.GetAppConfig();
            Assert.That(cfg, Is.InstanceOf<Configuration>());
            Console.WriteLine(cfg.FilePath);
            Assert.That(cfg.FilePath.Contains("Codentia.Common.Config.Test"), Is.True);
        }

        /// <summary>
        /// Scenario: Call GetAppConfig to get a Web Configuration
        /// Expected: Returns Configuration
        /// </summary>
        [Test]
        public void _009_GetAppConfig_WebConfiguration()
        {
            HttpContext context = HttpHelper.CreateHttpContext("s@abc.com");
            HttpContext.Current = context;
            HttpRuntime runtime = new HttpRuntime();

            Configuration cfg = ConfigManager.GetAppConfig();

            // Note this has no effect in unit tests
            Assert.That(cfg, Is.InstanceOf<Configuration>());
            Assert.That(cfg.FilePath.Contains("web.config"), Is.True);
        }
    }
}
