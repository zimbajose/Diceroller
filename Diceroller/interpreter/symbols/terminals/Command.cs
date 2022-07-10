
namespace Diceroller.interpreter.symbols.terminals
{
    //Is terminal
    internal class Command : Symbol
    {
        
        //Returns the command type, since the program only has the dice command, it will always return 0.
        public int getType() 
        {
            return 0;
        }

    }
}
