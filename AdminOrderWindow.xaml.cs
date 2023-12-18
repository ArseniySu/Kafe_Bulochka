using Microsoft.EntityFrameworkCore;
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

namespace Kafe_Bulochka
{
    /// <summary>
    /// Логика взаимодействия для AdminOrderWindow.xaml
    /// </summary>
    public partial class AdminOrderWindow : Window
    {
        public AdminOrderWindow(DataBases.Order order)
        {
            InitializeComponent();
            var a = Helper.db.Users.Include(z => z.Orders).FirstOrDefault(u => u.Id == order.Waiter);
            titleTBox.Text = order.DateTimes.ToString();
            titleTBox1.Text = order.Tables.ToString();
            titleTBox11.Text = a.Fname + " " + a.Sname + " " + a.Mname;
            titleTBox111.Text = order.CookingStatus;
            DishesDGrid.ItemsSource = Helper.db.OrderedDishes.Include(q => q.Dishes).Include(q => q.Orders).Where(q => q.OrdersId == order.Id).ToList();

        }

        private void BackbTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
