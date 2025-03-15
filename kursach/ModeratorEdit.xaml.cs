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
    /// Interaction logic for ModeratorEdit.xaml
    /// </summary>
    public partial class ModeratorEdit : Page
    {
        private NotificationManager notificationManager;
        int selectedSolutionId;
        public ModeratorEdit()
        {
            InitializeComponent();
            LoadDataToLB();
            notificationManager = new NotificationManager();
        }

        private void LoadDataToLB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var _solvedIssues = db.SolvedIssues.ToList();

                LB_Solutions.ItemsSource = _solvedIssues;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Solutions.SelectedItem != null)
                {
                    var selectedsolutions = LB_Solutions.SelectedItem as Solution;
                    if (selectedsolutions != null)
                    {
                        selectedSolutionId = selectedsolutions.SolutionId;
                    }

                    var _selectedSolutionInDB = db.SolvedIssues.FirstOrDefault(i => i.SolutionId == selectedSolutionId);

                    var notification = new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Решение удалено!",
                        Type = NotificationType.Success
                    };
                    notificationManager.Show(notification);

                    db.SolvedIssues.Remove(_selectedSolutionInDB);
                    db.SaveChanges();
                }
            }
            LoadDataToLB();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Solutions.SelectedItem != null)
                {
                    var selectedsolutions = LB_Solutions.SelectedItem as Solution;
                    if (selectedsolutions != null)
                    {
                        selectedSolutionId = selectedsolutions.SolutionId;
                    }

                    var _selectedIssueInDB = db.SolvedIssues.FirstOrDefault(i => i.SolutionId == selectedSolutionId);

                    _selectedIssueInDB.Description = TB_SolvedIssue.Text;

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

        private void LB_Solutions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Solutions.SelectedItem != null)
                {
                    var selectedsolutions = LB_Solutions.SelectedItem as Solution;
                    if (selectedsolutions != null)
                    {
                        selectedSolutionId = selectedsolutions.SolutionId;
                    }

                    var _selectedIssueInDB = db.SolvedIssues.FirstOrDefault(i => i.SolutionId == selectedSolutionId);

                    TB_SolvedIssue.Text = _selectedIssueInDB.Description;
                }
            }
        }
    }
}
