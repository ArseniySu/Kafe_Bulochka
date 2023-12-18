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
using Microsoft.Win32;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using Aspose.Cells.Drawing;
using System.Text.RegularExpressions;

namespace Kafe_Bulochka
{
    /// <summary>
    /// Логика взаимодействия для AddUsersWindow.xaml
    /// </summary>
    public partial class AddUsersWindow : Window
    {
        byte[] image_bytes;
        byte[] image_bytes1;

        public AddUsersWindow()
        {
            
            InitializeComponent();
            RolrCBox.ItemsSource = Helper.db.Roles.ToList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FnameTBox.Text) &&
                !string.IsNullOrWhiteSpace(SnameTBox.Text) &&
                !string.IsNullOrWhiteSpace(LoginTBox.Text) &&
                !string.IsNullOrWhiteSpace(PusswordTBox.Text) &&
                !string.IsNullOrWhiteSpace(RolrCBox.Text) )
            {
                if (Regex.IsMatch(LoginTBox.Text, @"[a-z]$") &&
                Regex.IsMatch(PusswordTBox.Text, @"[a-z]$") &&
                Regex.IsMatch(FnameTBox.Text, @"[а-я]$") &&
                Regex.IsMatch(SnameTBox.Text, @"[а-я]$") &&
                Regex.IsMatch(MnameTBox.Text, @"[а-я]$"))
                {
                    DataBases.User user1 = Helper.db.Users.FirstOrDefault(q => q.Logins == LoginTBox.Text);

                    if (user1 == null)
                    {
                        if(image.Source != null && image1.Source != null)
                        {
                            DataBases.User user = new DataBases.User()
                            {
                                Fname = FnameTBox.Text,
                                Sname = SnameTBox.Text,
                                Mname = MnameTBox.Text,
                                Logins = LoginTBox.Text,
                                Passwords = PusswordTBox.Text,
                                RoleId = int.Parse(RolrCBox.SelectedValue.ToString()),
                                Jobstatus = "Работает",
                                Photo = image_bytes,
                                Copys = image_bytes1,
                            };
                            Helper.db.Users.Add(user);
                            Helper.db.SaveChanges();
                            MessageBox.Show("Регистрация успешна", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Необходимо выбрать изображение", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин уже существует", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Введены недопустимые символы", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }
        private void SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
            openFileDialog.ShowDialog(); // показываем
            byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла
            this.image_bytes = image_bytes;
            image.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])image_bytes));

        }

        private void SelectImage1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
            openFileDialog.ShowDialog(); // показываем
            byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла
            this.image_bytes1 = image_bytes;
            image1.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])image_bytes));
        }
    }
}
