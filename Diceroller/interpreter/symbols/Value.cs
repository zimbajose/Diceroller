using Diceroller.interpreter.symbols.terminals;


namespace Diceroller.interpreter.symbols
{
    
    internal class Value : Symbol
    {
        private int raw_value;
        private Diceroll? dice_roll;
        //When it is derivated from a number
        public Value(int raw_value) 
        { 
            this.raw_value = raw_value;
        }
        //When it is derivated from a diceroll
        public Value(Diceroll dice_roll) 
        { 
            this.dice_roll = dice_roll;
        }
        
        //Value when it is a simple roll
        public int getValue()
        {
            if (dice_roll == null)
            {
                return (int)raw_value;
            }
            return dice_roll.normalRoll();
        }
        //Value when it is a comparison roll
        public int getValueComparate(int threshhold, Comparator c)
        {
            if (dice_roll == null) 
            {
                return (int)raw_value;
            }
            return dice_roll.RollAgainstThreshhold(threshhold,c);
        }

        public override int getAction(int t)
        {
            switch (t)
            {
                case 0:
                case 7:
                case 8:
                    return 3;
                default:
                    throw new Exception("Invalid action exception");
            }
        }

    }


    
}
