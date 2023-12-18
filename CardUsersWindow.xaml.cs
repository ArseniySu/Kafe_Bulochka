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
using Microsoft.EntityFrameworkCore;

namespace Kafe_Bulochka
{
    /// <summary>
    /// Логика взаимодействия для CardUsersWindow.xaml
    /// </summary>
    public partial class CardUsersWindow : Window
    {
        DataBases.User user;
        public CardUsersWindow(DataBases.User user)
        {
            this.user = user;
            InitializeComponent();
            fioUsersTblok.Text = $"{user.Fname} {user.Sname} {user.Mname}";
            loginTblok.Text = $"л. {user.Logins}";
            pusswordTblok.Text = $"п. {user.Passwords}";
            var a = Helper.db.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            RoleTblok.Text = a.Tittle;
            stworkTblok.Text = $"Статус: {user.Jobstatus}";

            if(user.Photo != null && user.Copys != null)
            {
                photoimage.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])user.Photo));
                copyimage.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])user.Copys));
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DelitBtn_Click(object sender, RoutedEventArgs e)
        {
            user.Jobstatus = "уволен";
            Helper.db.SaveChanges();
            stworkTblok.Text = $"Статус: {user.Jobstatus}";

        }
    }
}
