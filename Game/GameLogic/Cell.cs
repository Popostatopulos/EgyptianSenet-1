namespace Game.GameLogic
{
    public class Cell
    { 
        //public int ConditionOfExit { get; set; }
        public Figure State { get; set; }
        public Cell(Figure state)
        {
            State = state;
        }
    }
}