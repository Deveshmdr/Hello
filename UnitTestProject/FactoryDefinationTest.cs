using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class FactoryDefinitionTest
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
