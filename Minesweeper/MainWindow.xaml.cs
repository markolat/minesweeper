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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper
{
    public partial class MainWindow : Window
    {
        private Grid gGrid;
        private Grid sbGrid;
        private string name;
        private string difficulty;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            btnGame.Click += game_scoreboard_click;
            btnScoreboard.Click += game_scoreboard_click;
            gGrid = gameGrid;
            sbGrid = scoreboardGrid;
            sbGrid.Visibility = Visibility.Hidden;
            difficulty = "Easy";
            justChecking();
            MessageBox.Show("Congratulations! You won! :)", "Congratulations", MessageBoxButton.OK, MessageBoxImage.None);
        }

        // Creating controls for the scoreboard segment
        private void justChecking()
        {
            for(int i = 0; i < 10; i++)
            {
                spScores.Children.Add(new TextBlock { Text = new Score("Jon Doe", difficulty, 555).ToString(), FontSize = 15 });
            }
        }

        // Game/Scoreboard button click
        private void game_scoreboard_click(object sender, System.EventArgs e)
        {
            Button btn = sender as Button;
            if(btn == btnGame)
            {
                sbGrid.Visibility = Visibility.Hidden;
                gGrid.Visibility = Visibility.Visible;
            }
            else
            {
                sbGrid.Visibility = Visibility.Visible;
                gGrid.Visibility = Visibility.Hidden;
            }
        }

        // Radio button check event that changes difficulty level in game segment
        private void radiobutton_difficulty_change(object sender, System.EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            difficulty = rdb.Content.ToString();
        }

        // Play button click event
        private void btnPlay_click(object sender, System.EventArgs e)
        {
            name = nameTextBox.Text;
            new GameWindow(this, difficulty, name).Show();
        }
    }
}
