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
    /// Логика взаимодействия для EditAnsDialog.xaml
    /// </summary>
    public partial class EditAnsDialog : Window
    {
        private Data.Answers answer;
        public EditAnsDialog(Data.Answers answer,int id)
        {
            InitializeComponent();
            if (answer == null)
            {
                this.answer = new Data.Answers();
                this.answer.Id = Core.VOID;
                this.answer.QuestId = id;
                CaptionTextBlock.Text = FindResource("NewTest").ToString();
            }
            else
            {
                this.answer = answer;
                NameTextBox.Text = answer.Answer.ToString();
                CorrectComboBox.Text = answer.Correct==true?FindResource("Correct").ToString(): FindResource("notCorrect").ToString();
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
            if (CorrectComboBox.Text == String.Empty)
            {
                MessageBox.Show(FindResource("CorrectEmpty").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                CorrectComboBox.Focus();
                return false;
            }

            answer.Answer=NameTextBox.Text;
            answer.Correct=CorrectComboBox.Text==FindResource("Correct").ToString()?true:false;
            
            return true;
        }

        private bool SaveInfo()
        {
            try
            {
                if (answer.Id == Core.VOID)
                {
                    Core.Database.Answers.Add(answer);
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            OkButton.Focus();
            if(CheckInfo()&&SaveInfo())
            {
                Close();
            }
        }
    }
}
