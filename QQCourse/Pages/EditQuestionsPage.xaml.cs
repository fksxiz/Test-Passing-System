using QQCourse.Data;
using QQCourse.Windows;
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

namespace QQCourse.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditQuestionsPage.xaml
    /// </summary>
    public partial class EditQuestionsPage : Page
    {
        private int id;
        private CollectionViewSource QuestViewModel { get; set; }
        private CollectionViewSource AnsViewModel { get; set; }
        private Page Page;
        public EditQuestionsPage(Page page,int id)
        {
            InitializeComponent();
            this.id = id;
            Page=page;
            UpdateQuesListView(id);
            UpdateAnsListView();
        }

        private int? GetQuesId()
        {
            if (QuestionsDataGrid.SelectedIndex>=0)
            {
                return (QuestionsDataGrid.SelectedItem as Data.Questions).Id;
            }
            return null;
        }

        private void UpdateQuesListView(int id)
        {
            if (SearchQuesTextBox.Text.Length>=3)
            {
                ObservableCollection<Data.Questions> Questions = new ObservableCollection<Data.Questions>(Core.Database.Questions.Where(q => q.Question.Contains(SearchQuesTextBox.Text) && q.TestId == id));
                QuestionsDataGrid.ItemsSource = Questions;
            }
            else
            {
                ObservableCollection<Data.Questions> Questions = new ObservableCollection<Data.Questions>(Core.Database.Questions.Where(q => q.TestId == id));
                QuestionsDataGrid.ItemsSource = Questions;
            }
        }

        private void UpdateAnsListView()
        {
            int? quesId = GetQuesId();
            if (SearchAnsTextBox.Text.Length >= 3)
            {
                ObservableCollection<Data.Answers> answers = new ObservableCollection<Data.Answers>(Core.Database.Answers.Where(a => a.Answer.Contains(SearchAnsTextBox.Text) && a.QuestId == quesId));
                AnswersDataGrid.ItemsSource = answers;
            }
            else
            {
                ObservableCollection<Data.Answers> answers = new ObservableCollection<Data.Answers>(Core.Database.Answers.Where(a => a.QuestId == quesId));
                AnswersDataGrid.ItemsSource = answers;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateQuesListView(id);
            UpdateAnsListView();
        }

        private void SearchAnsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAnsListView();
        }

        private void AnswersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void QuestionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAnsListView();
        }

        private void AddQuesButton_Click(object sender, RoutedEventArgs e)
        {
            EditQuesDialog editQuesDialog = new EditQuesDialog(null, id);
            editQuesDialog.ShowDialog();
            UpdateQuesListView(id);
            UpdateAnsListView();
        }

        private void DeleteQuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsDataGrid.SelectedIndex >= 0)
            {
                if (MessageBox.Show(FindResource("DeleteConfirmation").ToString(), FindResource("Confirmation").ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Data.Questions selected = (QuestionsDataGrid.SelectedItem as Data.Questions);
                        List<Data.Answers> answers = Core.Database.Answers.Where(x => x.QuestId == selected.Id).ToList();
                        foreach (var a in answers)
                        {
                            Core.Database.Answers.Remove(a);
                        }
                        Core.Database.Questions.Remove(selected);
                        Core.Database.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show(FindResource("DeleteDBException").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        Core.CancelChanges(Core.Database.Questions);
                        Core.CancelChanges(Core.Database.Answers);
                    }
                    UpdateQuesListView(id);
                    UpdateAnsListView();
                }
            }
        }

        private void EditQuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsDataGrid.SelectedIndex>=0)
            {
                EditQuesDialog editQuesDialog = new EditQuesDialog(QuestionsDataGrid.SelectedItem as Questions,id);
                editQuesDialog.ShowDialog();
                UpdateQuesListView(id);
                UpdateAnsListView();
            }
        }

        private void AddAnsButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsDataGrid.SelectedIndex >= 0)
            {
                EditAnsDialog edit = new EditAnsDialog(null, (QuestionsDataGrid.SelectedItem as Questions).Id);
                edit.ShowDialog();
                UpdateAnsListView();
            }
        }

        private void DeleteAnsButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnswersDataGrid.SelectedIndex >= 0)
            {
                if (MessageBox.Show(FindResource("DeleteConfirmation").ToString(), FindResource("Confirmation").ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Data.Answers selected = (AnswersDataGrid.SelectedItem as Data.Answers);
                        Core.Database.Answers.Remove(selected);
                        Core.Database.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show(FindResource("DeleteDBException").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        Core.CancelChanges(Core.Database.Answers);
                    }
                    UpdateAnsListView();
                }
            }
        }

        private void EditAnsButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnswersDataGrid.SelectedIndex >= 0)
            {
                EditAnsDialog edit = new EditAnsDialog((AnswersDataGrid.SelectedItem as Answers), (QuestionsDataGrid.SelectedItem as Questions).Id);
                edit.ShowDialog();
            }
            UpdateAnsListView();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog(true);
        }

        private void CloseDialog(bool NeedUpdate)
        {
            if (Page is MyTestsPage)
            {
                MyTestsPage myTests = Page as MyTestsPage;
                myTests.CloseAllPage();
                if (NeedUpdate)
                {
                    myTests.UpdateTests();
                }
                else
                {
                    myTests.TestsListView.Items.Refresh();
                }
            }
        }

        private void QuestionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAnsListView();
        }
    }
}
