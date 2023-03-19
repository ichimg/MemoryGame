using System;
using System.Runtime.Serialization;

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

        public Card(SerializationInfo info, StreamingContext ctxt)
        {
            Id = (int)info.GetValue("Id", typeof(int));
            Path = (string)info.GetValue("Path", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", Id);
            info.AddValue("Path", Path);
        }
    }
}
