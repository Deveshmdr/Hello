using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApp1;

namespace UnitTestP
{
    [TestClass]
    public class FactoryDefinationTest
    {
        [TestMethod]
        public void isShape()
        {
            //assign
            bool result;
            String type;
            FactoryProducerDefination fp = new FactoryProducerDefination();

            //act
            type = "shape";
            result = fp.isShape(type);

            //assert
            Assert.IsTrue(result);
        }
    }
}
