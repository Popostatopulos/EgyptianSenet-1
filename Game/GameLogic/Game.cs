using Game;

namespace Game.GameLogic
{
    public class Game
    {
        public Cell[] Map { get; set; }

        public Player PlayerFirst { get; set; }
        public Player PlayerSecond { get; set; }
        
            
        //Для игрока - человека нужно сделать выбор фигуры по нажатию ЛКМ
        //Для ИИ метод ChooseFigure должен быть написан в классе Player
        
        public Game()
        {
            Map = new Cell[31];
            MapFilling();
            PlayerFirst = new Player(ChipsType.Cone, Map);
            PlayerSecond = new Player(ChipsType.Coil, Map);
        }

        private void MapFilling()
        {
            for (var i = 1; i < 14; i += 2)
            {
                Map[i] = new Cell(new Figure(i, (i+1)/2, ChipsType.Cone));
                Map[i + 1] = new Cell(new Figure(i + 1, (i+1)/2, ChipsType.Coil));
            }

            for (var i = 16; i <= 25; i++)
            {
                Map[i] = new Cell(null);
            }
            
            Map[15] = new HouseOfRevival(null);
            Map[26] = new HouseOfBeauty(null);
            Map[27] = new HouseOfWater(null);
            Map[28] = new HouseOfThreeTruths(null);
            Map[29] = new HouseOfIsidNeftid(null);
            Map[30] = new HouseOfRaHorati(null);
        }
    }
}