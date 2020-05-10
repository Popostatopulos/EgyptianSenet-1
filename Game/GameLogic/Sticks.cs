using System;

namespace Game.GameLogic
{
    public class Sticks
    {
        //public int[] Wands { get; set; }
        public bool ExtraMove { get; set; }

        public Sticks()
        {
            //Wands = new int[4];
            ExtraMove = false;
        }

        public int Throw()
        {
            var sum = new Random().Next(1, 6);
            if (sum >= 2 && sum <= 4)
                ExtraMove = false;
            
            else
                ExtraMove = true;
            
            return sum;
        }
    }
}