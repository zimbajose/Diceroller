using Diceroller.interpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace InterepreterTests
{
    [TestClass]
    public class InterpreterTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Interpreter.interpret("1d4");
        }
    }
}