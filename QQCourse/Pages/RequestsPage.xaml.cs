using QQCourse.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRequests();
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
                    var results = Core.Database.Results.Where(T => T.TestId == currentRequest.TestId && T.UserId == currentRequest.UserId);
                    string testName = currentRequest.Tests.Name;
                    int userId = (int)currentRequest.UserId;
                    foreach (var result in results)
                    {
                        Core.Database.Results.Remove(result);
                    }
                    if (currentRequest.Users.Verified==true) {
                        emailSender.SendMessage(currentRequest.Users.Email, testName, "одобрена");
                    }
                    Core.Database.Requests.Remove(currentRequest);
                    SendNotification(userId, testName);
                    Core.Database.SaveChanges();
                    UpdateRequests();
                }catch
                {
                    Core.CancelChanges(Core.Database.Results);
                    Core.CancelChanges(Core.Database.Requests);
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
                        int userId = (int)currentRequest.UserId;
                        if (currentRequest.Users.Verified == true)
                        {
                            emailSender.SendMessage(currentRequest.Users.Email, testName, "отклонена");
                        }
                        SendNotification(userId, testName,"отклонен");
                        Core.Database.Requests.Remove(currentRequest);
                        Core.Database.SaveChanges();
                        UpdateRequests();
                    }
                    catch
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

        private void SendNotification(int userId, string testName, string requestsState="одобрен")
        {
            try
            {
                Notifications notification = new Notifications();
                notification.UserId = userId;
                notification.Message = $"Ваш запрос на перепрохождение теста {testName} был {requestsState}";
                Core.Database.Notifications.Add(notification);
                Core.Database.SaveChanges();
            }
            catch
            {
                Core.CancelChanges(Core.Database.Notifications);
                DBSaveException();
            }
        }
    }
}
