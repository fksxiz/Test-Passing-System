using QQCourse.Data;
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
    /// Логика взаимодействия для EditQuesDialog.xaml
    /// </summary>
    public partial class EditQuesDialog : Window
    {
        private Data.Questions question;
        public EditQuesDialog(Data.Questions question, int id)
        {
            InitializeComponent();
            if (question == null)
            {
                this.question = new Data.Questions();
                this.question.Id = Core.VOID;
                this.question.TestId = id;
                CaptionTextBlock.Text = FindResource("NewTest").ToString();
            }
            else
            {
                this.question = question;
                NameTextBox.Text = question.Question.ToString();
                TypeComboBox.Text = question.Type;
                CaptionTextBlock.Text = FindResource("EditTest").ToString();
            }

        }

        private bool CheckInfo()
        {
            if (NameTextBox.Text == String.Empty)
            {
                MessageBox.Show(FindResource("NameFieldEmpty").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                NameTextBox.Focus();
                return false;
            }
            if (TypeComboBox.Text == String.Empty)
            {
                MessageBox.Show(FindResource("CorrectEmpty").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                TypeComboBox.Focus();
                return false;
            }

            question.Question = NameTextBox.Text;
            question.Type = TypeComboBox.Text;

            return true;
        }

        private bool SaveInfo()
        {
            try
            {
                if (question.Id == Core.VOID)
                {
                    Core.Database.Questions.Add(question);
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
            if (CheckInfo() && SaveInfo()) Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
