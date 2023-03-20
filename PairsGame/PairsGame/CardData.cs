using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PairsGame
{
    public class CardData
    {
        public static List<Card> Cards { get; private set; }
        public static List<List<string>> ButtonCards { get; set; }

        static CardData() 
        { 
            Cards = new List<Card>(); 
            LoadCardImages();
        }

        private static void LoadCardImages()
        {
            int cardsNo = 0;
            Cards = Directory.GetFiles(@"../../Data/CardImages/")
                .Select(x => new Card(cardsNo++, "/PairsGame;component" + x.Substring(5))).ToList();
            Cards.Shuffle<Card>();
        }
    }
}
