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
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private NotificationManager notificationManager;
        User _currentUserData;
        public ProfilePage(User CurrentUser)
        {
            InitializeComponent();
            _currentUserData = CurrentUser;
            GetUserData();
            notificationManager = new NotificationManager();
        }

        public void GetUserData()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    var _validateUser = db.Users.FirstOrDefault(u => u.UserId == _currentUserData.UserId);

                    TB_UserLogin.Text = _validateUser.Login;
                    TB_UserPass.Text = _validateUser.Password;
                    Lbl_CurrentRole.Content = _validateUser.Role;
                }
                catch(Exception ex) { MessageBox.Show($"{ex}"); }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var _validateUser = db.Users.FirstOrDefault(u => u.UserId == _currentUserData.UserId);

                _validateUser.Login = TB_UserLogin.Text;
                _validateUser.Password = TB_UserPass.Text;

                var notification = new NotificationContent
                {
                    Title = "Уведомление",
                    Message = "Данные обновлены!",
                    Type = NotificationType.Success
                };
                notificationManager.Show(notification);

                db.SaveChanges();
            }
        }
    }
}
