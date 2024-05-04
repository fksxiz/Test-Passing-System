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
    /// Логика взаимодействия для NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Page
    {
        public NotificationsPage()
        {
            InitializeComponent();
            UpdateListBox();
            RemoveNotifications();
        }

        private void UpdateListBox()
        {
            NotificationsListBox.ItemsSource = Core.AppMainWindow.notifications;
        }

        private void RemoveNotifications()
        {
            try
            {
                var notifications = Core.Database.Notifications.Where(n => n.UserId == Core.CurrentUser.Id);
                foreach (var notification in notifications)
                {
                    Core.Database.Notifications.Remove(notification);
                }
                Core.Database.SaveChanges();
            }
            catch
            {
                Core.CancelChanges(Core.Database.Notifications);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Core.AppMainWindow.CloseAllPage();
        }

        private void NotificationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
