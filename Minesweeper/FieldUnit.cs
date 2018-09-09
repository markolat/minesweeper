using System.Collections.Generic;


namespace Minesweeper
{
    class FieldUnit : System.Windows.Controls.Button
    {
        // Constructor --> No auttributes (using auto property)
        public FieldUnit(bool bomb, int nearbyBombs, int i, int j)
        {
            Bomb = bomb;
            NearbyBombs = nearbyBombs;
            Flag = false;
            IsOpened = false;
            Row = i;
            Col = j;
        }

        // getters & setters
        public bool Bomb { get; set; }
        public bool Flag { get; set; }
        public int NearbyBombs { get; set; }
        public bool IsOpened { get; set; }
        public int Row { get; }
        public int Col { get; }

        // Static method that returns field unit from provided list of units with the i, j coords
        public static FieldUnit GetUnit(List<FieldUnit> listOfUnits, int i, int j)
        {
            foreach(FieldUnit fu in listOfUnits)
            {
                if (fu.Row == i && fu.Col == j)
                    return fu;
            }
            return null;
        }
    }
}
