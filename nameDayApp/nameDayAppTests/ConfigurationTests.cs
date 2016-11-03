using Microsoft.VisualStudio.TestTools.UnitTesting;
using nameDayApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace nameDayApp.Tests
{
    [TestClass()]
    public class ConfigurationTests
    {
        [TestMethod()]
        // Unit test for config reading
        public void ConfigurationTest()
        {
            string value = ConfigurationManager.AppSettings["testFileName"];
            Assert.IsFalse(String.IsNullOrEmpty(value), "No App.Config found.");

        }

        [TestMethod()]
        // Unit test for getFileName method's return values
        public void FileNameNotEmptyTest()
        {
            MyConfiguration conf = new MyConfiguration();
            string value = conf.getFileName();
            Assert.IsFalse(String.IsNullOrEmpty(value), "filename empty.");

         
        }


        [TestMethod()]
        public void FileNamecomparisonTest()
        {
            MyConfiguration conf = new MyConfiguration();
            string value = conf.getFileName();
            string configValue = ConfigurationManager.AppSettings["filename"];
            Assert.AreEqual( value,configValue);


        }
    }


}