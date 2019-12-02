using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class ShapeTypeTest
    {
        [TestMethod]
        public void isCircle_stringCircle_returnTrue()
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
