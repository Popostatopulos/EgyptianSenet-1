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

        public bool InTheHouseOfBeauty { get; set; }
        public bool InTheHouseOfWater { get; set; }

        public Figure(int location, int serialNumber, ChipsType type)
        {
            Type = type;
            Location = location;
            SerialNumber = serialNumber;
            IsFree = true;
            InTheHouseOfBeauty = false;
            InTheHouseOfWater = false;
        }
    }
}
