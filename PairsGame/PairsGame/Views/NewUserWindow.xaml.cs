using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PairsGame
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        private int currentAvatar;
        public Delegate UpdateList;
        public NewUserWindow()
        {
            InitializeComponent();
            AvatarImage.Source = new BitmapImage(new Uri(UserData.ImagePaths[currentAvatar], uriKind: UriKind.Relative));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NextImageButtonClick(object sender, RoutedEventArgs e)
        {
            if (currentAvatar + 1 > UserData.ImagePaths.Count - 1)
            {
                currentAvatar = 0;
            }
            else
            { 
                currentAvatar++;
            }

            AvatarImage.Source = new BitmapImage(new Uri(UserData.ImagePaths[currentAvatar], uriKind: UriKind.Relative));
        }

        private void PreviousImageButtonClick(object sender, RoutedEventArgs e)
        {
            if (currentAvatar - 1 < 0)
            {
                currentAvatar = UserData.ImagePaths.Count - 1;
            }
            else
            {
                currentAvatar--;
            }
            AvatarImage.Source = new BitmapImage(new Uri(UserData.ImagePaths[currentAvatar], uriKind: UriKind.Relative));
        }

        private void EnterButtonPressed(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            Guid guid= Guid.NewGuid();
            User newUser = new User(NameTextBox.Text, UserData.ImagePaths[currentAvatar], guid, 0);
            newUser.CreateUserFile();
            LogIn LogInWindow = Application.Current.MainWindow as LogIn;
            LogInWindow.AddUser(newUser);
            UpdateList.DynamicInvoke();
            MessageBox.Show(this, "Your account has been created successfully!", "Account", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
