using System;
using CapstoneProject.Database;
using CapstoneProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;

namespace CapstoneProject.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckPhoneTest()
        {

            //Arrange       
            AcademicTrackerRepository academicTrackerRepository = new AcademicTrackerRepository();
            string testNum = "123-4567";
            
            

            // Act
            academicTrackerRepository.CheckPhone(testNum);
            

            //Assert
            Assert.IsTrue(academicTrackerRepository.isPhoneNum == true, "The string did not match phone number format");

        }
    }
}
