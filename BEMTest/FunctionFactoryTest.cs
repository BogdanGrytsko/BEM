using System.Diagnostics;

using BEM;
using BEM.Bounds;
using BEM.Factory;
using BEM.InnerSource;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BEM.Common.Points;

namespace BEMTest
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
        ///A test for Q1
        ///</summary>
        [TestMethod()]
        public void Q1Test()
        {
            Point3D x = new Point3D (1,1,1); 
            Point3D ksi = new Point3D (0,0,0); 
            double expected = -1 / (4 * Math.PI * Math.Sqrt(3) * Math.Sqrt(3) * Math.Sqrt(3)); 
            double actual;
            actual = FunctionFactory.Q1(x, ksi);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for InnnerSource
        ///</summary>
        [TestMethod()]
        public void InnnerSourceTest()
        {
            Point3D x = new Point3D(0,0.25,0);
            double expected = 4; 
            double actual;
            actual = InnerSourcePlate.SourceFunction(x);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for K
        ///</summary>
        [TestMethod()]
        public void KTest()
        {
            ////Point3D x = new Point3D(1, 1, 1);
            ////double expected = 3; 
            ////double actual;
            ////actual = FunctionFactory.KuExp(x);
            ////Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FunctionsAreContiniousTest()
        {
            var bound = new ParallelepipedNearBound();
            foreach (var point in bound.CornerPoints)
            {
                var x = FunctionFactory.G(point);
                var y = FunctionFactory.Gother(point);
                Assert.AreEqual(x,y);
            }

        }
    }
}
