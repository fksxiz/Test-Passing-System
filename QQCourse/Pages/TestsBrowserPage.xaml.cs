using QQCourse.Data;
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
    /// Логика взаимодействия для TestsBrowserPage.xaml
    /// </summary>
    public partial class TestsBrowserPage : Page
    {
        private ObservableCollection<Tests> tests;
        public TestsBrowserPage()
        {
            InitializeComponent();
            UpdateTests();
        }

        private void UpdateTests()
        {
            if (SearchTextBox.Text.Length < 3)
            {
                    var Tests = Core.Database.Tests;
                    TestsListView.ItemsSource = Tests.ToList();
            }
            else
            {
                    var Tests = Core.Database.Tests.Where(T => T.Name.Contains(SearchTextBox.Text));
                    TestsListView.ItemsSource = Tests.ToList();
            }
        }

        public void CloseAllPage()
        {
            TestFrame.Navigate(null);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTests();
        }

        private void TestsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TestsListView.SelectedIndex >= 0)
            {
                TestPage tp = new TestPage(this,TestsListView.SelectedItem as Data.Tests);
                TestFrame.Navigate(tp);
            }
        }

        private void TestFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void PassedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTests();
        }
    }
}
