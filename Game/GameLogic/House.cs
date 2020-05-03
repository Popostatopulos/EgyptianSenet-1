namespace Game.GameLogic
{
    public class HouseOfRevival : Cell
    {
        public HouseOfRevival(Figure state) : base (state)
        {
            State = state;
        }
    }
    
    public class HouseOfBeauty : Cell
    {
        public int ConditionOfExit = 5;
        public HouseOfBeauty(Figure state) : base (state)
        {
            State = state;
        }
    }
    
    public class HouseOfWater : Cell
    {
        public HouseOfWater(Figure state) : base (state)
        {
            State = state;
        }
    }
    
    public class HouseOfThreeTruths : Cell
    {
        public HouseOfThreeTruths(Figure state) : base (state)
        {
            State = state;
        }
    }
    
    public class HouseOfIsidNeftid : Cell
    {
        public HouseOfIsidNeftid(Figure state) : base (state)
        {
            State = state;
        }
    }
    
    public class HouseOfRaHorati : Cell
    {
        public HouseOfRaHorati(Figure state) : base (state)
        {
            State = state;
        }
    }
}