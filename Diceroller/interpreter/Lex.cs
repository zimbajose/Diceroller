using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diceroller.interpreter
{
    //List of all the lex symbols
    internal static class Lex
    {
        internal static char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        internal static char[] commands = {'d','D'};
        internal static char[] operators = { '+', '-' };
        internal static char[] comparators = {'>','<','='};
    }
}
