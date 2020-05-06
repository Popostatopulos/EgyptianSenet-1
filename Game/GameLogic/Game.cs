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
        
        public static bool MakeStep(int stepCount, Cell[] map, Figure figure)//Перенести часть? логики в Game
        {
            if (stepCount == 0) return false;
            if (figure.Location == 27)// Свойство дома
            {
                switch (stepCount)
                {
                    case 4:
                        map[figure.Location].State = null;
                        return true;
                    case 5:
                        return false;
                    default:
                        MoveToHouseOfRevival(map, figure); 
                        return true;
                }
            }
            
            if (figure.Location + stepCount >= 26 && map[26].State == null && figure.Location < 26)
            {
                StepToHouseOfBeauty(map, figure);
                return true;
            }

            if (figure.Location == 26)
            {
                if (stepCount == 5)
                    StepOut(stepCount, map, figure);
                else
                {
                    if (map[figure.Location + stepCount].State == null)
                    {
                        SimpleStep(stepCount, map, figure);
                    }

                    else
                    {
                        var temp = map[figure.Location + stepCount].State;
                        MoveToHouseOfRevival(map, temp);
                        // map[figure.Location + stepCount].State = map[figure.Location].State;
                        // map[figure.Location].State = null;
                        // figure.Location += stepCount; 
                        SimpleStep(stepCount, map, figure);
                    }
                }

                return true;
            }//Вложенность
            

            if (figure.Location >= 28)
                return StepOut(stepCount, map, figure);
            
            if (map[figure.Location + stepCount].State == null)
            {
                SimpleStep(stepCount, map, figure);
                return true;
            }

            map[figure.Location + stepCount].State.IsFree = !CheckNeighbors(map, map[figure.Location + stepCount].State);
            
            if (map[figure.Location + stepCount].State != null
                && map[figure.Location + stepCount].State.IsFree && map[figure.Location + stepCount].State.Type != figure.Type)
            {
                StepWithCut(stepCount, map, figure);
                return true;
            }
            
            return false;
        }
        
        public static void MoveToHouseOfRevival(Cell[] map, Figure figure)
        {
            for (var i = 15; i >= 1; i--)
            {
                if (map[i].State != null) continue;
                map[i].State = figure;
                figure.Location = i;
                return;
            }
        }

        public static void SimpleStep(int stepCount, Cell[] map, Figure figure)
        {
            map[figure.Location + stepCount].State = map[figure.Location].State;
            map[figure.Location].State = null;
            figure.Location += stepCount;   
        }

        public static void StepWithCut(int stepCount, Cell[] map, Figure figure)
        {
            var temp = map[figure.Location + stepCount].State;
            map[figure.Location + stepCount].State = map[figure.Location].State;
            map[figure.Location].State = temp;
            figure.Location += stepCount;  
        }

        public static bool StepOut(int stepCount, Cell[] map, Figure figure)
        {
            if (stepCount != map.Length - figure.Location) return false;
            map[figure.Location].State = null;
            return true;
        }
        
        public static void StepToHouseOfBeauty(Cell[] map, Figure figure)
        {
            map[26].State = map[figure.Location].State;
            map[figure.Location].State = null;
            figure.Location = 26; 
        }

        private static bool CheckNeighbors(Cell[] map, Figure figure)
        {
            if (figure.Location + 1 < map.Length)
            {
                if (map[figure.Location + 1].State != null && map[figure.Location + 1].State.Type == figure.Type) return true;
                if (map[figure.Location - 1].State != null && map[figure.Location - 1].State.Type == figure.Type) return true;
            }

            return map[figure.Location - 1].State != null && map[figure.Location - 1].State.Type == figure.Type;
        }
    }
}