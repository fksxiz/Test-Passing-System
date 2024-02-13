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
using System.Windows.Shapes;
using static QQCourse.PrimaryDialog;

namespace QQCourse
{
    /// <summary>
    /// Логика взаимодействия для PrimaryDialog.xaml
    /// </summary>
    public partial class PrimaryDialog : Window
    {
        public enum InputType
        {
            Text,
            Date,
            Gender
        }
        private InputType inputType;

        public string DialogResponse {

            get
            {
                return _DialogResponse;
            }
        }

        private string _DialogResponse { get; set; }

        public PrimaryDialog(string Caption="Диалог",string Message = "Сообщение", InputType inputType=InputType.Text,int Heigth=200)
        {
            InitializeComponent();
            CaptionTextBlock.Text = Caption;
            MessageTextBlock.Text = Message;
            switch(inputType)
            {
                case InputType.Date:
                    InputDatePicker.Width=290;
                    InputDatePicker.Visibility=Visibility.Visible;
                    InputTextBox.Visibility=Visibility.Hidden;
                    break;
                case InputType.Gender:
                    InputComboBox.Width = 290;
                    InputComboBox.Visibility = Visibility.Visible;
                    InputTextBox.Visibility = Visibility.Hidden;
                    break;
            }
            this.inputType = inputType;
        }
        //220
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string r="";
            switch (inputType)
            {
                case InputType.Text:
                        r = InputTextBox.Text;
                    break;
                case InputType.Date:
                    r=InputDatePicker.Text;
                    break;
                case InputType.Gender:
                    r=InputComboBox.Text;
                    break;
            }
            if (r == String.Empty)
            {
                MessageBox.Show("Поле ввода не заполнено!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            _DialogResponse =r;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
