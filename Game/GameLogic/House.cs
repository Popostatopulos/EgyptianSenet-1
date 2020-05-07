namespace Game.GameLogic
{
    public class HouseOfRevival : Cell
    {
        public static int Location { get; set; }
        public HouseOfRevival(Figure state, int location) : base (state)
        {
            State = state;
            Location = location;
        }
    }
    
    public class HouseOfBeauty : Cell, IFinalHouse
    {
        public HouseOfBeauty(Figure state) : base (state)
        {
            State = state;
        }
    }
    
    public class HouseOfWater : Cell, IFinalHouse
    {
        public HouseOfWater(Figure state) : base (state)
        {
            State = state;
        }
    }
    
    public class HouseOfThreeTruths : Cell, IFinalHouse
    {
        public HouseOfThreeTruths(Figure state) : base (state)
        {
            State = state;
        }
    }
    
    public class HouseOfIsidaAndNeftida : Cell, IFinalHouse
    {
        public HouseOfIsidaAndNeftida(Figure state) : base (state)
        {
            State = state;
        }
    }
    
    public class HouseOfRaHorati : Cell, IFinalHouse
    {
        public HouseOfRaHorati(Figure state) : base (state)
        {
            State = state;
        }
    }
}