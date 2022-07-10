
using Diceroller.interpreter.symbols.terminals;
using Diceroller.lib;
namespace Diceroller.interpreter.symbols
{
    internal class Diceroll : Symbol
    {
        
        
        //Modifier of this dice
        private Modifier? dice_modifier;
        private int modifier_value = 0;
        //Dice command with its range and quantity
        private DiceCommand dice_command;
        public Diceroll(DiceCommand dc, Modifier? dice_modifier = null)
        {
            dice_command = dc;
            this.dice_modifier = dice_modifier;
            if (dice_modifier != null) 
            {
                modifier_value = dice_modifier.getValue();
            }
        }


        //Delegate function
        private delegate bool CheckFunc(int dice_range, int threshold);
        
        //Takes a int and a operator char shows how much results, pass the threshhold
        public int RollAgainstThreshhold(int threshold, Comparator p)
        {
            //Delegates check function function according to operator
            CheckFunc func;
            switch (p.getValue())
            {
                case (int)ComparatorType.e:
                    func = IsEqual;
                    break;
                case (int)ComparatorType.lt:
                    func = Islt;
                    break;
                case (int)ComparatorType.lte:
                    func = Islte;
                    break;
                case (int)ComparatorType.gt:
                    func = Isgt;
                    break;
                case (int)ComparatorType.gte:
                    func = Isgte;
                    break;
                default:
                    func = IsEqual;
                    break;


            }
            int sucesses = 0; //Sum of the sucesses
            //Getting the quantity and range values
            int quantity = dice_command.getNumber();
            int range = dice_command.getRange();
            for (int i = 0; i < quantity ; i++)
            {
                if (func(RollDice.rollDice(range)+modifier_value,threshold))
                { 
                    sucesses++;
                }
            }
            return sucesses;
        }
        //The checks of the threshhold
        private bool IsEqual(int diceroll, int threshold)
        {
            return diceroll== threshold;
        }

        private bool Isgt(int diceroll, int threshold) 
        {
            return diceroll > threshold;
        }
        private bool Isgte(int diceroll, int threshold)
        {
            return diceroll >= threshold;
        }
        private bool Islt(int diceroll, int threshold)
        {
            return diceroll < threshold;
        }
        private bool Islte(int diceroll, int threshold)
        {
            return diceroll <= threshold;
        }
        //Returns the sum of a normal roll of these dice
        public int normalRoll() 
        {
            int sum = 0;
            //Gets the quantity and range values
            int quantity = dice_command.getNumber();
            int range = dice_command.getRange();
            for (int i = 0; i < quantity; i++) 
            {
                sum = sum + RollDice.rollDice(range);
            }
            return sum + modifier_value;
        }

        public override int getAction(int t)
        {
            switch (t)
            {
                case 0:
                case 7:
                case 8:
                case 11:
                    return 5;

                default:
                    throw new Exception("Invalid action exception");
            }
        }
    }
}
