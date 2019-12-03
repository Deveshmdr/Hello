using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApp1;

namespace UnitTestP
{
    [TestClass]
    public class ShapeTypeTest
    {
        [TestMethod]
        public void IsCircle_stringCircle_returnTrue()
        {
            //arrange
            bool result;
            ShapeFactoryDefination sd = new ShapeFactoryDefination();

            //act
            result = sd.isCircle("circle");

            //assert
            Assert.IsTrue(result);
        }
        public void isRectangle_stringRectangle_returnTrue()
        {
            //arrange
            bool result;
            ShapeFactoryDefination sd = new ShapeFactoryDefination();

            //act
            result = sd.isCircle("rectangle");

            //assert
            Assert.IsTrue(result);
        }
    }
}
