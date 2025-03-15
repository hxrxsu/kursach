using kursach.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Notifications.Wpf;
using Notifications.Wpf.Controls;
using System.Security.Policy;

namespace kursach
{
    /// <summary>
    /// Interaction logic for Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        private NotificationManager notificationManager;
        public Reg()
        {
            InitializeComponent();

            notificationManager = new NotificationManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TB_FLP.Text) || string.IsNullOrEmpty(TB_Login.Text) || string.IsNullOrEmpty(TB_Pass.Text)) 
            {
                var notification = new NotificationContent
                {
                    Title = "Уведомление",
                    Message = "Заполните все данные!",
                    Type = NotificationType.Warning
                };
                notificationManager.Show(notification);
            }
            else if(CB_ModeratorRole.IsChecked == false & CB_UserRole.IsChecked == false) 
            {
                var notification = new NotificationContent
                {
                    Title = "Уведомление",
                    Message = "Выберите роль!",
                    Type = NotificationType.Warning
                };
                notificationManager.Show(notification);
            }
            else
            {
                string _checkedRole = CB_ModeratorRole.IsChecked == true ? "Модератор" : "Пользователь";

                using (ApplicationContext db = new ApplicationContext())
                    {
                        User _newUser = new User()
                        {
                            FLP = TB_FLP.Text,
                            Role = _checkedRole,
                            Login = TB_Login.Text,
                            Password = TB_Pass.Text
                        };
                        db.Users.Add(_newUser);
                        db.SaveChanges();
                    }

                var notification = new NotificationContent
                {
                    Title = "Уведомление",
                    Message = "Вы успешно зарегистрировались",
                    Type = NotificationType.Success
                };
                notificationManager.Show(notification);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TB_FLP_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_FLP.Text = "";
        }

        private void TB_Login_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Login.Text = "";
        }

        private void TB_Pass_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Pass.Text = "";
        }

        private void CB_UserRole_Checked(object sender, RoutedEventArgs e)
        {
            CB_ModeratorRole.IsChecked = false;
        }

        private void CB_ModeratorRole_Checked(object sender, RoutedEventArgs e)
        {
            CB_UserRole.IsChecked = false;
        }
    }
}
