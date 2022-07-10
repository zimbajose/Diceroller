using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diceroller.interpreter
{
    internal abstract class Symbol
    {

        //Takes int for state and returns the rule index of what should be done when said token is next, returns -1 if terminal
        public virtual int getAction(int t) 
        {
            return -1;
        }

        

    }



}
