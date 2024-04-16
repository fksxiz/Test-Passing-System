using QQCourse.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace QQCourse.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPassingPage.xaml
    /// </summary>
    public partial class TestPassingPage : Page
    {
        private TimeSpan _time;
        private DispatcherTimer _timer;
        private TimeSpan testTime;
        private TimeSpan timeToPass;
        private Tests _test;
        public TestPassingPage(Tests test)
        {
            InitializeComponent();
            _timer=new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += ChangeTimer;
            _timer.Start();
            DataContext = new TestViewModel(test);
            TestLabel.Content = test.Name;
            _time = test.Time;
            testTime = new TimeSpan(0,0,0);
            timeToPass = test.Time;
            _test = test;
        }

        private void ChangeTimer(object sender, EventArgs e)
        {
            if (_time==new TimeSpan(0, 0, 0))
            {
                AddInfo();
                _timer.Stop();
                return;
            }
            testTime = testTime.Add(new TimeSpan(0, 0, 1));
            _time =_time.Subtract(new TimeSpan(0, 0, 1));
            TimeLabel.Content = _time.ToString("hh':'mm':'ss");
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInfo()) return;
            AddInfo();
        }

        private void AddInfo()
        {
            List<QuestResult> res = new List<QuestResult>();
            TestViewModel c = DataContext as TestViewModel;

            foreach (var question in c.Questions)
            {
                bool IsCorrect = question.Answers.Select(answer => answer.IsSelected).SequenceEqual(question.CorrectAnswers);
                List<TextS> selans = new List<TextS>();
                List<TextS> corans = new List<TextS>();
                var sel = question.Answers.Where(answer => answer.IsSelected);
                foreach (var cur in sel)
                {
                    selans.Add(new TextS(cur.Text));
                }
                for (int i = 0; i < question.CorrectAnswers.Count; i++)
                {
                    if (question.CorrectAnswers[i])
                    {
                        corans.Add(new TextS(question.Answers[i].Text));
                    }
                }

                res.Add(new QuestResult(question.Text, IsCorrect, selans, corans));
            }

            ResFrame.Navigate(new ResultsPage(res, testTime, timeToPass, _test));
        }

        private bool CheckInfo()
        {
            TestViewModel c = DataContext as TestViewModel;
            foreach (var question in c.Questions)
            {
                if (question.Answers.Where(answer => answer.IsSelected).Count() <= 0)
                {
                    MessageBox.Show("Вы ответили не на все вопросы!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            return true;
        }

        private void DBSaveException()
        {
            MessageBox.Show(FindResource("SaveDBException").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            Core.CancelChanges(Core.Database.Results);
        }

        private void ResFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
