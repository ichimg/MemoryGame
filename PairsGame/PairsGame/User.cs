using System;
using System.IO;
using System.Linq;
using System.Security.Policy;

namespace PairsGame
{
    public class User : IEquatable<User>
    {
        public string Name { get; private set; } 
        public string ImagePath { get; private set; }
        public Guid Guid { get; private set; }

        public User(string name, string imagePath, Guid guid)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if(string.IsNullOrEmpty(imagePath)) throw new ArgumentException(nameof(imagePath));

            Name = name;
            ImagePath = imagePath;
            Guid = guid;
        }

        public void CreateUserFile()
        {
            string fileName = $"../../Data/Users/user-{Name}-{Guid}.txt";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (StreamWriter streamWriter = File.CreateText(fileName))
            {
                streamWriter.WriteLine(Name);
                streamWriter.WriteLine(ImagePath);
                streamWriter.WriteLine(Guid);
            }
        }

        public void DeleteUserFile()
        {
            var FileToDelete = Directory.GetFiles(@"../../Data/Users/")
                .Where(x => x.Equals($"../../Data/Users/user-{Name}-{Guid}.txt")).First();

            if(File.Exists(FileToDelete))
            {
                File.Delete(FileToDelete);
            }
        }

        public bool Equals(User other)
        {
            return Name == other.Name && ImagePath == other.ImagePath;
        }

        public override int GetHashCode()
        {
            return (Name, ImagePath).GetHashCode();
        }
    }
}
