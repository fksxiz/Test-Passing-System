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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;

namespace QQCourse.Pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            initInfo();
        }

        private bool SettingsVisible=true;
        private ImageParserBase64 ImageParser = new ImageParserBase64();

        public void initInfo()
        {
            if (Core.CurrentUser.Avatar!=null)
            {
                AvatarImage.Source = ImageParser.ParseByteToImage(Core.CurrentUser.Avatar);
            }
            else
            {
                AvatarImage.UpdateDefaultStyle();
            }
            NicknameLabel.Content = Core.CurrentUser.Login;
            LastnameLabel.Content = FindResource("Lastname").ToString()+" "+ Core.CurrentUser.Surname;
            NameLabel.Content = FindResource("Name").ToString() + " " + Core.CurrentUser.Name;
            PatronimicLabel.Content = FindResource("Middlename").ToString() + " " + Core.CurrentUser.Middlename;
            BirthdayLabel.Content = FindResource("Birthday").ToString() + " " + Core.CurrentUser.Birthday;
            RoleLabel.Content = FindResource("Role").ToString() + " " + Core.CurrentUser.Role;
            GenderLabel.Content = FindResource("Gender").ToString() + " " + (Core.CurrentUser.Gender == true ? FindResource("Male").ToString() : FindResource("Female").ToString());
            EmailLabel.Content = FindResource("Email").ToString() + " " + Core.CurrentUser.Email;
            if (Core.CurrentUser.Verified == false|| Core.CurrentUser.Verified==null)
            {
                ConfirmEmailButton.Visibility = Visibility.Visible;
            }
            else
            {
                ConfirmEmailButton.Visibility = Visibility.Hidden;
            }
        }

        private void DBSaveException()
        {
            MessageBox.Show(FindResource("SaveDBException").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            Core.CancelChanges(Core.Database.Users);
        }

        private void NicknameLabel_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void EditLoginButton_Click(object sender, RoutedEventArgs e)
        {
            PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("ChLogin").ToString(), FindResource("ChLoginMessage").ToString(), PrimaryDialog.InputType.Text);
            primaryDialog.Height = 200;
            primaryDialog.ShowDialog();
            if (primaryDialog.DialogResponse != null)
            {
                try
                {
                    Core.CurrentUser.Login = primaryDialog.DialogResponse;
                    Core.Database.SaveChanges();
                }
                catch
                {
                    DBSaveException();
                }
            }
            initInfo();
        }

        private void EditLastnameButton_Click(object sender, RoutedEventArgs e)
        {
            PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("ChLastname").ToString(), FindResource("ChLastnameMessage").ToString(), PrimaryDialog.InputType.Text);
            primaryDialog.Height = 200;
            primaryDialog.ShowDialog();
            if (primaryDialog.DialogResponse != null)
            {
                try
                {
                    Core.CurrentUser.Surname = primaryDialog.DialogResponse;
                    Core.Database.SaveChanges();
                }
                catch
                {
                    DBSaveException();
                }
            }
            initInfo();
        }

        private void EditNameButton_Click(object sender, RoutedEventArgs e)
        {
            PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("ChName").ToString(), FindResource("ChNameMessage").ToString(), PrimaryDialog.InputType.Text);
            primaryDialog.Height = 200;
            primaryDialog.ShowDialog();
            if (primaryDialog.DialogResponse != null)
            {
                try
                {
                    Core.CurrentUser.Name = primaryDialog.DialogResponse;
                    Core.Database.SaveChanges();
                }
                catch
                {
                    DBSaveException();
                }
            }
            initInfo();
        }

        private void EditMiddlenameButton_Click(object sender, RoutedEventArgs e)
        {
            PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("ChMiddlename").ToString(), FindResource("ChMiddlenameMessage").ToString(), PrimaryDialog.InputType.Text);
            primaryDialog.Height = 200;
            primaryDialog.ShowDialog();
            if (primaryDialog.DialogResponse != null)
            {
                try
                {
                    Core.CurrentUser.Middlename = primaryDialog.DialogResponse;
                    Core.Database.SaveChanges();
                }
                catch
                {
                    DBSaveException();
                }
            }
            initInfo();
        }

        private void EditBirthdayButton_Click(object sender, RoutedEventArgs e)
        {
            PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("ChBirthday").ToString(), FindResource("ChBirthdayMessage").ToString(), PrimaryDialog.InputType.Date,230);
            primaryDialog.Height = 230;
            primaryDialog.ShowDialog();
            if (primaryDialog.DialogResponse != null)
            {
                try
                {
                    Core.CurrentUser.Birthday = DateTime.Parse(primaryDialog.DialogResponse);
                    Core.Database.SaveChanges();
                }
                catch
                {
                    DBSaveException();
                }
            }
            initInfo();
        }

        private void EditGenderButton_Click(object sender, RoutedEventArgs e)
        {
            PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("ChGender").ToString(), FindResource("ChGenderMessage").ToString(), PrimaryDialog.InputType.Gender);
            primaryDialog.Height = 200;
            primaryDialog.ShowDialog();
            if (primaryDialog.DialogResponse != null)
            {
                try
                {
                    Core.CurrentUser.Gender = primaryDialog.DialogResponse== FindResource("Male").ToString() ? true:false;
                    Core.Database.SaveChanges();
                }
                catch
                {
                    DBSaveException();
                }
            }
            initInfo();
        }

        private void EditPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("Confirmation").ToString(), FindResource("ConfirmPasswordMessage").ToString(), PrimaryDialog.InputType.Text);
            primaryDialog.Height = 200;
            primaryDialog.ShowDialog();
            if (primaryDialog.DialogResponse != null)
            {
                if (primaryDialog.DialogResponse == Core.CurrentUser.Password)
                {
                    PrimaryDialog d = new PrimaryDialog(FindResource("ChPassword").ToString(), FindResource("ChPasswordMessage").ToString(), PrimaryDialog.InputType.Text);
                    d.Height = 200;
                    d.ShowDialog();
                    if (d.DialogResponse != null)
                    {
                        try
                        {
                            Core.CurrentUser.Password = d.DialogResponse;
                            Core.Database.SaveChanges();
                        }
                        catch
                        {
                            DBSaveException();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(FindResource("InvalidPassword").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                }
                initInfo();
            }
        }

        private void EditEmailButton_Click(object sender, RoutedEventArgs e)
        {
            PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("ChEmail").ToString(), FindResource("ChEmailMessage").ToString(), PrimaryDialog.InputType.Text);
            primaryDialog.Height = 200;
            primaryDialog.ShowDialog();
            if (primaryDialog.DialogResponse != null)
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

                if (regex.IsMatch(primaryDialog.DialogResponse)) {
                    if (Core.Database.Users.Where(u=>u.Email==primaryDialog.DialogResponse).Count()<=0) {
                        try
                        {
                            Core.CurrentUser.Email = primaryDialog.DialogResponse;
                            Core.CurrentUser.Verified = false;
                            Core.Database.SaveChanges();

                        }
                        catch
                        {
                            DBSaveException();
                        }
                    }
                    else
                    {
                        MessageBox.Show(FindResource("EmailAlreadyBusy").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(FindResource("InvalidEmailPattern").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            initInfo() ;
        }

        private void ConfirmEmailButton_Click(object sender, RoutedEventArgs e)
        {
            EmailMessageSender emailMessageSender = new EmailMessageSender(EmailMessageSender.MessageType.CONFIRMATION);
            if (emailMessageSender.SendMessage(Core.CurrentUser.Email))
            {
                PrimaryDialog primaryDialog = new PrimaryDialog(FindResource("EmailVerefication").ToString(), FindResource("InputEmailCode").ToString()+" "+Core.CurrentUser.Email, PrimaryDialog.InputType.Text);
                primaryDialog.Height = 230;
                primaryDialog.ShowDialog();
                if (primaryDialog.DialogResponse != null)
                {
                    if (primaryDialog.DialogResponse == Core.VereficationCode)
                    {
                        try
                        {
                            Core.CurrentUser.Verified = true;
                            Core.Database.SaveChanges();
                            MessageBox.Show(FindResource("SuccefullyVereficated").ToString(), FindResource("Notification").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch
                        {
                            DBSaveException();
                        }
                    }
                    else
                    {
                        MessageBox.Show(FindResource("InvalidCode").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(FindResource("FailedSendCode").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            initInfo();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility visibility = SettingsVisible?Visibility.Hidden:Visibility.Visible;
            SettingsVisible = !SettingsVisible;
            if (Core.CurrentUser.Verified == false || Core.CurrentUser.Verified == null)
            {
                ConfirmEmailButton.Visibility = visibility;
            }
            EditBirthdayButton.Visibility = visibility;
            EditEmailButton.Visibility = visibility;
            EditGenderButton.Visibility = visibility;
            EditLastnameButton.Visibility = visibility;
            EditLoginButton.Visibility = visibility;
            EditMiddlenameButton.Visibility = visibility;
            EditNameButton.Visibility = visibility;
            EditPasswordButton.Visibility = visibility;
            EditAvatarButton.Visibility = visibility;
            //DeleteAvatarButton.Visibility = visibility;
        }

        private void EditAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Image";
            dialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    string filename = dialog.FileName;
                    BitmapImage image = new BitmapImage(new Uri(filename));
                    BitmapSource bitmapSource = image;
                    Core.CurrentUser.Avatar = ImageParser.ParseImageToByte(bitmapSource);
                    Core.Database.SaveChanges();
                }
                catch
                {
                    MessageBox.Show(FindResource("SaveDBExceptionImage").ToString(), FindResource("Error").ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    try
                    {
                        Core.CancelChanges(Core.Database.Users);
                    }
                    catch { }
                }
            }
            initInfo();
        }
    }
}
