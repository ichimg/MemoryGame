using System;
using System.Windows;


namespace PairsGame
{
    /// <summary>
    /// Interaction logic for CustomGameWindow.xaml
    /// </summary>
    public partial class CustomGameWindow : Window
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public CustomGameWindow()
        {
            InitializeComponent();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(RowsBox.Text) || string.IsNullOrEmpty(ColsBox.Text))
            {
                MessageBox.Show(this, "The rows and columns needs to be set!", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            int rows = Convert.ToInt32(RowsBox.Text);
            int cols = Convert.ToInt32(ColsBox.Text);
            if (rows * cols % 2 == 1)
            {
                MessageBox.Show(this, "The result of multiplying rows and cols must be even!", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            Rows = Convert.ToInt32(RowsBox.Text);
            Cols = Convert.ToInt32(ColsBox.Text);
            Close();
        }
    }
}
