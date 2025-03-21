﻿using kursach.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Win32;
using Notifications.Wpf;
using SQLitePCL;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace kursach
{
    public partial class IssuePage : Page
    {
        private NotificationManager notificationManager;
        private User _currentUser;
        int selectedSolutionId;
        public IssuePage(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            notificationManager = new NotificationManager();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_IssueDesc.Text))
            {
                var notification = new NotificationContent
                {
                    Title = "Уведомление",
                    Message = "Заполните поле вашей проблемы ниже!",
                    Type = NotificationType.Warning
                };
                notificationManager.Show(notification);
            }
            else
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Issue _newIssue = new Issue()
                    {
                        Description = TB_IssueDesc.Text.ToLower()
                    };
                    db.Add(_newIssue);

                    var notification = new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Проблема добавлена! Ожидайте её решения.",
                        Type = NotificationType.Success
                    };
                    notificationManager.Show(notification);

                    db.SaveChanges();
                }
            }
        }

        private void TB_IssueDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_IssueDesc.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetIssuesFromDB();
        }
        private void GetIssuesFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var _filteredIssues = db.Issues
                                         .Where(i => i.Description
                                         .ToLower().
                                         Contains(TB_IssueDesc.Text))
                                         .ToList();

                LB_Issues.ItemsSource = _filteredIssues;
            }
        }

        private void Button_Details(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Issues.SelectedItem != null)
                {
                    var selectedIssue = LB_Issues.SelectedItem as Issue;
                    if (selectedIssue != null)
                    {
                        int selectedIssueId = selectedIssue.IssueId;

                        var selectedSolution = db.SolvedIssues
                                                .Include(s => s._Issue) 
                                                .FirstOrDefault(s => s.IssueId == selectedIssueId);

                        if (selectedSolution != null)
                        {
                            var notification = new NotificationContent
                            {
                                Title = "Уведомление",
                                Message = $"Решение: {selectedSolution.Description}",
                                Type = NotificationType.Information
                            };
                            notificationManager.Show(notification);
                        }
                    }
                }
            }
        }
    }
}
