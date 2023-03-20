using System.Windows;

namespace PairsGame.Views
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow()
        {
            UserData userData = new UserData();
            InitializeComponent();
            StatisticsView.ItemsSource = userData.Users;
        }

    }
}
