using Diceroller.interpreter.symbols.terminals;


namespace Diceroller.interpreter.symbols
{
    
    internal class Modifier : Symbol
    {
        //The operator of this modifier
        Operator? modifier_operator;
        //The value of  the modifier
        private int modifier_value =0;
        public Modifier(Operator p,int value) 
        {
            this.modifier_operator = p;
            modifier_value = value;
        }
        public Modifier() 
        { 
            
        }
        //Returns the value of the modifier
        public int getValue() 
        {
            if (modifier_operator == null) 
            {
                return 0;
            }
            return modifier_operator.getValue() == (int)OperatorType.plus? 
            modifier_value :
            -modifier_value;
        }

        public override int getAction(int t)
        {
            switch (t)
            {
                case 6:
                    return 10;
                default:
                    throw new Exception("Invalid action exception");
            }
        }
    }

}
