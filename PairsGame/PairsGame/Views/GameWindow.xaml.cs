using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PairsGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        List<KeyValuePair<Button, Button>> cards;
        List<List<string>> cardsForNextLevel;
        int CardsFound;
        User ActiveUser;
        private GameEngine GameEngine { get; set; }
        int CurrentLevel = 1;
        public GameWindow(User selectedUser, int rows, int columns)
        {
            ActiveUser = selectedUser;
            cards = new List<KeyValuePair<Button, Button>>();
            GameEngine = new GameEngine(rows, columns);
            cardsForNextLevel = CardData.ButtonCards;

            InitializeComponent();
            Uri uri = new Uri(ActiveUser.ImagePath, uriKind: UriKind.Relative);
            ActiveUserImage.Source = new BitmapImage(uri);
            SetResourceReference(BackgroundProperty, SystemColors.ControlBrushKey);
            userLabel.Content = ActiveUser.Name;
            
            currentLevelLabel.Content = CurrentLevel;
        }

        public GameWindow(User selectedUser, List<List<string>> savedBoard, int currentLevel, int cardsFound)
        {
            ActiveUser = selectedUser;
            cards = new List<KeyValuePair<Button, Button>>();
            GameEngine = new GameEngine(savedBoard);
            cardsForNextLevel = CardData.ButtonCards;
            CurrentLevel = currentLevel;
            CardsFound = cardsFound;
            InitializeComponent();
            Uri uri = new Uri(ActiveUser.ImagePath, uriKind: UriKind.Relative);
            ActiveUserImage.Source = new BitmapImage(uri);
            userLabel.Content = ActiveUser.Name;
            currentLevelLabel.Content = CurrentLevel;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (cards.Count == 2) // useful for when clicking fast
                return;          // and the previous click is in Delay before taking actions on the cards

            Button clickedButton = sender as Button;

            FrameworkElement parent = clickedButton.Parent as FrameworkElement;
            Button cardButton = parent.FindName("CardButton") as Button;

            if (cardButton is null) return;
            cards.Add(new KeyValuePair<Button, Button>(clickedButton, cardButton));

            cardButton.Visibility = Visibility.Visible;

            checkCards();
        }

        private async void checkCards()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            if (cards.Count != 2) return;

            if (GameEngine.IsMatching(cards))
            {
                GameEngine.RemoveMatching(cards);
                CardsFound += 2;
                CardData.ButtonCards = CardData.ButtonCards.Select(x => x.Where(card => card != cards[0].Value.DataContext as string).ToList()).ToList();
            }
            else
            {
                GameEngine.HideUnmatching(cards);
            }

                cards.RemoveRange(0, 2);
        }

        private void FileSaveGameClick(object sender, RoutedEventArgs e)
        {
            Serialize();
            MessageBox.Show(this, "Game saved successfully!", "Save", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            Close();
        }

        public void Serialize()
        {
            SaveGame saveGame = new SaveGame();
            saveGame.Cards = CardData.ButtonCards;
            saveGame.CurrentLevel = CurrentLevel;
            saveGame.CardsFound = CardsFound;
            Serializer serializer = new Serializer();
            serializer.SerializeObject($"../../Data/Users/saves/user-{ActiveUser.Name}-{ActiveUser.Guid}-save.txt", saveGame);
        }

        private void NextLevelClick(object sender, RoutedEventArgs e)
        {
            if (CardsFound != cardsForNextLevel.Count * cardsForNextLevel[0].Count)
            {
                MessageBox.Show(this, "Finish the current level first!", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            if(CurrentLevel >= 3)
            {
                MessageBox.Show(this, "Congratulations! You win!", "Win", MessageBoxButton.OK, MessageBoxImage.Information);
                ActiveUser.CountWin();
                Close();
                return;
            }
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            CardsFound = 0;
            CurrentLevel++;
            currentLevelLabel.Content = CurrentLevel;
            cardsForNextLevel.Shuffle();

            CardData.ButtonCards = cardsForNextLevel;
            CardItemsControl.ItemsSource = cardsForNextLevel;

            CardItemsControl.Items.Refresh();
        }

    }
}
