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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QQCourse.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page
    {
        private EmailMessageSender emailSender;

        public RequestsPage()
        {
            InitializeComponent();
            UpdateRequests();
            emailSender = new EmailMessageSender(EmailMessageSender.MessageType.REQUEST_INFO);
        }

        public void UpdateRequests()
        {
            if (SearchTextBox.Text.Length < 3)
            {
                var Requests = Core.Database.Requests.Where(R => R.Tests.CreatorId == Core.CurrentUser.Id);
                RequestListView.ItemsSource = Requests.ToList();
            }
            else
            {
                var Requests = Core.Database.Requests.Where(R => R.Tests.CreatorId == Core.CurrentUser.Id && R.Tests.Name.Contains(SearchTextBox.Text));
                RequestListView.ItemsSource = Requests.ToList();
            }
        }

        private int GetSelectedIndex()
        {
            return RequestListView.SelectedIndex;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRequests();
        }

        private void TestsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void RequestListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AgreeButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestListView.SelectedIndex >= 0)
            {
                try
                {
                    Requests currentRequest = RequestListView.SelectedItem as Requests;
                    var results = Core.Database.Results.Where(T=>T.TestId == currentRequest.TestId && T.UserId == currentRequest.UserId);
                    string testName = currentRequest.Tests.Name;

                    foreach (var result in results)
                    {
                        Core.Database.Results.Remove(result);
                    }
                    Core.Database.Requests.Remove(currentRequest);
                    Core.Database.SaveChanges();
                    emailSender.SendMessage(Core.CurrentUser.Email, testName,"одобрена");
                    UpdateRequests();
                }catch (Exception ex)
                {
                    Core.CancelChanges(Core.Database.Requests);
                    Core.CancelChanges(Core.Database.Results);
                    DBSaveException();
                }
            }
        }

        private void DenyButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestListView.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Вы уверены что хотите отклонить запрос?", FindResource("Confirmation").ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var currentRequest = RequestListView.SelectedItem as Requests;
                        string testName = currentRequest.Tests.Name;
                        Core.Database.Requests.Remove(currentRequest);
                        Core.Database.SaveChanges();
                        emailSender.SendMessage(Core.CurrentUser.Email, testName, "отклонена");
                        UpdateRequests();
                    }
                    catch (Exception ex)
                    {
                        Core.CancelChanges(Core.Database.Requests);
                        Core.CancelChanges(Core.Database.Results);
                        DBSaveException();
                    }
                }
            }
        }

        private void DBSaveException()
        {
            MessageBox.Show(FindResource("SaveDBException").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            Core.CancelChanges(Core.Database.Requests);
        }
    }
}
