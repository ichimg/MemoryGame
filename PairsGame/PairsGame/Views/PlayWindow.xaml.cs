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

namespace PairsGame
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {
        private int Rows { get; set ; }
        private int Cols { get; set; }
        public PlayWindow()
        {
            InitializeComponent();
            Rows = 5;
            Cols = 6;
        }

        private void FileExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HelpAboutClick(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void FileNeGameClick(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(Rows, Cols);
            gameWindow.ShowDialog();
        }

        private void OptionsStandardClick(object sender, RoutedEventArgs e)
        {
            Rows = 5;
            Cols = 6;
            MessageBox.Show($"Game mode set at: {Rows * Cols} cards ");
        }

        private void OptionsCustomClick(object sender, RoutedEventArgs e)
        {
            CustomGameWindow customGameWindow = new CustomGameWindow();
            customGameWindow.ShowDialog();
            Rows = customGameWindow.Rows;
            Cols = customGameWindow.Cols;
        }
    }
}
