using BEM;
using BEM.Common;
using BEM.Common.Points;

using System;

using BEM.Factory;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BEMTest
{
    
    
    /// <summary>
    ///This is a test class for BoundTest and is intended
    ///to contain all BoundTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BoundTest
    {
        private const double Eps = 0.0000000000000001;

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
        ///A test for Q
        ///</summary>
        [TestMethod()]
        public void QTest()
        {
            Point3D x = new Point3D(1, 1, 1); // TODO: Initialize to an appropriate value
            Point3D ksi = new Point3D(0, 0, 0); // TODO: Initialize to an appropriate value
            double expected = 1 / (4 * Math.PI * Math.Sqrt(3)); // TODO: Initialize to an appropriate value
            double actual;
            actual = FunctionFactory.Q(x, ksi);
            Assert.IsTrue(Math.Abs(expected - actual) < Eps);
        }
    }
}
