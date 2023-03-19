using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly User ActiveUser;
        public PlayWindow(User user)
        {
            InitializeComponent();
            Rows = 5;
            Cols = 6;
            ActiveUser = user;
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

        private void FileNewGameClick(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(ActiveUser, Rows, Cols);
            gameWindow.ShowDialog();
        }

        private void FileOpenGameClick(object sender, RoutedEventArgs e)
        {
            if (!IsAnyGameSaved())
            {
                MessageBox.Show(this, "You don't have any game saved!", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return; 
            }

            var savedBoard = DeserializeCards();

            GameWindow gameWindow = new GameWindow(ActiveUser, savedBoard);
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

        public List<List<string>> DeserializeCards()
        {
            Serializer serializer = new Serializer();
            ObjectToSerialize objectToSerialize = serializer.DeserializeObject($"../../Data/Users/saves/user-{ActiveUser.Name}-{ActiveUser.Guid}-save.txt");
            return objectToSerialize.Cards;
        }

        private bool IsAnyGameSaved()
        {
            var saves = Directory.GetFiles("../../Data/Users/saves/").Where(x=> x == $"../../Data/Users/saves/user-{ActiveUser.Name}-{ActiveUser.Guid}-save.txt").ToList();
            if (saves.Count == 0)
                return false;
            return true;
        }
    }
}
