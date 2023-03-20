using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace PairsGame
{
    public class UserData
    {
        public List<User> Users { get; set; }
        public static List<string> ImagePaths { get; private set; }

        public UserData()
        {
            Users = new List<User>();
            GetUsersFromFiles();
        }

        static UserData()
        {
            ImagePaths = new List<string>();
            LoadImagesPaths();
        }

        private void GetUsersFromFiles()
        {
            var userFilesPaths = Directory.GetFiles(@"../../Data/Users/").Where(x => x != "../../Data/Users/.gitkeep");

            foreach (var userFile in userFilesPaths)
            {
                using (StreamReader streamReader = new StreamReader(userFile))
                {
                    User user = new User(streamReader.ReadLine(), streamReader.ReadLine(), Guid.Parse(streamReader.ReadLine()), Int32.Parse(streamReader.ReadLine()));
                    Users.Add(user);
                }
            }
        }

      
        public static void LoadImagesPaths()
        {
            ImagePaths = Directory.GetFiles(@"../../Data/images/").Select(x => "/PairsGame;component" + x.Substring(5)).ToList();
        }
    }
}
