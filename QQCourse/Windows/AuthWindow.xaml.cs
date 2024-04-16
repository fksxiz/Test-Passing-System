using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QQCourse
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {

        public AuthWindow()
        {
            InitializeComponent();
            SetLang(Properties.Settings.Default.Lang);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //BlockingTime = StandartBlockingTime.Subtract(DateTime.Now - Properties.Settings.Default.BlockingStartTime);
        }

        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordPasswordBox.Visibility == Visibility.Visible)
            {
                PasswordTextBox.Text = PasswordPasswordBox.Password;
                PasswordTextBox.Width = PasswordPasswordBox.ActualWidth;
                PasswordPasswordBox.Visibility = Visibility.Hidden;
                PasswordTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordPasswordBox.Password = PasswordTextBox.Text;

                PasswordTextBox.Width = 0;
                PasswordPasswordBox.Visibility = Visibility.Visible;
                PasswordTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void SetLang(int? cur)
        {
            if (cur == null) cur = 0;
            switch (cur)
            {
                case 0:
                    var uri = new Uri("Langs/RussianLanguage.xaml", UriKind.Relative);
                    ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                    break;
                case 1:
                    uri = new Uri("Langs/EnglishLanguage.xaml", UriKind.Relative);
                    resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                    break;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string Login = LoginTextBox.Text;
            string Password = PasswordPasswordBox.Visibility == Visibility.Visible ?
            PasswordPasswordBox.Password : PasswordTextBox.Text;
            try
            {
                Core.CurrentUser = Core.Database.Users
                    .SingleOrDefault(U => U.Login == Login && U.Password == Password);
                if (Core.CurrentUser != null)
                {
                    if (Core.CurrentUser.Verified!=true) {
                        MainWindow Window = new MainWindow();
                        Window.Show();
                        Close();
                    }
                    else
                    {
                        EmailMessageSender emailMessageSender = new EmailMessageSender(true);
                        emailMessageSender.SendMessage(Core.CurrentUser.Email);
                        PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("Confirmation").ToString(), FindResource("InputEmailCode").ToString()+" "+Core.CurrentUser.Email.ToString(),PrimaryDialog.InputType.Text);
                        primaryDialog.ShowDialog();
                        if (primaryDialog.DialogResponse!=null) {
                            if (primaryDialog.DialogResponse == Core.VereficationCode)
                            {
                                MainWindow Window = new MainWindow();
                                Window.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(FindResource("InvalidCode").ToString(), FindResource("Error").ToString(),
                                MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(FindResource("InvalidLoginOrPassword").ToString(), FindResource("Information").ToString(),
                        MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    /*Attempts--;
                    if (Attempts == 0)
                    {
                        //Close();
                        Properties.Settings.Default.BlockingStartTime = DateTime.Now;
                        Properties.Settings.Default.Save();
                        BlockingTime = StandartBlockingTime;
                        Attempts = MaxAttempts;
                    }*/
                }
            }
            catch
            {
                MessageBox.Show(FindResource("ConnectDBException").ToString(), FindResource("Error").ToString(),
                        MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
