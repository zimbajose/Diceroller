# Diceroller
A prototype for a basic interepreter , that can roll dice according to a simple sent expression.
## How it works
the user may type a expression for a dice roll, it can be either a simple dice roll where it will return the sum of all the sent dice and values, or a comparative roll where it will compare all the sent dice and values to another number. <br>
The values accepted for a dice roll or static value are defined by these rules:

- A dice roll is represented by xdy, where x is the amount of dice to be rolled, and y is the range of the dice. the expression 2d4 would return the sum of two numbers between 2 and 4, you can only use integers for dice rolls.
- A dice roll may also contain a modifier, for now it only supports numbers and not other dice as a modifier, a modifier is added by adding a + or - operator and another number, it will sum or subtract that value based on the number.
- You may also compare a dice roll to another dice roll or value by using >,=,<,<=,>= such expression will not return the sum of dice, but the amound of dice on the left side that are satisfy the operator condition. 

### Example expressions
- 1d9+4
- 7d5-2
- 1d8<3
- 5d6>4
- 2d6+1>2

## Installation
The project does not use any external libraries, simply loading the project in visual studio should be enough.

## Known issues
- The lex analyser has a problem analasying multiple digits numbers.
- The grammar is not able to handle multiple dice rolls, there needs to be added additional rules.
