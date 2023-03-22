using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame
{
    internal class Serializer
    {
        public void SerializeObject(string filename, SaveGame objectToSerialize)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, objectToSerialize);
            }
        }

        public SaveGame DeserializeObject(string filename)
        {
            SaveGame objectToSerialize;
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                objectToSerialize = (SaveGame)bFormatter.Deserialize(stream);
            }
            return objectToSerialize;
        }
    }
}
