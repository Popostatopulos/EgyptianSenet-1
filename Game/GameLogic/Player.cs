using System;
using System.Collections.Generic;

namespace Game.GameLogic
{
    
    public class Player
    {
        public ChipsType OwnType { get; }
        public bool IsMan { get; set; }//Странная переменная
        public List<Figure> OwnFigures { get; set; }
        public Figure FigureInTheHouseOfBeauty { get; set; }
        public Figure FigureInTheHouseOfWater { get; set; }
        public bool AbleToMove { get; set; }

        public Player(ChipsType chipsType, Cell[] map)
        {
            OwnType = chipsType;
            IsMan = false;
            OwnFigures = new List<Figure>();//из Game
            for (var i = 1; i <= 14; i++)
            {
                if (map[i].State.Type == OwnType)
                    OwnFigures.Add(map[i].State);
            }
        }
        
        public void ArtificialIntelligenceMove(Sticks sticks, Cell[] map, int figureNumber)// ChooseFigure in other place
        //Пример: метод принимает номер фигуры и координаты цели
        {
            var stepCount = sticks.Throw();
            // var figureNumber = ChooseFigure();//Индексация должна начинаться с 0
            //OwnFigures[figureNumber - 1].MakeStep(stepCount, map);//Не обращаться по индексу у листа
            //if (OwnFigures)
            if (OwnFigures[figureNumber - 1].Location == 26 || OwnFigures[figureNumber - 1].Location == 27) 
                ArtificialIntelligenceMove(sticks, map, figureNumber);
            else if (sticks.ExtraMove)
            {
                //Перевыбор номера
                //Step с этим номером
            }
        }
        
        //Check() и Move(), просчет хода(ИИ), сам ход

        public bool Check(int figureNumber, int stepCount)
        {
            throw new NotImplementedException();
        }
        
    }
}