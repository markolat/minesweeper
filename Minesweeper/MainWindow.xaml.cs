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
        private string name;
        private string level;
        public MainWindow()
        {
            InitializeComponent();
            btnEasy.Click += Button_click;
            btnMedium.Click += Button_click;
            btnHard.Click += Button_click;
        }

        // Button click event
        private void Button_click(object sender, System.EventArgs e)
        {
            name = txtName.Text;
            if(name == "")
            {
                //TODO dialog window "enter your name"
            }
            Button btn = sender as Button;
            level = btn.Content.ToString().ToLower();
            new GameWindow(this, level, name).Show();
        }
    }
}
