using System;

namespace Game.GameLogic
{
    public class Sticks
    {
        public int[] Wands { get; set; }
        public bool ExtraMove { get; set; }

        public Sticks()
        {
            Wands = new int[4];
            ExtraMove = false;
        }

        public int Throw()
        {
            var sum = 0;
            for (var i = 0; i < 4; i++)
            {
                Wands[i] = new Random().Next(1, 3);
                if (Wands[i] == 1)
                    sum++;
            }

            if (sum == 2 || sum == 3)
            {
                ExtraMove = false;
                return sum;
            }
            
            ExtraMove = true;
            if (sum == 0)
                sum = 5;
            return sum;
        }
    }
}