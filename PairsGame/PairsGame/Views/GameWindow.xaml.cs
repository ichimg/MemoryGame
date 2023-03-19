using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PairsGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        List<KeyValuePair<Button, Button>> cards;
        User ActiveUser;
        private GameEngine GameEngine { get; set; }
        public GameWindow(User selectedUser, int rows, int columns)
        {
            ActiveUser = selectedUser;
            cards = new List<KeyValuePair<Button, Button>>();
            GameEngine = new GameEngine(rows, columns);
            InitializeComponent();
        }

        public GameWindow(User selectedUser, List<List<string>> savedBoard)
        {
            ActiveUser = selectedUser;
            cards = new List<KeyValuePair<Button, Button>>();
            GameEngine = new GameEngine(savedBoard);
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (cards.Count == 2) // useful for clicking repeatedly and fast
                return;          // and the previous click is in Delay before taking actions on the cards

            Button clickedButton = sender as Button;

            FrameworkElement parent = clickedButton.Parent as FrameworkElement;
            Button cardButton = parent.FindName("CardButton") as Button;

            if (cardButton is null) return;
            cards.Add( new KeyValuePair<Button, Button>(clickedButton, cardButton));

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
            Serializer serializer = new Serializer();
            serializer.SerializeObject($"../../Data/Users/saves/user-{ActiveUser.Name}-{ActiveUser.Guid}-save.txt", objectToSerialize);
        }

    }
}
