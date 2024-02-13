using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QQCourse
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        private string Captcha;
        private int CaptchaLength = 5;
        public RegistrationWindow()
        {
            InitializeComponent();
            CreateCaptcha();
        }

        private void CreateCaptcha()
        {
            Captcha = "";
            Random Gen = new Random();
            string Alphabet = "1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            for (int i = 0; i < CaptchaLength; i++)
            {
                Captcha += Alphabet[Gen.Next(Alphabet.Length)];
            }
            CaptchaImage.Text = Captcha;
        }

        private void DBSaveException()
        {
            MessageBox.Show(FindResource("SaveDBException").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            Core.CancelChanges(Core.Database.Users);
        }

        private void ChangeCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            CreateCaptcha();
        }

        private void AudioCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow registrationWindow = new AuthWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInfo() && SaveInfo())
            {
                MainWindow mainWindow = new MainWindow();
                Core.CurrentUser = Core.Database.Users
                .SingleOrDefault(U => U.Login == LoginTextBox.Text && U.Password == (PasswordPasswordBox.IsVisible ? PasswordPasswordBox.Password : PasswordTextBox.Text));
                mainWindow.Show();
                this.Close();
            }
        }

        private bool CheckInfo()
        {
            if (CaptchaTextBox.Text != Captcha)
            {
                MessageBox.Show("Капча введена неверно!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                CaptchaTextBox.Focus();
                CreateCaptcha();
                return false;
            }

            if (LoginTextBox.Text == String.Empty)
            {
                MessageBox.Show("Поле логин не заполнено!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                LoginTextBox.Focus();
                return false;
            }

            if (LoginTextBox.Text == String.Empty)
            {
                MessageBox.Show("Поле логин не заполнено!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                LoginTextBox.Focus();
                return false;
            }

            string password = PasswordPasswordBox.IsVisible ? PasswordPasswordBox.Password : PasswordTextBox.Text;
            Regex regex1 = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (password == String.Empty)
            {
                MessageBox.Show("Поле пароль не заполнено!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                PasswordTextBox.Focus();
                PasswordPasswordBox.Focus();
                return false;
            }

            if (!regex1.IsMatch(password))
            {
                MessageBox.Show("Пароль не соответствует паттерну! (Минимум 8 символов один из них должен быть заглавным, один прописным, один цифрой и один спец. символ)", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                PasswordTextBox.Focus();
                PasswordPasswordBox.Focus();
                return false;
            }

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if(EmailTextBox.Text == String.Empty)
            {
                MessageBox.Show("Поле почта не заполнено!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                EmailTextBox.Focus();
                return false;
            }

            if (!regex.IsMatch(EmailTextBox.Text))
            {
                MessageBox.Show(FindResource("InvalidEmailPattern").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                EmailTextBox.Focus();
                return false;
            }

            if (SurnameTextBox.Text == String.Empty)
            {
                MessageBox.Show("Поле фамилия не заполнено!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                SurnameTextBox.Focus();
                return false;
            }

            if (NameTextBox.Text == String.Empty)
            {
                MessageBox.Show(FindResource("NameFieldEmpty").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                NameTextBox.Focus();
                return false;
            }

            if (MiddlenameTextBox.Text == String.Empty)
            {
                MessageBox.Show("Поле отчество не заполнено!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                MiddlenameTextBox.Focus();
                return false;
            }

            if (BirthdayDatePicker.Text == String.Empty)
            {
                MessageBox.Show("Поле дата рождения не заполнено!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                BirthdayDatePicker.Focus();
                return false;
            }

            if (GenderComboBox.Text == String.Empty)
            {
                MessageBox.Show("Поле пол не заполнено!", FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                GenderComboBox.Focus();
                return false;
            }

            try
            {
                if (Core.Database.Users.FirstOrDefault(u => u.Login == LoginTextBox.Text) != null)
                {
                    MessageBox.Show(FindResource("LoginAlreadyBusy").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information); ;
                    LoginTextBox.Focus();
                    return false;
                }
                if (Core.Database.Users.FirstOrDefault(u => u.Email == EmailTextBox.Text) != null)
                {
                    MessageBox.Show(FindResource("EmailAlreadyBusy").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                    EmailTextBox.Focus();
                    return false;
                }
            }
            catch
            {
                MessageBox.Show(FindResource("ConnectDBException").ToString(), FindResource("Error").ToString(),
                        MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            return true;
        }

        private bool SaveInfo()
        {
            try
            {
                Data.Users user = new Data.Users();
                user.Id = Core.VOID;
                user.Login = LoginTextBox.Text;
                user.Name = NameTextBox.Text;
                user.Surname = SurnameTextBox.Text;
                user.Middlename = MiddlenameTextBox.Text;
                user.Role = "User";
                user.Email = EmailTextBox.Text;
                user.Password = PasswordPasswordBox.IsVisible ? PasswordPasswordBox.Password : PasswordTextBox.Text;
                user.Birthday= DateTime.Parse(BirthdayDatePicker.Text);
                user.Gender = GenderComboBox.Text == FindResource("Male").ToString() ? true : false;
                user.Verified = false;
                Core.Database.Users.Add(user);
                Core.Database.SaveChanges();
            }catch
            {
                DBSaveException();
                return false;
            }
            return true;
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

        private void CaptchaImage_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
