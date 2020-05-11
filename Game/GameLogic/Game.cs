using System;
using System.Collections.Generic;
using System.Linq;
using Game.Views;

namespace Game.GameLogic
{
    public class Game
    {
        public Cell[] Map { get; set; }
        public static Sticks Sticks { get; set; }
        public Player PlayerFirst { get; set; }
        public Player PlayerSecond { get; set; }
        private bool isFirstPlayerCurrent = false;
        public static Player CurrentPlayer { get; set; }
        


        //Для игрока - человека нужно сделать выбор фигуры по нажатию ЛКМ
        //Для ИИ метод ChooseFigure должен быть написан в классе Player
        public Game()
        {
            Map = new Cell[31];
            MapFilling();
            PlayerFirst = new Player(ChipsType.Cone, Map);
            PlayerSecond = new Player(ChipsType.Coil, Map);
            Sticks = new Sticks();
            CurrentPlayer = PlayerSecond;
        }
        

        private void MapFilling()
        {
            for (var i = 1; i < 14; i += 2)
            {
                Map[i] = new Cell(new Figure(i, (i + 1) / 2, ChipsType.Cone));
                Map[i + 1] = new Cell(new Figure(i + 1, (i + 1) / 2, ChipsType.Coil));
            }

            for (var i = 16; i <= 25; i++)
            {
                Map[i] = new Cell(null);
            }
            
            Map[15] = new HouseOfRevival(null, 15);
            Map[26] = new HouseOfBeauty(null);
            Map[27] = new HouseOfWater(null);
            Map[28] = new HouseOfThreeTruths(null);
            Map[29] = new HouseOfIsidaAndNeftida(null);
            Map[30] = new HouseOfRaHorati(null);
        }
        
        public void ChangeCurrentPlayer()
        {
            CurrentPlayer = isFirstPlayerCurrent ? PlayerSecond : PlayerFirst;
            isFirstPlayerCurrent = CurrentPlayer == PlayerFirst;
        }

        public static bool IsGameOver() => CurrentPlayer.OwnFigures.Count == 0;

        public void NoHumanMove(int figureNumber, Sticks sticks)
        {
            var stepCount = sticks.Throw();
            var currentFigure = CurrentPlayer.OwnFigures.Find(figure => figure.SerialNumber == figureNumber);
            if (!MakeStep(stepCount, Map, currentFigure)) return;
            if (sticks.ExtraMove) 
                NoHumanMove(currentFigure.SerialNumber, sticks);
            ChangeCurrentPlayer();
        }

        public static bool MakeStep(int stepCount, Cell[] map, Figure figure)
        { 
            if (stepCount == 0) return false;
            var targetLocation = figure.Location + stepCount;
                
            if (map[figure.Location] is HouseOfWater)
            {
                switch (stepCount)
                {
                    case 4:
                        map[figure.Location].State = null;
                        CurrentPlayer.FigureInTheHouseOfWater = null;
                        return true;
                    case 5:
                        return false;
                    default:
                        MoveToHouseOfRevival(map, figure);
                        CurrentPlayer.FigureInTheHouseOfWater = null;
                        return true;
                }
            }
            
            if (targetLocation < map.Length 
                && map[targetLocation] is IFinalHouse 
                && !(map[figure.Location] is IFinalHouse))
            {
                StepToHouseOfBeauty(map, figure);
                CurrentPlayer.FigureInTheHouseOfBeauty = figure;
                return true;
            }

            if (map[figure.Location] is HouseOfBeauty)
            {
                if (stepCount == 5)
                    StepOut(stepCount, map, figure);
                else
                {
                    if (map[targetLocation].State == null)
                        SimpleStep(stepCount, map, figure);

                    else
                    {
                        var temp = map[targetLocation].State;
                        MoveToHouseOfRevival(map, temp);
                        SimpleStep(stepCount, map, figure);
                    }

                    if (stepCount == 1)
                        CurrentPlayer.FigureInTheHouseOfWater = figure;
                }
                CurrentPlayer.FigureInTheHouseOfBeauty = null;
                return true;
            }//Вложенность
            

            if (map.Length - figure.Location <= 3)
                return StepOut(stepCount, map, figure);
            
            if (map[figure.Location + stepCount].State != null)
                map[figure.Location + stepCount].State.IsFree = !CheckNeighbors(map, map[figure.Location + stepCount].State);
            
            if (map[targetLocation].State == null)
            {
                SimpleStep(stepCount, map, figure);
                return true;
            }

            if (map[targetLocation].State != null
                && map[targetLocation].State.IsFree && map[targetLocation].State.Type != figure.Type)
            {
                StepWithCut(stepCount, map, figure);
                return true;
            }
            
            return false;
        }

        private static void MoveToHouseOfRevival(Cell[] map, Figure figure)
        {
            map[figure.Location].State = null;
            for (var i = HouseOfRevival.Location; i >= 1; i--)
            {
                if (map[i].State != null) continue;
                map[i].State = figure;
                figure.Location = i;
                return;
            }
        }

        private static void SimpleStep(int stepCount, Cell[] map, Figure figure)
        {
            map[figure.Location + stepCount].State = map[figure.Location].State;
            map[figure.Location].State = null;
            figure.Location += stepCount;   
        }

        private static void StepWithCut(int stepCount, Cell[] map, Figure figure)
        {
            var temp = map[figure.Location + stepCount].State;
            map[figure.Location + stepCount].State = map[figure.Location].State;
            map[figure.Location].State = temp;
            map[figure.Location].State.Location -= stepCount;
            figure.Location += stepCount;
        }

        private static bool StepOut(int stepCount, Cell[] map, Figure figure)
        {
            if (stepCount != map.Length - figure.Location) return false;
            map[figure.Location].State = null;
            CurrentPlayer.OwnFigures.Remove(figure);
            return true;
        }

        private static void StepToHouseOfBeauty(Cell[] map, Figure figure)
        {
            map[26].State = map[figure.Location].State;
            map[figure.Location].State = null;
            figure.Location = 26;
            Sticks.ExtraMove = true;
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

        public List<Figure> CheckAbleToMove(int stepCount)
        {
            var result = new List<Figure>();
            foreach (var figure in CurrentPlayer.OwnFigures)
            {
                if (figure.Location + stepCount < Map.Length)
                {
                    var target = Map[figure.Location + stepCount];
                    if (target.State == null || target.State.IsFree && figure.Type != target.State.Type)
                        result.Add(figure);
                }

                else if (figure.Location + stepCount == Map.Length)
                    result.Add(figure);
            }

            return result;
        }
    }
}