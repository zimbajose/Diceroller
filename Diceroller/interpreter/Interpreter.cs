
using Diceroller.interpreter.symbols;
using Diceroller.interpreter.symbols.terminals;
using Diceroller.lib;

namespace Diceroller.interpreter
{
    public static class Interpreter
    {
        //Current position in the expression string
        private static int expression_index =0;

        //Current expression being compiled
        private static String expression = "";

        //Interpretor variables
        private static SimpleRollFragment left_rolls;
        private static SimpleRollFragment right_rolls;
        private static bool save_to_right;

        //Final Expression
        private static Expression roll_expression;
        //Heaps
        private static Heap<int> state_heap;
        private static Heap<Symbol> symbol_heap;
        //Next symbol
        private static Symbol next;
        //Obtains the next token in the list
        
        private static Symbol getNextToken() 
        {
            //The current token state
            int state = 0;
            String current_token_str = "";
            loop: while (true) 
            {
                //If it is the last element
                if (expression.Length < expression_index + 1)
                {
                    return new EndToken();
                }
                //Ignores white spaces
                if (expression[expression_index] == ' ') 
                {
                    expression_index++;
                    continue;
                }
               
                switch (state) 
                {
                    
                    //Starting state
                    case 0:                     
                        //If it is a number
                        if (Lex.numbers.Contains(expression[expression_index])) 
                        { 
                            current_token_str = current_token_str + expression[expression_index];
                            if (expression.Length == expression_index + 1)
                            {
                                expression_index++;
                                return new Number(current_token_str);
                            }
                            state = 1;
                            break;
                        }
                        //If it is a command
                        if (Lex.commands.Contains(expression[expression_index]))
                        {
                            expression_index++;
                            return new Command(); // No need to pass its type since there is only one type of command
                        }
                        //If it is a operator
                        if (Lex.operators.Contains(expression[expression_index]))
                        {
                            OperatorType p;
                            switch (expression[expression_index]) 
                            {
                                case '-':
                                    p = OperatorType.minus;
                                    break;
                                case '+':
                                    p = OperatorType.plus;
                                    break;
                                default:
                                    throw new Exception("Invalid lex exception");

                            }
                            expression_index++;
                            return new Operator(p);

                        }
                        //If it is a comparator
                        if (Lex.comparators.Contains(expression[expression_index])) 
                        {
                            current_token_str = current_token_str + expression[expression_index];
                            state = 2;
                            break;
                        }
                        throw new Exception("Invalid lex exception");
                    //Keeps parsing until it find the entire number
                    case 1:
                        if (Lex.numbers.Contains(expression[expression_index])) 
                        {
                            current_token_str = current_token_str + expression[expression_index];
                            state = 1;
                            break;
                        }
                        return new Number(current_token_str);
                    case 2:
                        if (Lex.comparators.Contains(expression[expression_index])) 
                        {
                            current_token_str = current_token_str + expression[expression_index];
                           
                        }
                       
                        switch (current_token_str)
                        {
                            case ">=":
                                return new Comparator((int)ComparatorType.gte);
                            case ">":
                                return new Comparator((int)ComparatorType.gt);
                            case "<":
                                return new Comparator((int)ComparatorType.lt);
                            case "<=":
                                return new Comparator((int)ComparatorType.lte);
                            case "=":
                                return new Comparator((int)ComparatorType.e);
                            default:
                                throw new Exception("Invalid token sequence exception");
                        }

                    default:
                        throw new Exception("Invalid token sequence exception");
                }
                expression_index++;

            }
           
        }


