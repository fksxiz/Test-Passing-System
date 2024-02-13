﻿using QQCourse.Data;
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

namespace QQCourse.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        private Page Page;
        private Tests Test;
        public TestPage(Page page, Tests test)
        {
            InitializeComponent();
            this.Page = page;
            Test = test;
            NameTextBlock.Text = test.Name;
            DescTextBlock.Text = test.Description;
            TimeTextBlock.Text = test.Time.ToString();
            MinScoreTextBlock.Text = test.MinScore.ToString();
            AuthorTextBlock.Text = Core.Database.Users.FirstOrDefault(u => u.Id == test.CreatorId).Login;
            if (Core.Database.Results.Count(r=>r.TestId==Test.Id&&r.UserId==Core.CurrentUser.Id)>0)
            {
                Results result = Core.Database.Results.FirstOrDefault(r => r.TestId == Test.Id && r.UserId == Core.CurrentUser.Id);
                ContentScrollViewer.Visibility = Visibility.Hidden;
                ContentDockPanel.Visibility = Visibility.Visible;
                Name2TextBlock.Text = test.Name;
                ScoreTextBlock.Text = result.Score.ToString();
                PassingTimeTextBlock.Text = result.Time.ToString("hh':'mm':'ss");
                MinScore2TextBlock.Text = (test.Questions.Count(q => q.Answers.Count() > 0) / 100.0 * test.MinScore).ToString();
                Author2TextBlock.Text = Core.Database.Users.FirstOrDefault(u => u.Id == test.CreatorId).Login;
                int c = test.Questions.Count(q => q.Answers.Count() > 0);
                if (c != 0)
                {
                    if (c / 100.0 * test.MinScore <= result.Score * 1.0 / c * 100.0)
                    {
                        PassedTextBlock.Text = "Проходной балл набран!";
                    }
                    else
                    {
                        PassedTextBlock.Text = "Проходной балл не набран!";
                        PassedTextBlock.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 141, 123));
                    }
                }
            }
        }

        private void CloseDialog()
        {
            if (Page is TestsBrowserPage)
            {
                TestsBrowserPage myTests = Page as TestsBrowserPage;
                myTests.CloseAllPage();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog();
        }

        private void TestFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            TestPassingPage testPassingPage = new TestPassingPage(Test);
            TestFrame.Navigate(testPassingPage);
        }
    }
}