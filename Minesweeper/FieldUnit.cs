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
        private bool flag;
        private int nearbyBombs;
        private bool opened;
        private int row;
        private int col;

        // Constructor
        public FieldUnit(bool bomb, int nearbyBombs, int i, int j)
        {
            this.bomb = bomb;
            this.nearbyBombs = nearbyBombs;
            this.flag = false;
            this.opened = false;
            this.row = i;
            this.col = j;
        }

        // getters & setters
        public bool Bomb { get { return this.bomb; } set { this.bomb = value; } }
        public bool Flag { get { return this.flag; } set { this.flag = value; } }
        public int NearbyBombs { get { return this.nearbyBombs; } set { this.nearbyBombs = value; } }
        public bool IsOpened { get { return this.opened; } set { this.opened = value; } }
        public int Row { get { return this.row; } }
        public int Col { get { return this.col; } }

        // Static method that returns field unit from provided list of units with the i, j coords
        public static FieldUnit GetUnit(List<FieldUnit> listOfUnits, int i, int j)
        {
            foreach(FieldUnit fu in listOfUnits)
            {
                if (fu.row == i && fu.col == j)
                    return fu;
            }
            return null;
        }
    }
}
