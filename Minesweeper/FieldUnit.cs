using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class FieldUnit : System.Windows.Controls.Button
    {
        private bool bomb;
        private int nearbyBombs;
        private bool open;
        private int i;
        private int j;

        // Constructor
        public FieldUnit(bool bomb, int nearbyBombs, int i, int j)
        {
            this.bomb = bomb;
            this.nearbyBombs = nearbyBombs;
            this.open = false;
            this.i = i;
            this.j = j;
        }

        // getters & setters
        public bool Bomb { get { return this.bomb; } set { this.bomb = value; } }
        public int NearbyBombs { get { return this.nearbyBombs; } set { this.nearbyBombs = value; } }
        public bool IsOpened { get { return this.open; } set { this.open = value; } }

        // Static method that returns field unit from provided list of units with the i, j coords
        public static FieldUnit GetUnit(List<FieldUnit> listOfUnits, int i, int j)
        {
            foreach(FieldUnit fu in listOfUnits)
            {
                if (fu.i == i && fu.j == j)
                    return fu;
            }
            return null;
        }
    }
}
