using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace Minesweeper
{
    public partial class MainWindow : Window
    {
        private Grid gGrid;
        private Grid sbGrid;
        private string playerName;
        private string difficulty;
        private static StackPanel spName;
        private static StackPanel spTime;

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
            spName = spScoresName;
            spTime = spScoresTime;
            UpdateScoreBoard(difficulty);
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
            playerName = nameTextBox.Text;
            if (playerName.Equals(""))
            {
                MessageBox.Show("Name is required. Please, enter your name.", "Empty name field", MessageBoxButton.OK);
                return;
            }
            new GameWindow(this, difficulty, playerName).Show();
        }

        // Radio button check event that changes category in scoreboard segment
        private void radiobutton_category_change(object sender, System.EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            string category = rdb.Content.ToString();
            switch (category)
            {
                case "Easy":
                    UpdateScoreBoard(category);
                    break;
                case "Medium":
                    UpdateScoreBoard(category);
                    break;
                case "Hard":
                    UpdateScoreBoard(category);
                    break;
                default:
                    break;
            }
        }

        // Method that updates scoreboard stackpanel with scores
        public static void UpdateScoreBoard(string category)
        {
            string fileName = "sb" + category;
            List<Score> listOfScores = new List<Score>();

            if (File.Exists(fileName))
                listOfScores = Score.ReadScores(fileName);
            else
                for (int i = 0; i < 10; i++)
                    listOfScores.Add(new Score());

            spName.Children.Clear();
            spTime.Children.Clear();
            foreach (Score score in listOfScores)
            {
                spName.Children.Add(new TextBlock { Text = "Name: " + score.PlayerName.ToString() });
                if(score.Score_time == 999)
                     spTime.Children.Add(new TextBlock { Text = "Time: N/A" });
                else
                    spTime.Children.Add(new TextBlock { Text = "Time: " + score.Score_time.ToString() });
            }    
        }
    }
}
