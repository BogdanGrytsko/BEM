using BEM.BoundaryElements;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BEMTest
{
    
    
    /// <summary>
    ///This is a test class for IntegratorTest and is intended
    ///to contain all IntegratorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IntegratorTest
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
        ///A test for Integrate
        ///</summary>
        [TestMethod()]
        public void IntegrateTest()
        {
            double a = 5;
            int n = 2;
            var target = new Integrator<Point3D>(n);
            BoundaryElement2DFirstOrder elem = new BoundaryElement2DFirstOrder(
                new Point3D(),
                new Point3D(0, 0, a),
                new Point3D(0, a, a),
                new Point3D(0, a, 0),
                new Point3D(0, a / 2, a / 2),
                null);
            Point3D eta = new Point3D();
            Func<Point3D, Point3D, double> f = (p1, p2) => 1;
            double expected = a * a;
            double actual;
            actual = target.Integrate(elem, eta, f);
            Assert.AreEqual(expected, actual);
        }
    }
}
