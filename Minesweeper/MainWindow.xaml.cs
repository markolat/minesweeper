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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Frame gFrame;
        private Frame sbFrame;
        private string name;
        private string difficulty;
        private TextBox nameTextBox;
        private StackPanel spScores;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            btnGame.Click += game_scoreboard_click;
            btnScoreboard.Click += game_scoreboard_click;
            gFrame = gameFrame;
            sbFrame = scoreboardFrame;
            sbFrame.Visibility = Visibility.Hidden;
            difficulty = "Easy";
            createGameControls();
            createScoreboardControls();
        }

        // Creating controls for the game segment
        private void createGameControls()
        {
            Grid gameSectionGrid = new Grid();
            gameSectionGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameSectionGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameSectionGrid.RowDefinitions.Add(new RowDefinition());
            gameSectionGrid.RowDefinitions.Add(new RowDefinition());
            gameSectionGrid.RowDefinitions.Add(new RowDefinition());
            gameSectionGrid.RowDefinitions.Add(new RowDefinition());

            TextBlock nameText = new TextBlock
            {
                Text = "Name: ",
                FontFamily = new FontFamily("Verdana"),
                FontSize = 20,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303030")),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(nameText, 0);
            Grid.SetColumn(nameText, 0);
            gameSectionGrid.Children.Add(nameText);

            nameTextBox = new TextBox
            {
                Style = FindResource("textBoxStyle") as Style,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(nameTextBox, 0);
            Grid.SetColumn(nameTextBox, 1);
            gameSectionGrid.Children.Add(nameTextBox);

            TextBlock difficultyText = new TextBlock
            {
                Text = "Difficulty:",
                FontFamily = new FontFamily("Verdana"),
                FontSize = 20,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303030")),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(difficultyText, 1);
            Grid.SetColumn(difficultyText, 0);
            Grid.SetColumnSpan(difficultyText, 2);
            gameSectionGrid.Children.Add(difficultyText);

            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            RadioButton rdb1 = new RadioButton
            {
                Content = "Easy",
                Style = FindResource("radioButtonStyle") as Style,
                Margin = new Thickness(0),
                IsChecked = true
            };
            rdb1.Click += radiobutton_difficulty_change;
            sp.Children.Add(rdb1);

            RadioButton rdb2 = new RadioButton
            {
                Content = "Medium",
                Style = FindResource("radioButtonStyle") as Style,
                Margin = new Thickness(20, 0, 0, 0)
            };
            rdb2.Click += radiobutton_difficulty_change;
            sp.Children.Add(rdb2);

            RadioButton rdb3 = new RadioButton
            {
                Content = "Hard",
                Style = FindResource("radioButtonStyle") as Style,
                Margin = new Thickness(20, 0, 0, 0)
            };
            rdb3.Click += radiobutton_difficulty_change;
            sp.Children.Add(rdb3);

            Grid.SetRow(sp, 2);
            Grid.SetColumn(sp, 0);
            Grid.SetColumnSpan(sp, 2);
            gameSectionGrid.Children.Add(sp);

            Button btnPlay = new Button
            {
                Style = FindResource("btnPlayStyle") as Style,
                Content = "Play",
                FontFamily = new FontFamily("Verdana"),
                FontSize = 20,
                Width = 150,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };
            btnPlay.Click += btnPlay_click;

            Grid.SetRow(btnPlay, 3);
            Grid.SetColumn(btnPlay, 0);
            Grid.SetColumnSpan(btnPlay, 2);
            gameSectionGrid.Children.Add(btnPlay);

            gFrame.Content = gameSectionGrid;
        }

        // Creating controls for the scoreboard segment
        private void createScoreboardControls()
        {
            Grid sbSectionGrid = new Grid();
            sbSectionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50)});
            sbSectionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30)});
            sbSectionGrid.RowDefinitions.Add(new RowDefinition());

            TextBlock category = new TextBlock
            {
                Text = "Category: ",
                FontFamily = new FontFamily("Verdana"),
                FontSize = 20,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303030")),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 10, 0, 0)
            };
            Grid.SetRow(category, 0);
            Grid.SetColumn(category, 0);
            sbSectionGrid.Children.Add(category);

            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            RadioButton rdb1 = new RadioButton
            {
                Content = "Easy",
                Style = FindResource("radioButtonStyle") as Style,
                Margin = new Thickness(0),
                IsChecked = true
            };
            rdb1.Click += radiobutton_difficulty_change;
            sp.Children.Add(rdb1);

            RadioButton rdb2 = new RadioButton
            {
                Content = "Medium",
                Style = FindResource("radioButtonStyle") as Style,
                Margin = new Thickness(20, 0, 0, 0)
            };
            rdb2.Click += radiobutton_difficulty_change;
            sp.Children.Add(rdb2);

            RadioButton rdb3 = new RadioButton
            {
                Content = "Hard",
                Style = FindResource("radioButtonStyle") as Style,
                Margin = new Thickness(20, 0, 0, 0)
            };
            rdb3.Click += radiobutton_difficulty_change;
            sp.Children.Add(rdb3);

            Grid.SetRow(sp, 1);
            Grid.SetColumn(sp, 1);
            sbSectionGrid.Children.Add(sp);

            spScores = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            for(int i = 0; i < 10; i++)
            {
                spScores.Children.Add(new TextBlock { Text = new Score("Jon Doe", difficulty, 555).ToString(), FontSize = 15 });
            }

            Grid.SetRow(spScores, 2);
            Grid.SetColumn(spScores, 0);
            sbSectionGrid.Children.Add(spScores);

            sbFrame.Content = sbSectionGrid;
        }

        // Game/Scoreboard button click
        private void game_scoreboard_click(object sender, System.EventArgs e)
        {
            Button btn = sender as Button;
            if(btn == btnGame)
            {
                sbFrame.Visibility = Visibility.Hidden;
                gFrame.Visibility = Visibility.Visible;
            }
            else
            {
                sbFrame.Visibility = Visibility.Visible;
                gFrame.Visibility = Visibility.Hidden;
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
