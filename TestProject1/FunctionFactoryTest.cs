﻿using BoundaryElementsMetod;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoundaryElementsMetod.Common.Points;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for FunctionFactoryTest and is intended
    ///to contain all FunctionFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FunctionFactoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for K
        ///</summary>
        [TestMethod()]
        public void KTest()
        {
            Point3D x = null; // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = FunctionFactory.K(x);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
