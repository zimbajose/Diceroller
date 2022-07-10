using System;
using System.Collections.Generic;


namespace Diceroller.interpreter.symbols.terminals
{
    //Is terminal
    internal class Number : Symbol
    {
        //The value of the number
        private int value;
        
        public Number(String number) 
        {
            this.value = Int32.Parse(number);
        }

        //Returns the value as a number
        public int getValue()
        {
            return this.value;
        }

        
    
    }
}
