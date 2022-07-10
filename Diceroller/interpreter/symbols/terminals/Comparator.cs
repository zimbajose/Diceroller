

namespace Diceroller.interpreter.symbols.terminals
{
    public enum ComparatorType 
    { 
        e =0,
        gt = 1,
        gte = 2,
        lt = 3,
        lte = 4,
    }
    internal class Comparator : Symbol
    {
        private ComparatorType comarator_type;
       

        public Comparator(int type) 
        {
            comarator_type = (ComparatorType)type;
        }

        //Returns the type this is
        public int getValue() 
        {
            return (int)comarator_type;
        }
    }
}
