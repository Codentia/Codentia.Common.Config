using System;
using System.Data;
using NUnit.Framework;

namespace Codentia.Common.Config.Test
{
    /// <summary>
    /// Unit testing framework for TemplateManager
    /// </summary>
    [TestFixture]
    public class TemplateManagerTest
    {
        /// <summary>
        /// Prepare for tests to execute
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // check initial value - if wrong, fail as something bad has happened
            Assert.That(TemplateManager.DataSourceName, Is.EqualTo("config"));

            // now set to test value
            TemplateManager.DataSourceName = "config_test";
            Assert.That(TemplateManager.DataSourceName, Is.EqualTo("config_test"));
        }

        /// <summary>
        /// Scenario: Call method with invalid argument
        /// Expected: Exception
        /// </summary>
        [Test]
        public void _001_GetString_InvalidCode()
        {
            Assert.That(delegate { TemplateManager.GetString(null); }, Throws.Exception.With.Message.EqualTo("stringTemplateCode is not specified"));
            Assert.That(delegate { TemplateManager.GetString(string.Empty); }, Throws.Exception.With.Message.EqualTo("stringTemplateCode is not specified"));
            Assert.That(delegate { TemplateManager.GetString("NONEXISTANT1"); }, Throws.Exception.With.Message.EqualTo("stringTemplateCode 'NONEXISTANT1' does not exist"));
        }

        /// <summary>
        /// Scenario: Call method with valid argument and data type
        /// Expected: Appropriate value returned
        /// </summary>
        [Test]
        public void _003_GetString_ValidCode_ValidDataType()
        {
            string config1 = TemplateManager.GetString("ST1");
            Assert.That(config1, Is.EqualTo("10"));

            string config2 = TemplateManager.GetString("ST2");
            Assert.That(config2, Is.EqualTo("true"));

            string config3 = TemplateManager.GetString("ST3");
            Assert.That(config3, Is.EqualTo("myconfig"));
        }

        /// <summary>
        /// Scenario: Call method with override parameters where none exists
        /// Expected: Default value returned
        /// </summary>
        [Test]
        public void _004_GetString_Override_NoneExists()
        {
            string config3 = TemplateManager.GetString("ST3", "TEST1", 10);
            Assert.That(config3, Is.EqualTo("myconfig"));
        }

        /// <summary>
        /// Scenario: Call method with override parameters where value exists
        /// Expected: Override value returned
        /// </summary>
        [Test]
        public void _005_GetString_Override_Exists()
        {
            string config1 = TemplateManager.GetString("ST1", "ORT1", 2);
            Assert.That(config1, Is.EqualTo("20"));
        }

        /// <summary>
        /// Scenario: Call method
        /// Expected: Returns correct set of settings
        /// </summary>
        [Test]
        public void _006_GetStrings()
        {
            DataTable dtMethod = TemplateManager.GetAllStrings();
            Assert.That(dtMethod.Rows.Count, Is.EqualTo(3));

            Assert.That(dtMethod.Rows[0]["StringTemplateCode"], Is.EqualTo("ST1"));
            Assert.That(dtMethod.Rows[0]["Value"], Is.EqualTo("10"));

            Assert.That(dtMethod.Rows[1]["StringTemplateCode"], Is.EqualTo("ST2"));
            Assert.That(Convert.ToString(dtMethod.Rows[1]["Value"]).ToLower(), Is.EqualTo("true"));

            Assert.That(dtMethod.Rows[2]["StringTemplateCode"], Is.EqualTo("ST3"));
            Assert.That(dtMethod.Rows[2]["Value"], Is.EqualTo("myconfig"));
        }

        /// <summary>
        /// Scenario: Call method with override parameters (no override records exist)
        /// Expected: Returns correct set of settings
        /// </summary>
        [Test]
        public void _007_GetStrings_Overrides_NoneFound()
        {
            DataTable dtMethod = TemplateManager.GetAllStrings("OVERRIDE_DOESNT_EXIST", 10);
            Assert.That(dtMethod.Rows.Count, Is.EqualTo(3));

            Assert.That(dtMethod.Rows[0]["StringTemplateCode"], Is.EqualTo("ST1"));
            Assert.That(dtMethod.Rows[0]["Value"], Is.EqualTo("10"));

            Assert.That(dtMethod.Rows[1]["StringTemplateCode"], Is.EqualTo("ST2"));
            Assert.That(Convert.ToString(dtMethod.Rows[1]["Value"]).ToLower(), Is.EqualTo("true"));

            Assert.That(dtMethod.Rows[2]["StringTemplateCode"], Is.EqualTo("ST3"));
            Assert.That(dtMethod.Rows[2]["Value"], Is.EqualTo("myconfig"));
        }

        /// <summary>
        /// Scenario: Call method with override parameters (override records exist)
        /// Expected: Returns correct set of settings
        /// </summary>
        [Test]
        public void _007_GetStrings_Overrides_Found()
        {
            DataTable dtMethod = TemplateManager.GetAllStrings("ORT1", 2);
            Assert.That(dtMethod.Rows.Count, Is.EqualTo(3));

            Assert.That(dtMethod.Rows[0]["StringTemplateCode"], Is.EqualTo("ST1"));
            Assert.That(dtMethod.Rows[0]["Value"], Is.EqualTo("20"));

            Assert.That(dtMethod.Rows[1]["StringTemplateCode"], Is.EqualTo("ST2"));
            Assert.That(Convert.ToString(dtMethod.Rows[1]["Value"]).ToLower(), Is.EqualTo("true"));

            Assert.That(dtMethod.Rows[2]["StringTemplateCode"], Is.EqualTo("ST3"));
            Assert.That(dtMethod.Rows[2]["Value"], Is.EqualTo("myconfig"));
        }
    }
}
