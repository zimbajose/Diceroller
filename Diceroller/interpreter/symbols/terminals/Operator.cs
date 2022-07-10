

namespace Diceroller.interpreter.symbols.terminals
{
    internal enum OperatorType
    { 
        minus =0,
        plus =1
    }
    //Is terminal
    internal class Operator : Symbol
    {
        //This enum's value
        private OperatorType operator_type;
        public Operator(OperatorType p) 
        { 
            this.operator_type = p;
        }
        //Returns the type of the operator
        public int getValue() 
        {
            return (int)operator_type;
        }
       
        
    }
}
