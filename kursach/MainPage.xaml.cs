using kursach.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kursach
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(User CurrentUser)
        {
            InitializeComponent();

            _currentUser = CurrentUser;
        }
        User _currentUser;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BTN_Back_Click(object sender, RoutedEventArgs e)
        {
            BTN_Back.Visibility = Visibility.Collapsed;
            PageFrame.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageFrame.Visibility = Visibility.Visible;
            BTN_Back.Visibility = Visibility.Visible;
            PageFrame.Navigate(new ProfilePage(_currentUser));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PageFrame.Visibility = Visibility.Visible;
            BTN_Back.Visibility = Visibility.Visible;
            PageFrame.Navigate(new IssuePage(_currentUser));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth());
        }
    }
}
