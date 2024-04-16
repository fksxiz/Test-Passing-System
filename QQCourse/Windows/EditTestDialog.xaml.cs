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
using System.Windows.Shapes;

namespace QQCourse.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditTestDialog.xaml
    /// </summary>
    public partial class EditTestDialog : Window
    {
/*        public ETDOut DialogResponse { get { return _DialogResponse; } }
        private ETDOut _DialogResponse = null;*/
        public Data.Tests test;
        public EditTestDialog(Data.Tests test)
        {
            InitializeComponent();
            if(test == null)
            {
                this.test = new Data.Tests();
                this.test.Id = Core.VOID;
                this.test.CreatorId = Core.CurrentUser.Id;
                CaptionTextBlock.Text = FindResource("NewTest").ToString();
            }
            else
            {
                this.test = test;
                NameTextBox.Text = test.Name.ToString();
                DescTextBox.Text = test.Description.ToString();
                double min = test.MinScore==null ? 0 : test.MinScore.Value;
                MinScoreSlider.Value=min;
                TimeSpan b = test.Time==null?new TimeSpan(0,0,0): test.Time;
                HoursTextBox.Text = b.Hours.ToString();
                MinutesTextBox.Text = b.Minutes.ToString();
                SecondsTextBox.Text = b.Seconds.ToString();
                CaptionTextBlock.Text = FindResource("EditTest").ToString();
            }
        }

        private bool CheckInfo()
        {
            if (NameTextBox.Text==String.Empty)
            {
                MessageBox.Show(FindResource("NameFieldEmpty").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                NameTextBox.Focus();
                return false;
            }
            if (HoursTextBox.Text == String.Empty)
            {
                MessageBox.Show(FindResource("HHFieldEmpty").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                HoursTextBox.Focus();
                return false;
            }
            if (MinutesTextBox.Text == String.Empty)
            {
                MessageBox.Show(FindResource("MMFieldEmpty").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                MinutesTextBox.Focus();
                return false;
            }
            if (SecondsTextBox.Text == String.Empty)
            {
                MessageBox.Show(FindResource("SSFieldEmpty").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                SecondsTextBox.Focus();
                return false;
            }
            try
            {
                int hh=int.Parse(HoursTextBox.Text);
                int mm=int.Parse(MinutesTextBox.Text);
                int ss=int.Parse(SecondsTextBox.Text);
                TimeSpan timeSpan = new TimeSpan(hh, mm, ss);
            }catch {
                MessageBox.Show(FindResource("OnlyNumbersExc").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            test.Description=DescTextBox.Text;
            test.Name=NameTextBox.Text;
            test.MinScore = Convert.ToInt32(MinScoreSlider.Value);
            test.Time = new TimeSpan(Math.Abs(int.Parse(HoursTextBox.Text)), Math.Abs(int.Parse(MinutesTextBox.Text)), Math.Abs(int.Parse(SecondsTextBox.Text)));
            test.FinishWhenTimeRunsOut = FinishEarlyComboBox.SelectedIndex == 0;
            return true;
        }

        private bool SaveInfo()
        {
            try
            {
                if (test.Id==Core.VOID)
                {
                    Core.Database.Tests.Add(test);
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
            Core.CancelChanges(Core.Database.Tests);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            OkButton.Focus();
            if (CheckInfo() && SaveInfo())
            {
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
