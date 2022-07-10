

namespace Diceroller.interpreter.symbols
{
    
    internal class DiceCommand : Symbol
    {
        private int dice_range;
        private int dice_number;

        public DiceCommand(int number, int range) 
        { 
            this.dice_number = number;
            this.dice_range = range;
        }


        public override int getAction(int t)
        {
            switch (t)
            {
                case 0:
                case 7:
                case 8:
                case 11:
                    return 6;


                default:
                    throw new Exception("Invalid action exception");
            }
        }
        public int getRange() 
        {
            return dice_range;
        }
        public int getNumber() 
        {
            return dice_number;
        }

    }
}
