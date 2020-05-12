using System;
using System.Collections.Generic;

namespace Game.GameLogic
{
    
    public class Player
    {
        public ChipsType OwnType { get; }
        public bool IsAI { get; }
        public List<Figure> OwnFigures { get; }
        public Figure FigureInTheHouseOfBeauty { get; set; }
        public Figure FigureInTheHouseOfWater { get; set; }

        public Player(ChipsType chipsType, Cell[] map, bool isAI = false)
        {
            IsAI = isAI;
            OwnType = chipsType;
            OwnFigures = new List<Figure>();
            for (var i = 1; i <= 14; i++)
            {
                if (map[i].State.Type == OwnType)
                    OwnFigures.Add(map[i].State);
            }
        }

        public Figure AIChoice(Dictionary<string, List<Figure>> priorities)
        {
            Figure currentFigure = null;
            if (priorities["high"].Count > 0)
            {
                var random = new Random();
                currentFigure = priorities["high"][random.Next(0, priorities["high"].Count)];
            }
            else if (priorities["low"].Count > 0)
            {
                var random = new Random();
                currentFigure = priorities["low"][random.Next(0, priorities["low"].Count)];
            }
            
            return currentFigure;
        }

        public Dictionary<string, List<Figure>> GetFigureWithHighestPriority(List<Figure> figures, int stepCount, Cell[] map)
        {
            var priorityDict = new Dictionary<string, List<Figure>>();
            priorityDict["high"] = new List<Figure>();
            priorityDict["low"] = new List<Figure>();
            foreach (var figure in figures)
            {
                Figure target = null;
                if (figure.Location + stepCount < map.Length)
                    target = map[figure.Location + stepCount].State;
                if (target != null && target.Type != figure.Type && target.IsFree)
                    priorityDict["high"].Add(figure);
                else if (figure.Location + stepCount == map.Length)
                    priorityDict["high"].Add(figure);
                else if (target == null)
                    priorityDict["low"].Add(figure);
            }

            return priorityDict;
        }
        
    }
}