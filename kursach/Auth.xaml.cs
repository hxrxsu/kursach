using kursach.Data;
using Notifications.Wpf;
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
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        private NotificationManager notificationManager;

        public Auth()
        {
            InitializeComponent();

            notificationManager = new NotificationManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Reg());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                if(string.IsNullOrEmpty(TB_Login.Text) || string.IsNullOrEmpty(TB_Pass.Text))
                {
                    var notification = new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Заполните все данные!",
                        Type = NotificationType.Warning
                    };
                    notificationManager.Show(notification);
                }
                else
                {
                    try 
                    {
                    var _currentUser = db.Users.FirstOrDefault(u => u.Login == TB_Login.Text && u.Password == TB_Pass.Text);

                    if(_currentUser != null)
                        {
                            var notification = new NotificationContent
                            {
                                Title = "Уведомление",
                                Message = "Вы успешно вошли!",
                                Type = NotificationType.Success
                            };
                            notificationManager.Show(notification);

                            if (_currentUser.Role == "Пользователь")
                            NavigationService.Navigate(new MainPage(_currentUser));
                            else
                            NavigationService.Navigate(new ModeratorPage(_currentUser));
                        }
                        else 
                        {
                            var notification = new NotificationContent
                            {
                                Title = "Уведомление",
                                Message = "Неверный логин или пароль!",
                                Type = NotificationType.Warning
                            };
                            notificationManager.Show(notification);
                        }
                    }
                    catch (Exception ex) { }
                }
            }
        }

        private void TB_Login_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Login.Text = "";
        }

        private void TB_Pass_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Pass.Text = "";
        }
    }
}
