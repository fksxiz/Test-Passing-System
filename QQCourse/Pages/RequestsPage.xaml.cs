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
        public RequestsPage()
        {
            InitializeComponent();
            UpdateRequests();
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

        }

        private void TestsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void RequestListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AgreeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DenyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
