using System;
using System.Collections.Generic;
using System.Drawing;
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
using QQCourse.Data;

namespace QQCourse.Pages
{
    /// <summary>
    /// Interaction logic for ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {
        private Results res;
        TimeSpan timeToPass;
        TimeSpan time;
        public ResultsPage(List<QuestResult> results, TimeSpan time,TimeSpan timeToPass, Tests test)
        {
            InitializeComponent();
            this.timeToPass = timeToPass;
            this.time = time;
            var (score, maxScore) = CalculateScore(results);
            ResultsListBox.ItemsSource = results;
            TimeLabel.Content = time.ToString("hh':'mm':'ss");
            TimeToPassLabel.Content = timeToPass.ToString("hh':'mm':'ss");
            ScoreLabel.Content = score;
            double correctAnswPercent = score * 1.0 / maxScore * 100.0;
            if (maxScore != 0)
            {
                if (score >= maxScore / 100.0 * test.MinScore)
                {
                    ResLabel.Content = "Проходной балл набран!";
                }
                else
                {
                    ResLabel.Content = "Проходной балл не набран!";
                    ResLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 141, 123));
                }
            }
            MinScoreLabel.Content = maxScore / 100.0 * test.MinScore;
            Results results1 = new Results();
            results1.Id = Core.VOID;
            results1.Score = score;
            results1.TestId = test.Id;
            results1.Time = time;
            results1.UserId = Core.CurrentUser.Id;
            res = results1;
            SaveInfo();
        }

        private (double,int) CalculateScore(List<QuestResult> results)
        {
            int correct = 0;
            int count = 0;
            foreach(var r in results)
            {
                if (r.IsCorrect) correct++;
                count++;
            }
            double score = correct;
            if (timeToPass < time)
            {
                score = correct - correct / 100.0 * 20.0;
            }
            
            return (score, count);
        }

        private bool SaveInfo()
        {
            try
            {
                if (res.Id == Core.VOID)
                {
                    Core.Database.Results.Add(res);
                }
                Core.Database.SaveChanges();
            }
            catch
            {
                DBSaveException();
                return false;
            }
            return true;
        }

        private void DBSaveException()
        {
            MessageBox.Show(FindResource("SaveDBException").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            Core.CancelChanges(Core.Database.Results);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Core.AppMainWindow.CloseAllPage();
        }

        private void ResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