        //Returns the result of a expression string who was sent
        public static Expression interpret(String expression) 
        {
            //Sets values
            expression_index = 0;
            Interpreter.expression = expression;
            state_heap = new Heap<int>();
            symbol_heap = new Heap<Symbol>();
            left_rolls = null;
            right_rolls = null;
            save_to_right = false;
            next = getNextToken();

            //Pushes state 0
            state_heap.push(0);
            
           
            //My main loop
            while (true) 
            {
                //Checks if top of the heap is accept
                if (symbol_heap.size()>0) 
                {
                    if (symbol_heap.getFirst() is Accept)
                    {
                        return accept();
                    }
                }

                //Checks if the heap is empty
                if (symbol_heap.size() > 0)
                {
                    if (symbol_heap.size() == state_heap.size())
                    {
                        //Gets the action
                        int action = symbol_heap.getFirst().getAction(state_heap.getFirst());
                        //Checks if terminal                    
                        if (action != -1)
                        {
                            goTo(action);
                        }
                    }
                }
                
                Interpreter.action();
                
            }
           
        }
        
        private static void action() 
        {
            int state = state_heap.getFirst();
            
            //Goes through the table
            switch (state) 
            {
                case 0:
                    if (next is Number)
                    {
                        shift(4, next);
                    }
                    else {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 1:
                    if (next is EndToken)
                    {
                        symbol_heap.push(new Accept());
                    }
                    else 
                    {
                        throw new Exception("Invalid syntax exception, spare elements");
                    }
                    break;
                case 2:
                    if (next is Comparator) 
                    {
                        shift(7, next);
                    }
                    else if (next is Operator) 
                    {
                        shift(8, next);
                    }
                    else if (next is EndToken) 
                    {
                        reduce(3);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 3:
                    if (next is Comparator || next is Operator || next is EndToken) 
                    {
                        reduce(5);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 4:
                    if (next is Comparator || next is Operator || next is EndToken)
                    {
                        reduce(6);
                    }
                    else if (next is Command) 
                    {
                        shift(9, next);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 5:
                    if (next is Comparator || next is Operator || next is EndToken) 
                    {
                        reduce(7);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 6:
                    if (next is Comparator || next is EndToken)
                    {
                        reduce(10);
                    }
                    else if (next is Operator) 
                    {
                        shift(11,next);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 7:
                    if (next is Number) 
                    {
                        shift(4, next);
                        save_to_right = true;//Sets the savetoright flag
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 8:
                    if (next is Number) 
                    {
                        shift(4, next);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 9:
                    if (next is Number) 
                    {
                        shift(14, next);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 10:
                    if (next is Comparator || next is Operator || next is EndToken) 
                    {
                        reduce(8);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 11:
                    if (next is Number) 
                    {
                        shift(15, next);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 12:
                    if (next is Operator)
                    {
                        shift(8, next);
                    }
                    else if (next is EndToken) 
                    {
                        reduce(2);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 13:
                    if (next is Comparator || next is EndToken)
                    {
                        reduce(4);
                    }
                    else if (next is Operator) 
                    {
                        shift(8, next);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 14:
                    if (next is Comparator || next is Operator || next is EndToken) 
                    {
                        reduce(9);
                    } 
                    else
                    {
                        throw new Exception("Invaliz syntax exception at "+ state);
                    }
                    break;
                case 15:
                    if (next is Comparator || next is Operator || next is EndToken)
                    {
                        reduce(11);
                    }
                    else if (next is Command) 
                    {
                        shift(9, next);
                    }
                    else
                    {
                        throw new Exception("Invaliz syntax exception at " + state);
                    }
                    break;
                default:
                    throw new Exception("Invalid heap state exception");
            }
        }
       
        private static void goTo(int state) 
        {
            state_heap.push(state);
        }
        private static void shift(int state, Symbol s) 
        {
            state_heap.push(state);
            symbol_heap.push(s);
            next = getNextToken();
        }
       
        private static void reduce(int rule) 
        {
            //If it is a rule that derives by nothing, wont pop
            Symbol top;
            if (rule == 10)
            {
               top = symbol_heap.getFirst();
                
            }
            else
            {
                top = symbol_heap.pop();
                state_heap.pop();
            }
            switch (rule) 
            {
                case 2:
                    if (top is SimpleRollFragment)
                    {
                        SimpleRollFragment sr1 = (SimpleRollFragment)top;
                        top = symbol_heap.pop();
                        state_heap.pop();
                        if (top is Comparator)
                        {
                            Comparator c = (Comparator)top;
                            top = symbol_heap.pop();
                            state_heap.pop();
                            if (top is SimpleRollFragment)
                            {
                                SimpleRollFragment sr2 = (SimpleRollFragment)top;
                                roll_expression = new ComparativeRoll(left_rolls,right_rolls, c);
                                symbol_heap.push((Symbol)roll_expression);
                                break;
                            }
                        }
                    }
                    throw new Exception("Invalid reduce Exception at rule" + rule);
                case 3:
                    if (top is SimpleRollFragment) 
                    {
                        SimpleRoll sr = new SimpleRoll((SimpleRollFragment)top);
                        roll_expression = sr;
                        symbol_heap.push(sr);
                        break;
                    }
                    throw new Exception("Invalid reduce Exception at rule" + rule);
                case 4:
                    if (top is SimpleRollFragment)
                    {
                        SimpleRollFragment sr1 = (SimpleRollFragment)top;
                        top = symbol_heap.pop();
                        state_heap.pop();
                        if (top is Operator)
                        {
                            Operator o = (Operator)top;
                            top = symbol_heap.pop();
                            state_heap.pop();

                            if (top is Value)
                            {
                                Value v = (Value)top;
                                sr1 = new SimpleRollFragment(v, sr1, o);
                                if (save_to_right)
                                {
                                    left_rolls = sr1;
                                }
                                else 
                                {
                                    right_rolls = sr1;
                                }
                                symbol_heap.push(sr1);
                                break;
                            }
                        }
                    }
                    throw new Exception("Invalid reduce Exception at rule" + rule);
                case 5:
                    if (top is Value) 
                    {
                        SimpleRollFragment sr1 = new SimpleRollFragment((Value)top);
                        if (Interpreter.save_to_right)
                        {
                            right_rolls = sr1;
                        }
                        else
                        {
                            left_rolls = sr1;
                        }
                        symbol_heap.push(sr1);
                        break;
                    }
                    throw new Exception("Invalid reduce Exception at rule" + rule);
                case 6:
                    if (top is Number) 
                    {
                        Number n  = (Number)top;
                        symbol_heap.push(new Value(n.getValue()));
                        break;
                    }
                    throw new Exception("Invalid reduce Exception at rule" + rule);
                case 7:
                    if (top is Diceroll)
                    {
                        Diceroll dr = (Diceroll)top;
                        Value v = new Value(dr);
                        symbol_heap.push(v);
                        break;
                    }
                    throw new Exception("Invalid reduce Exception at rule" + rule);
                case 8:
                    if (top is Modifier)
                    {
                        Modifier m = (Modifier)top;
                        top = symbol_heap.pop();
                        state_heap.pop();
                        if (top is DiceCommand) 
                        {
                            DiceCommand dc = (DiceCommand)top;
                            symbol_heap.push(new Diceroll(dc,m));
                            break;
                        }
                    }
                    throw new Exception("Invalid reduce Exception at rule" + rule);
                case 9:
                    if (top is Number) 
                    {
                        Number n1 = (Number)top;
                        top = symbol_heap.pop();
                        state_heap.pop();
                        if (top is Command) 
                        {
                            top = symbol_heap.pop();
                            state_heap.pop();
                            if (top is Number) 
                            {
                                Number n2 = (Number)top;
                                symbol_heap.push(new DiceCommand(n2.getValue(),n1.getValue()));
                                break;
                            }
                        }
                    }
                    throw new Exception("Invalid reduce Exception at rule" + rule);
                case 10:
                    symbol_heap.push(new Modifier());
                    break;
                case 11:
                    if (top is Number) 
                    {
                        Number n = (Number)top;
                       
                        top = symbol_heap.pop();
                        state_heap.pop();
                        if (top is Operator) 
                        {
                            Operator o = (Operator)top;
                            symbol_heap.push(new Modifier(o, n.getValue()));
                            break;
                        }
                    }
                    throw new Exception("Invalid reduce Exception at rule" + rule);
                default:
                    throw new Exception("Invalid rule Exception");
            }
        }
        //The function that end and returns the fial value
        private static Expression accept() 
        {
            return roll_expression;
        }
    }
}
