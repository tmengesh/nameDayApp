using Microsoft.VisualStudio.TestTools.UnitTesting;
using nameDayApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nameDayApp.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        // Unit test for valid name and date input
        public void validateValidInput()
        {
                {
                    bool result = Program.validateInput("20", "6");
                    Assert.IsTrue(result, "input not valid"); 
                }    
        }

        [TestMethod()]
        // Unit test for wrong month and date values
        public void validateWrongInput()
        {
            {
                bool result = Program.validateInput("A", "6B");
                Assert.IsFalse(result, "Wrong input format");
            }
        }

        [TestMethod()]
        // Unit test for wrong date value
        public void validateWrongDate()
        {
            {
                bool result = Program.validateInput("35", "6");
                Assert.IsFalse(result, "Wrong date input");
            }
        }

        [TestMethod()]
        // Unit test for wrong month value
        public void validateWrongMonth()
        {
            {
                bool result = Program.validateInput("3", "66");
                Assert.IsFalse(result, "Wrong month input");
            }
        }
    }
}