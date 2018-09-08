using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            updateScoreBoardSP(difficulty);
            spScoresEasy.Visibility = Visibility.Visible;
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

        // Radio button check event that changes category in scoreboard segment
        private void radiobutton_category_change(object sender, System.EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            string category = rdb.Content.ToString();
            switch (category)
            {
                case "Easy":
                    updateScoreBoardSP(category);
                    spScoresEasy.Visibility = Visibility.Visible;
                    spScoresMedium.Visibility = Visibility.Hidden;
                    spScoresHard.Visibility = Visibility.Hidden;
                    break;
                case "Medium":
                    updateScoreBoardSP(category);
                    spScoresEasy.Visibility = Visibility.Hidden;
                    spScoresMedium.Visibility = Visibility.Visible;
                    spScoresHard.Visibility = Visibility.Hidden;
                    break;
                case "Hard":
                    updateScoreBoardSP(category);
                    spScoresEasy.Visibility = Visibility.Hidden;
                    spScoresMedium.Visibility = Visibility.Hidden;
                    spScoresHard.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void updateScoreBoardSP(string category)
        {
            string fileName = "sb" + category;
            List<Score> listOfScores = new List<Score>();

            if (File.Exists(fileName))
            {
                listOfScores.Clear();
                listOfScores = Score.ReadScores(fileName);
            }
            else
            {
                for (int i = 0; i < 10; i++)
                    listOfScores.Add(new Score());
            }

            switch (category)
            {
                case "Easy":
                    spScoresEasy.Children.Clear();
                    foreach(Score score in listOfScores)
                    {
                        if (score.Score_time < 999)
                            spScoresEasy.Children.Add(new TextBlock { Text = score.ToString() });
                        else
                            spScoresEasy.Children.Add(new TextBlock { Text = "N/A"});
                    }
                    break;
                case "Medium":
                    spScoresMedium.Children.Clear();
                    foreach (Score score in listOfScores)
                    {
                        if (score.Score_time < 999)
                            spScoresMedium.Children.Add(new TextBlock { Text = score.ToString() });
                        else
                            spScoresMedium.Children.Add(new TextBlock { Text = "N/A" });
                    }
                    break;
                case "Hard":
                    spScoresHard.Children.Clear();
                    foreach (Score score in listOfScores)
                    {
                        if (score.Score_time < 999)
                            spScoresHard.Children.Add(new TextBlock { Text = score.ToString() });
                        else
                            spScoresHard.Children.Add(new TextBlock { Text = "N/A" });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
