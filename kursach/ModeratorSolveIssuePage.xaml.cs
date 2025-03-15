using kursach.Data;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ModeratorSolveIssuePage.xaml
    /// </summary>
    public partial class ModeratorSolveIssuePage : Page
    {
        public int selectedIssueId;
        private NotificationManager notificationManager;

        public ModeratorSolveIssuePage(User _currentuser)
        {
            InitializeComponent();

            LoadDataToLB();
        }
         
        private void LoadDataToLB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var _Issues = db.Issues.ToList();

                LB_Issues.ItemsSource = _Issues;
                notificationManager = new NotificationManager();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Issues.SelectedItem != null)
                {
                    var selectedIssue = LB_Issues.SelectedItem as Issue;
                    if (selectedIssue != null)
                    {
                        selectedIssueId = selectedIssue.IssueId;
                    }

                    var _selectedIssueInDB = db.Issues.FirstOrDefault(i => i.IssueId == selectedIssueId);
                    Solution _solution = new Solution { Description = TB_SolvedIssue.Text.ToLower(), _Issue = _selectedIssueInDB};

                    var notification = new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Решение добавлено!",
                        Type = NotificationType.Success
                    };
                    notificationManager.Show(notification);

                    db.SolvedIssues.Add(_solution);
                    db.SaveChanges();
                }



            }
        }
    }
}
