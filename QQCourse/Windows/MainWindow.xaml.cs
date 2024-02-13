using QQCourse.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QQCourse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Page> ActivePages;
        private int CurrentPageIndex;
        private bool isOpen = true;
        private int curTheme;
        private int curLang;
        protected System.Timers.Timer _Timer;
        protected System.Timers.Timer _Timer1;
        public MainWindow()
        {
            InitializeComponent();
            curTheme = Properties.Settings.Default.Theme;
            curLang = Properties.Settings.Default.Lang;
            ActivePages = new List<Page>();
            CurrentPageIndex = -1;
            Core.AppMainWindow = this;
            sideMenuDockPanel.Width = 400;
            _Timer = new System.Timers.Timer(500);
            _Timer.Elapsed += OnTimer;
            _Timer.AutoReset = false;
            _Timer.Enabled = false;
            _Timer1 = new System.Timers.Timer(500);
            _Timer1.Elapsed += OnTimer1;
            _Timer1.AutoReset = false;
            _Timer1.Enabled = false;
            SetTheme(curTheme);
            SetLang(curLang);
            if (Core.CurrentUser.Birthday.Value.Date==DateTime.Now.Date)
            {
                WelcomeText.Text = "С днём рождения!";
            }
            else
            {
                WelcomeText.Text = "Добро пожаловать!";
            }
        }

        private int GetCurrentPageIndexByType(Type PageType)
        {
            int Index = ActivePages.Count - 1;
            while (Index > 0 && ActivePages[Index].GetType() != PageType)
            {
                Index--;
            }
            return Index;
        }

        public void ShowPage(Type PageType)
        {
            Page Page;
            if (PageType != null)
            {
                CurrentPageIndex = GetCurrentPageIndexByType(PageType);
                if (CurrentPageIndex < 0)
                {
                    Page = (Page)Activator.CreateInstance(PageType);
                    ActivePages.Add(Page);
                    CurrentPageIndex = ActivePages.Count - 1;
                }
                else
                {
                    Page = ActivePages[CurrentPageIndex];
                }
                RootFrame.Navigate(Page);
                WelcomeText.Visibility = Visibility.Hidden;
            }
        }

        public void CloseAllPage()
        {
            ActivePages.RemoveAll(p => true);
            CurrentPageIndex = -1;
            RootFrame.Navigate(null);
            if (Core.CurrentUser.Birthday.Value.Date == DateTime.Now.Date)
            {
                WelcomeText.Text = "С днём рождения!";
            }
            else
            {
                WelcomeText.Text = "Добро пожаловать!";
            }
        }

        private void RootFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {

        }

        private void SetTheme(int cur)
        {
            //if (cur == null) cur = 0;
            switch (cur)
            {
                case 0:
                    var uri = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
                    ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                    ThemeButton.Content = "D";
                    break;
                case 1:
                    uri = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
                    resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                    ThemeButton.Content = "L";
                    break;
            }
            Properties.Settings.Default.Theme = cur;
            Properties.Settings.Default.Save();
        }

        private void SetLang(int cur)
        {
            switch (cur)
            {
                case 0:
                    var uri = new Uri("Langs/RussianLanguage.xaml", UriKind.Relative);
                    ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                    LangButton.Content = "RU";
                    break;
                case 1:
                    uri = new Uri("Langs/EnglishLanguage.xaml", UriKind.Relative);
                    resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                    LangButton.Content = "EN";
                    break;
            }
            Properties.Settings.Default.Lang = cur;
            Properties.Settings.Default.Save();
            RootFrame.UpdateLayout();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e) {

                curTheme++;
                if (curTheme > 1) curTheme = 0;
                SetTheme(curTheme);
            Properties.Settings.Default.Theme = curTheme;
            Properties.Settings.Default.Save();
        }

        private void ChangeButtonsStyles(Button button)
        {
            myProfileButton.Style = (Style)FindResource("SideMenuButton");
            myTestsButton.Style = (Style)FindResource("SideMenuButton");
            TestsBrowserButton.Style = (Style)FindResource("SideMenuButton");
            if(button!= null) { 
                button.Style = (Style)FindResource("SelectedSideMenuButton");
            }
        }

        private void myProfileButton_Click(object sender, RoutedEventArgs e)
        {
            CloseAllPage();
            ShowPage(typeof(Pages.ProfilePage));
            ChangeButtonsStyles(myProfileButton);
        }

        private void myTestsButton_Click(object sender, RoutedEventArgs e)
        {
            CloseAllPage();
            ShowPage(typeof(Pages.MyTestsPage));
            ChangeButtonsStyles(myTestsButton);
        }

        private void TestsBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            CloseAllPage();
            ShowPage(typeof(Pages.TestsBrowserPage));
            ChangeButtonsStyles(TestsBrowserButton);
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow a = new AuthWindow();
            Core.CurrentUser = null;
            a.Show();
            Close();
        }

        private void clodePageButton_Click(object sender, RoutedEventArgs e)
        {
            CloseAllPage();
            WelcomeText.Visibility = Visibility.Visible;
            ChangeButtonsStyles(null);
        }

        protected virtual void OnTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                Grid.SetColumn(RootFrame, 0);
                Grid.SetColumnSpan(RootFrame, 3);
                Grid.SetColumn(WelcomeText, 0);
                Grid.SetColumnSpan(WelcomeText, 3);
                Grid.SetColumn(sideMenuDockPanel, 0);
            }));

        }

        protected virtual void OnTimer1(Object source, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                menuPageButton.IsEnabled = true;
            }));

        }

        private void menuPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (isOpen)
            {

                beginSideMenuAnimation(400, 0);
                _Timer.Start();
                menuPageButton.IsEnabled = false;
                _Timer1.Start();
            }
            else
            {
                beginSideMenuAnimation(0, 400);
                Grid.SetColumn(RootFrame, 2);
                Grid.SetColumn(WelcomeText, 2);
                menuPageButton.IsEnabled = false;
                _Timer1.Start();
                //Grid.SetColumnSpan(RootFrame, 0);
            }
            isOpen = !isOpen;
        }

        private void beginSideMenuAnimation(int from,int to)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(0.5)));
            widthAnimation.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn };
            sideMenuDockPanel.BeginAnimation(WidthProperty, widthAnimation);
        }

        private void LangButton_Click(object sender, RoutedEventArgs e)
        {
            curLang++;
            if (curLang > 1) curLang = 0;
            SetLang(curLang);
        }

        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
