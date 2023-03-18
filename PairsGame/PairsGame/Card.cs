using System;

namespace PairsGame
{
    public class Card
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public Card(int id, string path) 
        { 
            if(string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            Id = id;
            Path = path;
        }
    }
}
