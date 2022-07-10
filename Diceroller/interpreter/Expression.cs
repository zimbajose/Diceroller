using Diceroller.interpreter.symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diceroller.interpreter
{
    public interface Expression
    {
       
        //Returns the final result of the expression
        public int getResult();

    }


    

}
