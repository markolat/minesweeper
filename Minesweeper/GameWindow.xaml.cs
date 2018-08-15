using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;

namespace Minesweeper
{
    public partial class GameWindow : Window
    {
        private Grid Field;
        private int fieldWidth;
        private string gamerName;
        private string level;
        private int bombNumber;
        private bool startOfGame;
        List<FieldUnit> listOfUnits;
        private Random rnd;

        public GameWindow()
        {
            InitializeComponent();
        }

        // Constructor
        public GameWindow(string level, string gamerName): this()
        {
            this.Field = new Grid();
            this.gamerName = gamerName;
            this.level = level;
            this.startOfGame = true;
            listOfUnits = new List<FieldUnit>(this.bombNumber);
            switch (level)
            {
                case "easy":
                    this.fieldWidth = 10;
                    this.bombNumber = 30;
                    break;
                case "medium":
                    this.fieldWidth = 12;
                    this.bombNumber = 45;
                    break;
                case "hard":
                    this.fieldWidth = 14;
                    this.bombNumber = 60;
                    break;
                default:
                    break;
            }
            this.MinWidth = 500;
            this.MinHeight = 600;
            PrepareField();
        }

        // Creating dynamic grid and field units
        private void PrepareField()
        {
            for (int i = 0; i < this.fieldWidth; i++)
            {
                Field.ColumnDefinitions.Add(new ColumnDefinition());
                Field.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < this.fieldWidth; i++)
            {
                for (int j = 0; j < this.fieldWidth; j++)
                {
                    FieldUnit fu = new FieldUnit(false, 0, i, j);
                    listOfUnits.Add(fu);
                    fu.Background = new SolidColorBrush(Colors.LightGray);
                    Grid.SetRow(fu, i);
                    Grid.SetColumn(fu, j);
                    Field.Children.Add(fu);
                    fu.Click += Button_click;
                }
            }
            Grid.SetRow(Field, 1);
            mainGrid.Children.Add(Field);
        }

        // Initializing field units with bombs and other data such as nearby bombs
        private void InitializeField(int indexOfFirstFieldUnit)
        {
            this.rnd = new Random();
            int fieldUnitNumber = (int)Math.Pow(this.fieldWidth, 2);
            List<int> listOfBombs = new List<int>(this.fieldWidth);
            // First opened field unit can't have the bomb
            listOfBombs.Add(indexOfFirstFieldUnit);
            int rng;
                
            for(int k = 0; k < this.bombNumber; k++)
            {
                rng = rnd.Next(0, fieldUnitNumber - 1);
                if (listOfBombs.Contains(rng))
                {
                    k--;
                    continue;
                }
                else
                {
                    listOfBombs.Add(rng);
                    listOfUnits[rng].Bomb = true;
                    listOfUnits[rng].NearbyBombs = -1;
                    listOfUnits[rng].IsOpened = false;
                    int row = Grid.GetRow(listOfUnits[rng]);
                    int col = Grid.GetColumn(listOfUnits[rng]);

                    for(int i = -1; i < 2; i++)
                    {
                        for(int j = -1; j < 2; j++)
                        {
                            int r = row + i;
                            int c = col + j;

                            if (r < 0 || r > fieldWidth - 1 || c < 0 || c > fieldWidth - 1)
                                continue;

                            FieldUnit unit = FieldUnit.GetUnit(listOfUnits, r, c);
                            if (unit.Bomb || (i == 0 && j == 0))
                                continue;
                            unit.NearbyBombs++;                            
                        }
                    }
                }
            }
        }

        private void Button_click(object sender, System.EventArgs e)
        {
            FieldUnit b = sender as FieldUnit;
            b.IsOpened = true;
            int indexOfFirstFieldUnit = Field.Children.IndexOf(b);

            if (startOfGame)
            {
                startOfGame = false;
                InitializeField(indexOfFirstFieldUnit);
            }

            int row = Grid.GetRow(b);
            int column = Grid.GetColumn(b);

            TextBlock tb = new TextBlock();
            if (b.Bomb)
                tb.Text = "B";
            else
                tb.Text = b.NearbyBombs.ToString();

            tb.FontSize = 30;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            Field.Children.Remove(b);
            Border border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = new SolidColorBrush(Colors.LightGray);
            border.Child = tb;
            Grid.SetRow(border, row);
            Grid.SetColumn(border, column);
            Field.Children.Add(border);
        }
    }
}
