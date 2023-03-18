using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;

namespace PairsGame
{
    public class GameEngine
    {
        private ObservableCollection<List<string>> Board { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

       public GameEngine(int rows, int columns)
        {
            Board = new ObservableCollection<List<string>>();
            Rows = rows;
            Columns = columns;
            InitializeBoard();
        }

       private void InitializeBoard()
        {
            var cardList = CardData.Cards.GetRange(0, Rows * Columns / 2);
            var duplicatedCardList = cardList.Concat(cardList).ToList();

            for (int i = 0; i < Rows; i++)
            {
                List<string> innerList = new List<string>();
                for (int j = 0; j < Columns; j++)
                {
                    innerList.Add(duplicatedCardList[i * Columns + j].Path);
                }
                Board.Add(innerList);
            }
            Random random = new Random();
            RandomExtensions.Shuffle<string>(random, Board);
            CardData.ButtonCards = Board;
        }

        public bool IsMatching(List<KeyValuePair<Button, Button>> cards)
        {
            if (cards[0].Value.DataContext == cards[1].Value.DataContext)
                return true;
            return false;
        }

        public void RemoveMatching(List<KeyValuePair<Button, Button>> cards)
        {
            cards[0].Key.Visibility = System.Windows.Visibility.Hidden;
            cards[0].Value.Visibility = System.Windows.Visibility.Hidden;
            cards[1].Key.Visibility = System.Windows.Visibility.Hidden;
            cards[1].Value.Visibility = System.Windows.Visibility.Hidden;

        }

        public void HideUnmatching(List<KeyValuePair<Button, Button>> cards)
        {
            cards[0].Value.Visibility = System.Windows.Visibility.Hidden;
            cards[1].Value.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
