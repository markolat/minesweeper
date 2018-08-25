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
using System.IO;
using System.Windows.Threading;

namespace Minesweeper
{
    public partial class GameWindow : Window
    {
        private Window mainWindow;
        private Grid Field;
        private Frame frame;
        private DispatcherTimer dTimer;
        private Image smiley;
        private Image smiley_click;
        private int fieldWidth;
        private string gamerName;
        private string difficulty;
        private int bombNumber;
        private int mineCounter;
        private int timer;
        private const int fieldUnitSize = 40;
        private bool startOfGame;
        List<FieldUnit> listOfUnits;
        private Random rnd;

        // Default constructor
        public GameWindow()
        {
            InitializeComponent();
        }

        // Constructor
        public GameWindow(Window mainWindow, string difficulty, string gamerName): this()
        {
            this.mainWindow = mainWindow;
            this.mainWindow.Hide();
            Field = new Grid();
            frame = new Frame();
            dTimer = new DispatcherTimer();
            this.gamerName = gamerName;
            this.difficulty = difficulty;
            dTimer.Tick += dispatcherTimer_Tick;
            dTimer.Interval = new TimeSpan(0, 0, 1);
            timer = 0;
            listOfUnits = new List<FieldUnit>(this.bombNumber);

            // Setting attributes regarding the choosen difficulty
            switch (difficulty)
            {
                case "easy":
                    this.fieldWidth = 10;
                    this.bombNumber = 15; // change it later to 30 (Project conditions :S)
                    break;
                case "medium":
                    this.fieldWidth = 12;
                    this.bombNumber = 25; // change it to 45
                    break;
                case "hard":
                    this.fieldWidth = 14;
                    this.bombNumber = 35; // change it to 60
                    break;
                default:
                    break;
            }

            mineCounter = bombNumber;
            txtMineCounter.Text = mineCounter.ToString();

            // Defining frame width and height regarding the field unit size and
            // setting min width and height of game window
            this.frame.Width = fieldUnitSize * fieldWidth;
            this.frame.Height = fieldUnitSize * fieldWidth;
            this.MinWidth = this.frame.Width + 100;
            this.MinHeight = this.frame.Height + 200;

            // Instantiating smiley images
            smiley = new Image
            {
                Source = new BitmapImage(new Uri("Resources/Smiley.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center
            };
            smiley_click = new Image
            {
                Source = new BitmapImage(new Uri("Resources/Smiley-click.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center
            };

            // Attaching Smiley image to the "new game" button
            btnSmiley.Content = smiley;

            CreateGrid();
            PrepareField();
        }

        // Creating dynamic grid
        private void CreateGrid()
        {
            for (int i = 0; i < this.fieldWidth; i++)
            {
                Field.ColumnDefinitions.Add(new ColumnDefinition());
                Field.RowDefinitions.Add(new RowDefinition());
            }

            // Setting border around the field grid
            Border border = new Border
            {
                BorderThickness = new Thickness(2),
                BorderBrush = new SolidColorBrush(Colors.Gray),
                Child = Field
            };

            this.frame.Content = border;
            Grid.SetRow(this.frame, 1);
            Grid.SetColumn(this.frame, 0);
            Grid.SetColumnSpan(this.frame, 3);
            mainGrid.Children.Add(this.frame);
        }

        // Creating field units
        private void PrepareField()
        {
            this.startOfGame = true;
            for (int i = 0; i < this.fieldWidth; i++)
            {
                for (int j = 0; j < this.fieldWidth; j++)
                {
                    FieldUnit fu = new FieldUnit(false, 0, i, j);
                    listOfUnits.Add(fu);
                    // fu.Background = new SolidColorBrush(Colors.LightGray);
                    fu.Style = FindResource("FieldUnitStyle") as Style;
                    Grid.SetRow(fu, i);
                    Grid.SetColumn(fu, j);
                    Field.Children.Add(fu);

                    // Adding field unit events
                    fu.Click += FieldUnit_click;

                    fu.PreviewMouseLeftButtonDown += delegate
                    {
                        btnSmiley.Content = smiley_click;
                    };

                    fu.PreviewMouseLeftButtonUp += delegate
                    {
                        btnSmiley.Content = smiley;
                    };

                    fu.MouseRightButtonUp += FieldUnit_right_click;
                }
            }
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
                    listOfUnits[rng].Open = false;
                    int row = Grid.GetRow(listOfUnits[rng]);
                    int col = Grid.GetColumn(listOfUnits[rng]);

                    // updating 'nearby bombs' value to units around bomb-initialized unit
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

        // Field unit click event
        private void FieldUnit_click(object sender, System.EventArgs e)
        {
            FieldUnit fu = sender as FieldUnit;

            // Checking if it's start of game
            if (startOfGame)
            {
                startOfGame = false;
                dTimer.Start();
                int indexOfFirstFieldUnit = Field.Children.IndexOf(fu);
                InitializeField(indexOfFirstFieldUnit);
            }
            if (fu.Flag)
                return;

            openField(fu);
        }

        // Field unit rightclick event (setting flag)
        private void FieldUnit_right_click(object sender, System.EventArgs e)
        {
            FieldUnit fu = sender as FieldUnit;

            // If it is flag, remove it and return
            if (fu.Flag)
            {
                fu.Flag = false;
                fu.Content = "";
                mineCounter++;
                txtMineCounter.Text = mineCounter.ToString();
                return;
            }

            // If mine counter is 0 then can't put more flags
            if (mineCounter == 0)
                return;

            fu.Flag = true;

            // Attaches flag image to the button
            fu.Content = new Image
            {
                Source = new BitmapImage(new Uri("Resources/flag.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center
            };
            mineCounter--;
            txtMineCounter.Text = mineCounter.ToString();
        }

        // Recursive function that opens field units
        private void openField(FieldUnit fu)
        {
            fu.Open = true;

            int row = Grid.GetRow(fu);
            int column = Grid.GetColumn(fu);

            TextBlock tb = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            if (fu.Bomb)
            {
                gameOver(fu);
                return;
            }
            else
            {
                tb.Text = fu.NearbyBombs.ToString();
            }
                
            setTbStyle(tb);
            Field.Children.Remove(fu);
            Border border = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Colors.LightGray),
                Child = tb
            };
            Grid.SetRow(border, row);
            Grid.SetColumn(border, column);
            Field.Children.Add(border);

            // Opening adjacent field units if condition is true
            if (fu.NearbyBombs == 0)
            {
                for(int i = -1; i < 2; i++)
                {
                    for(int j = -1; j < 2; j++)
                    {
                        int r = row + i;
                        int c = column + j;

                        if (r < 0 || r > fieldWidth - 1 || c < 0 || c > fieldWidth - 1 ||
                            (i == 0 && j == 0))
                            continue;

                        FieldUnit unit = FieldUnit.GetUnit(listOfUnits, r, c);

                        // If it's already open or it's marked with flag, continue
                        if (unit.Open || unit.Flag)
                            continue;
                        else
                            openField(unit);
                    }
                }
            }
            else
                return;
        }

        // Ending the game (stepped on mine)
        private void gameOver(FieldUnit fu)
        {
            dTimer.Stop();
            fu.IsEnabled = false;
            // Attaches mine-blown image to the button
            fu.Content = new Image
            {
                Source = new BitmapImage(new Uri("Resources/mine-blown.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center
            };

            // Show other mines and disable filed units
            foreach (FieldUnit funit in listOfUnits)
            {
                funit.IsEnabled = false;
                if (funit.Bomb)
                {
                    if (funit == fu || funit.Flag)
                        continue;

                    funit.Content = new Image
                    {
                        Source = new BitmapImage(new Uri("Resources/mine.png", UriKind.Relative)),
                        VerticalAlignment = VerticalAlignment.Center
                    };
                }
                else
                {
                    if (funit.Flag)
                    {
                        funit.Content = new Image
                        {
                            Source = new BitmapImage(new Uri("Resources/notMine.png", UriKind.Relative)),
                            VerticalAlignment = VerticalAlignment.Center
                        };
                    }
                }
            }

            btnSmiley.Content = new Image
            {
                Source = new BitmapImage(new Uri("Resources/Smiley-dead.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        // Setting the color style for textblock controls
        private void setTbStyle(TextBlock tb)
        {
            switch (tb.Text)
            {
                case "0":
                    tb.Text = "";
                    break;
                case "1":
                    tb.Style = FindResource("StyleLightBlue") as Style;
                    break;
                case "2":
                    tb.Style = FindResource("StyleGreen") as Style;
                    break;
                case "3":
                    tb.Style = FindResource("StyleRed") as Style;
                    break;
                case "4":
                    tb.Style = FindResource("StyleDarkBlue") as Style;
                    break;
                case "5":
                    tb.Style = FindResource("StyleBrown") as Style;
                    break;
                case "6":
                    tb.Style = FindResource("StyleAqua") as Style;
                    break;
                case "7":
                    tb.Style = FindResource("StyleBlack") as Style;
                    break;
                case "8":
                    tb.Style = FindResource("StyleLightGray") as Style;
                    break;
                default:
                    break;
            }
        }

        // On closing event
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dTimer.Stop(); // I'm not sure if i need to do this, but, i did it just in case
            this.mainWindow.Show();
        }

        // Smiley button click event.. Starting new game
        private void btnSmiley_Click(object sender, RoutedEventArgs e)
        {
            Field.Children.Clear();
            listOfUnits.Clear();
            PrepareField();
            mineCounter = bombNumber;
            txtMineCounter.Text = mineCounter.ToString();
            dTimer.Stop();
            timer = 0;
            txtTimer.Text = timer.ToString();
            btnSmiley.Content = smiley;
        }

        // Dispatcher timer functionality
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timer++;
            txtTimer.Text = timer.ToString();
        }
    }
}
