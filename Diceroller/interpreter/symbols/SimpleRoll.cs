using Diceroller.interpreter.symbols;


namespace Diceroller.interpreter {
    internal class SimpleRoll : Symbol,Expression
    {
        //The first fragment of the list
        SimpleRollFragment fragment;
        public SimpleRoll(SimpleRollFragment srf) 
        {
            fragment = srf;
        }
        public int getResult()
        {
            return fragment.rollSimple();
        }
        public override int getAction(int t)
        {
            switch (t)
            {
                case 0:
                case 11:
                    return 1;
                default:
                    throw new Exception("Invalid action exception");
            }
        }
    }
}
