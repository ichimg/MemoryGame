using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PairsGame
{
    public class CardData
    {
        public static List<Card> Cards { get; private set; }
        public static ObservableCollection<List<string>> ButtonCards { get; set; }

        static CardData() 
        { 
            Cards = new List<Card>(); 
            LoadCardImages();
        }

        private static void LoadCardImages()
        {
            int cardsNo = 0;
            Cards = Directory.GetFiles(@"../../Data/CardImages/").Select(x => new Card(cardsNo++, "/PairsGame;component" + x.Substring(5))).ToList();
            Random random = new Random();
            Cards.OrderBy(x => random.Next());
        }
    }
}
