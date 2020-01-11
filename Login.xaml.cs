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

namespace TelerikWpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LoginDataContext cl = new LoginDataContext();
        public Login()
        {
            InitializeComponent();
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            email2.Visibility = System.Windows.Visibility.Collapsed;
            email.Visibility = System.Windows.Visibility.Visible;
            email.Focus();

        }
        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            password2.Visibility = System.Windows.Visibility.Collapsed;
            password.Visibility = System.Windows.Visibility.Visible;
            password.Focus();

        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(email.Text))
            {
                email.Visibility = System.Windows.Visibility.Collapsed;
                email2.Visibility = System.Windows.Visibility.Visible;

            }
        }
        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(password.Text))
            {
                password.Visibility = System.Windows.Visibility.Collapsed;
                password2.Visibility = System.Windows.Visibility.Visible;

            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var logi = (from l in cl.login
                            //where l.username == email.Text
                        select l).SingleOrDefault();

            if (logi.username == email.Text && logi.pass == password.Text)
            {

                error.Visibility = Visibility.Hidden;
                this.Hide();
                MainWindow f = new MainWindow();
                f.Show();
            }
            else
                error.Visibility = Visibility.Visible;
        }

        private void keyDown(object sender, KeyEventArgs e)
        {

            error.Visibility = Visibility.Hidden;
        }

        

    }

}
