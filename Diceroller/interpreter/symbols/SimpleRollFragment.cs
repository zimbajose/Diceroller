using Diceroller.interpreter.symbols.terminals;


namespace Diceroller.interpreter.symbols
{
    internal class SimpleRollFragment : Symbol
    {
        //The address of this roll fragment's value
        private Value current_value;
        //The address of the previous rollfragment
        private SimpleRollFragment? previous = null;
        //The operator
        private Operator? op = null;
        public SimpleRollFragment(Value current,SimpleRollFragment previous, Operator op) 
        {
            //Checks for null
            if (previous == null || op==null) 
            {
                throw new Exception("Values may not be null when using this constructor");
            }
            this.current_value = current;
            this.previous = previous;
            this.op = op;
        }
        //Constructor for when it is the first one to be derivated
        public SimpleRollFragment(Value current) 
        {
            this.current_value = current;
        }


        
        //Rolls all the dice on simple mode
        public int rollSimple(int sum =0) 
        {
            if (previous == null) 
            { 
                return sum + current_value.getValue();
            }
            if (op.getValue() == (int)OperatorType.plus)
            {
                sum += current_value.getValue();
            }
            else
            {
                sum -= current_value.getValue();
            }
            return previous.rollSimple(sum);
           
        }
        //Rolls all the dice on comparator mode
        public int rollCompare(int threshold, Comparator c, int sum =0) 
        {
            if (previous == null) 
            {
                return sum + current_value.getValueComparate(threshold, c);
            }
            if (op.getValue() == (int)OperatorType.plus)
            {
                sum += current_value.getValueComparate(threshold, c);
            }
            else 
            {
                sum -= current_value.getValueComparate(threshold, c);
            }

            return this.rollCompare(threshold, c,sum);
        }

        public override int getAction(int t)
        {
            switch (t)
            {
                case 0:
                    return 2;
                case 7:
                    return 12;
                case 8:
                case 11:
                    return 13;
                default:
                    throw new Exception("Invalid action exception");
            }
        }
    }
}
