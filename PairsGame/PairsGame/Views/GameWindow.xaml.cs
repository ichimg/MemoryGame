using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        List<KeyValuePair<Button, Button>> buttons;
        User ActiveUser;
        private GameEngine GameEngine { get; set; }
        int CurrentLevel = 1;
        public GameWindow(User selectedUser, int rows, int columns)
        {
            buttons = new List<KeyValuePair<Button, Button>>();
            ActiveUser = selectedUser;
            cards = new List<KeyValuePair<Button, Button>>();
            GameEngine = new GameEngine(rows, columns);
            cardsForNextLevel = CardData.ButtonCards;

            InitializeComponent();
            Uri uri = new Uri(ActiveUser.ImagePath, uriKind: UriKind.Relative);
            ActiveUserImage.Source = new BitmapImage(uri);
        }

        public GameWindow(User selectedUser, List<List<string>> savedBoard, int currentLevel)
        {
            buttons = new List<KeyValuePair<Button, Button>>();
            ActiveUser = selectedUser;
            cards = new List<KeyValuePair<Button, Button>>();
            GameEngine = new GameEngine(savedBoard);
            cardsForNextLevel = CardData.ButtonCards;
            CurrentLevel = currentLevel;
            InitializeComponent();
            Uri uri = new Uri(ActiveUser.ImagePath, uriKind: UriKind.Relative);
            ActiveUserImage.Source = new BitmapImage(uri);
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
                buttons.AddRange(cards);
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
            MessageBox.Show("Game saved successfully!");
            Close();
        }

        public void Serialize()
        {
            ObjectToSerialize objectToSerialize = new ObjectToSerialize();
            objectToSerialize.Cards = CardData.ButtonCards;
            objectToSerialize.CurrentLevel = CurrentLevel;
            Serializer serializer = new Serializer();
            serializer.SerializeObject($"../../Data/Users/saves/user-{ActiveUser.Name}-{ActiveUser.Guid}-save.txt", objectToSerialize);
        }

        private void NextLevelClick(object sender, RoutedEventArgs e)
        {
            if (buttons.Count != cardsForNextLevel.Count * cardsForNextLevel[0].Count)
            {
                MessageBox.Show(this, "Finish the current game first!", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            if(CurrentLevel >= 3)
            {
                MessageBox.Show(this, "Congratulations! You win!", "Win", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                ActiveUser.CountWin();
                Close();
                return;
            }
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            if (buttons.Count == 0) return;

            CurrentLevel++;
            Random random = new Random();
            var ButtonCards = cardsForNextLevel;
            foreach (var pair in buttons)
            {
                pair.Key.Visibility = Visibility.Visible;
                Panel.SetZIndex(pair.Key, 0);
                Panel.SetZIndex(pair.Value, 0);
                pair.Value.Visibility = Visibility.Collapsed;
            }
            RandomExtensions.Shuffle(random, cardsForNextLevel);
            CardData.ButtonCards = cardsForNextLevel;
            CardItemsControl.ItemsSource = cardsForNextLevel;
            CardItemsControl.Items.Refresh();
            buttons.Clear();
        }

    }
}
