using Diceroller.interpreter.symbols;
using Diceroller.interpreter.symbols.terminals;


namespace Diceroller.interpreter
{


    internal class ComparativeRoll : Symbol,Expression
    {
        Comparator c;// Comparator type
        SimpleRollFragment left_rolls;//Rolls before the comparator
        SimpleRollFragment right_rolls; //Rollf after the comparator
        public ComparativeRoll(SimpleRollFragment left_rolls, SimpleRollFragment right_rolls, Comparator c) 
        { 
            this.left_rolls = left_rolls;
            this.right_rolls = right_rolls;
            this.c = c;
        }
        public int getResult()
        {
            return left_rolls.rollCompare(right_rolls.rollSimple(), c);
        }

        public override int getAction(int t)
        {
            switch (t) 
            {
                case 0:
                    return 1;
                default:
                    throw new Exception("Invalid action exception");
            }
        }

    }
}

