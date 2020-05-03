using System;
using System.Collections.Generic;

namespace Game.GameLogic
{
    
    public class Player
    {
        public ChipsType OwnType { get; }
        public bool IsMan { get; set; }
        public List<Figure> OwnFigures { get; set; }

        public Player(ChipsType chipsType, Cell[] map)
        {
            OwnType = chipsType;
            IsMan = false;
            OwnFigures = new List<Figure>();
            for (var i = 1; i <= 14; i++)
            {
                if (map[i].State.Type == OwnType)
                    OwnFigures.Add(map[i].State);
            }
        }
        
        public void Step(Sticks sticks, Cell[] map, int figureNumber)// ChooseFigure in other place
        {
            var stepCount = sticks.Throw();
            // var figureNumber = ChooseFigure();//Индексация должна начинаться с 0
            OwnFigures[figureNumber - 1].MakeStep(stepCount, map);
            //if (OwnFigures)
            if (OwnFigures[figureNumber - 1].Location == 26 || OwnFigures[figureNumber - 1].Location == 27)
                Step(sticks, map, figureNumber);
            else if (sticks.ExtraMove)
            {
                //Перевыбор номера
                //Step с этим номером
            }
        }
    }
}