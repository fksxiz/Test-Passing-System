using QQCourse.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace QQCourse.Pages
{
    /// <summary>
    /// Логика взаимодействия для MyTestsPage.xaml
    /// </summary>
    public partial class MyTestsPage : Page
    {
        private List<Page> ActivePages;
        public MyTestsPage()
        {
            InitializeComponent();
            UpdateTests();
            ActivePages = new List<Page>();
        }



        public void UpdateTests()
        {
            if (SearchTextBox.Text.Length < 3)
            {
                var Tests = Core.Database.Tests.Where(T => T.CreatorId == Core.CurrentUser.Id);
                TestsListView.ItemsSource = Tests.ToList();
            }
            else
            {
                var Tests = Core.Database.Tests.Where(T => T.CreatorId == Core.CurrentUser.Id && T.Name.Contains(SearchTextBox.Text));
                TestsListView.ItemsSource = Tests.ToList();
            }
        }

        private void DBSaveException()
        {
            MessageBox.Show(FindResource("SaveDBException").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            Core.CancelChanges(Core.Database.Tests);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTests();
        }



        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            EditTestDialog edt = new EditTestDialog(null);
            edt.ShowDialog();
            UpdateTests();
        }

        private void DeleteTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestsListView.SelectedIndex >= 0)
            {
                if (MessageBox.Show(FindResource("DeleteConfirmation").ToString(), FindResource("Confirmation").ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Data.Tests selected = (TestsListView.SelectedItem as Data.Tests);
                        List<Data.Questions> questions = Core.Database.Questions.Where(x => x.TestId == selected.Id).ToList();
                        List<Data.Results> results = Core.Database.Results.Where(x => x.TestId == selected.Id).ToList();
                        foreach (var a in results)
                        {
                            Core.Database.Results.Remove(a);
                        }

                        foreach (var q in questions)
                        {
                            List<Data.Answers> ans = Core.Database.Answers.Where(x => x.QuestId == q.Id).ToList();

                            foreach (var a in ans)
                            {
                                Core.Database.Answers.Remove(a);
                            }
                            Core.Database.Questions.Remove(q);
                        }
                        Core.Database.Tests.Remove(selected);
                        Core.Database.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show(FindResource("DeleteDBException").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        Core.CancelChanges(Core.Database.Tests);
                        Core.CancelChanges(Core.Database.Questions);
                        Core.CancelChanges(Core.Database.Answers);
                        Core.CancelChanges(Core.Database.Results);
                    }
                    UpdateTests();
                }
            }
        }

        public void CloseAllPage()
        {
            ActivePages.RemoveAll(p => true);
            QeustionsFrame.Navigate(null);
        }

        private void ChangeTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestsListView.SelectedIndex >= 0)
            {
                EditTestDialog edt = new EditTestDialog(TestsListView.SelectedItem as Data.Tests);
                edt.ShowDialog();
                UpdateTests();
            }
        }

        private void ChangeQuestionsTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestsListView.SelectedItem!=null)
            {
                QeustionsFrame.Navigate(new EditQuestionsPage(this,(TestsListView.SelectedItem as Data.Tests).Id));
            }
        }

        private void QeustionsFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void TestsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TestsListView.SelectedIndex >= 0)
            {
                EditTestDialog edt = new EditTestDialog(TestsListView.SelectedItem as Data.Tests);
                edt.ShowDialog();
                UpdateTests();
            }
        }
    }
}
