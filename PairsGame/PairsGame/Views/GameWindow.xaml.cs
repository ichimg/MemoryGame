using System;
using System.Collections.Generic;
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
        private int currentLevel { get; set; }
        List<KeyValuePair<Button, Button>> cards;
        List<KeyValuePair<Button, Button>> buttons;
        private GameEngine GameEngine { get; set; }
        public GameWindow(int rows, int columns)
        {
            buttons = new List<KeyValuePair<Button, Button>>(); 
            cards = new List<KeyValuePair<Button, Button>>();
            GameEngine = new GameEngine(rows, columns);
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
            if (cards.Count == 2)
            {
                if (GameEngine.IsMatching(cards))
                {
                    GameEngine.RemoveMatching(cards);
                    buttons.AddRange(cards);
                }
                else
                {
                    GameEngine.HideUnmatching(cards);
                }

                cards.RemoveRange(0, 2);
            }
            //RefreshBoard();
        }

        //private void RefreshBoard()
        //{
        //    if(buttons.Count == 0) return;

        //    if (buttons.Count != CardData.ButtonCards.Count * CardData.ButtonCards[0].Count || currentLevel >= 3)
        //        return;

        //    buttons.Clear();
        //    currentLevel++;

        //    var ButtonCards = CardData.ButtonCards;
        //    Random random = new Random();
        //    foreach(var pair in buttons)
        //    {
        //        pair.Key.Visibility = Visibility.Visible;
        //        var randomRow = random.Next(0, ButtonCards.Count);
        //        while (ButtonCards[randomRow].Count == 0) { randomRow = random.Next(0, ButtonCards.Count); }
        //        var randomImagePath = ButtonCards[randomRow].OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        //        ButtonCards[randomRow].Remove(randomImagePath);
        //        pair.Value.DataContext = new Image
        //        {
        //            Source = new BitmapImage(new Uri(randomImagePath, UriKind.Relative))
        //        };
        //        pair.Value.Visibility = Visibility.Collapsed;
        //    }
        //}

    }
}
