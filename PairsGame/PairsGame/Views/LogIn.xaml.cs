using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace PairsGame
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public delegate void RefreshListBox();
        public event RefreshListBox RefreshListEvent;
        private UserData userData { get; set; }
        private User SelectedUser { get; set; } 
        public LogIn()
        {
            InitializeComponent();
            userData= new UserData();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(UserListBox.SelectedItem is User)) return;

            User selectedUser = UserListBox.SelectedItem as User;
            Uri uri = new Uri(@userData.Users.First(user => user.Name == selectedUser.Name).ImagePath, uriKind:UriKind.Relative);
            userImage.Source = new BitmapImage(uri);
            SelectedUser = selectedUser;
        }

        private void NewUserButtonClick(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow();
            RefreshListEvent += new RefreshListBox(UpdateListBox);
            newUserWindow.UpdateList = RefreshListEvent;
            newUserWindow.ShowDialog();
        }

        private void DeleteUserButtonClick(object sender, RoutedEventArgs e)
        {
            if (!userData.Users.Contains(SelectedUser)) return;

            userData.Users.Remove(SelectedUser);
            UserListBox.ItemsSource = userData.Users;
            UserListBox.Items.Refresh();
            SelectedUser.DeleteUserFile();
        }

        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            if(SelectedUser is null)
            {
                MessageBox.Show(this, "A user needs to be selected!", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }    
            PlayWindow playWindow = new PlayWindow(SelectedUser);
            playWindow.Show();
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateListBox()
        {
            UserListBox.ItemsSource = userData.Users;
            UserListBox.Items.Refresh();
        }

        public void AddUser(User user)
        {
            userData.Users.Add(user);
        }   
    }
}
