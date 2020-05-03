using System.Collections;

namespace Game.GameLogic
{
    public enum ChipsType
    {
        Cone,
        Coil
    }
    public class Figure
    {
        public ChipsType Type { get; }
        public int SerialNumber { get; }
        public int Location { get; set; }
        public bool IsFree { get; set; }
        public Figure(int location, int serialNumber, ChipsType type)
        {
            Type = type;
            Location = location;
            SerialNumber = serialNumber;
            IsFree = true;
        }


        public bool MakeStep(int stepCount, Cell[] map)
        {
            if (stepCount == 0) return false;
            if (Location == 27)
            {
                switch (stepCount)
                {
                    case 4:
                        map[Location].State = null;
                        return true;
                    case 5:
                        return false;
                    default:
                        MoveToHouseOfRevival(map); 
                        return true;
                }
            }
            
            if (Location + stepCount >= 26 && map[26].State == null && Location < 26)
            {
                StepToHouseOfBeauty(map);
                return true;
            }

            if (Location == 26)
            {
                if (stepCount == 5)
                    map[Location].State = null;
                else
                {
                    if (map[Location + stepCount].State == null)
                    {
                        SimpleStep(stepCount, map);
                    }

                    else
                    {
                        var temp = map[Location + stepCount].State;
                        temp.MoveToHouseOfRevival(map);
                        map[Location + stepCount].State = map[Location].State;
                        map[Location].State = null;
                        Location += stepCount; 
                    }
                }

                return true;
            }
            

            if (Location >= 28)
                return StepOut(stepCount, map);
            
            if (map[Location + stepCount].State == null)
            {
                SimpleStep(stepCount, map);
                return true;
            }

            map[Location + stepCount].State.IsFree = !map[Location + stepCount].State.CheckNeighbors(map);
            if (map[Location + stepCount].State != null
                && map[Location + stepCount].State.IsFree && map[Location + stepCount].State.Type != Type)
            {
                StepWithCut(stepCount, map);
                return true;
            }
            
            return false;
        }

        private void MoveToHouseOfRevival(Cell[] map)
        {
            for (var i = 15; i >= 1; i--)
            {
                if (map[i].State != null) continue;
                map[i].State = this;
                Location = i;
                return;
            }
        }

        private void SimpleStep(int stepCount, Cell[] map)
        {
            map[Location + stepCount].State = map[Location].State;
            map[Location].State = null;
            Location += stepCount;   
        }
        
        private void StepWithCut(int stepCount, Cell[] map)
        {
            var temp = map[Location + stepCount].State;
            map[Location + stepCount].State = map[Location].State;
            map[Location].State = temp;
            Location += stepCount;  
        }

        private bool StepOut(int stepCount, Cell[] map)
        {
            if (stepCount != map.Length - Location) return false;
            map[Location].State = null;
            return true;
        }
        
        private void StepToHouseOfBeauty(Cell[] map)
        {
            map[26].State = map[Location].State;
            map[Location].State = null;
            Location = 26; 
        }
        
        private bool CheckNeighbors(Cell[] map)
        {
            if (Location + 1 < map.Length)
            {
                if (map[Location + 1].State != null && map[Location + 1].State.Type == Type) return true;
                if (map[Location - 1].State != null && map[Location - 1].State.Type == Type) return true;
            }

            return map[Location - 1].State != null && map[Location - 1].State.Type == Type;
        }
    }
}
