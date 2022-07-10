

namespace Diceroller.lib
{
    public static class RollDice
    {
        //Random class
        private static Random roller = new Random();


        public static int rollDice(int range) 
        {
            if (range <= 0) 
            {
                throw new Exception("Invalid dice range exception");
            }
            return roller.Next(1, range);
        }

    
    }
}
