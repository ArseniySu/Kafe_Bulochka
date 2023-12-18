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

namespace Kafe_Bulochka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LoginTBox.Text) && !string.IsNullOrWhiteSpace(PasswordTBox.Password))
            {
                DataBases.User staff = Auth(LoginTBox.Text, PasswordTBox.Password);
                if (staff != null)
                {
                    Helper.usersession = staff;
                    if (staff.RoleId == 1)
                    {
                        new AdminWindow().Show();
                        this.Close();
                    }
                    if (staff.RoleId == 2)
                    {
                        new CookWindow().Show();
                        this.Close();
                    }
                    if (staff.RoleId == 3)
                    {
                        new WaiterWindow().Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка авторизации", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Введите логин и пароль", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
        public DataBases.User Auth(string login, string password)
        {
            return Helper.db.Users.FirstOrDefault(x => x.Logins == login && x.Passwords == password);
        }
    }
}
