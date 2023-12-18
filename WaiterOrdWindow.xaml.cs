using Kafe_Bulochka.DataBases;
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
    /// Логика взаимодействия для WaiterOrdWindow.xaml
    /// </summary>
    public partial class WaiterOrdWindow : Window
    {
        private DataBases.Order Order1;
        public WaiterOrdWindow(DataBases.Order order)
        {
            InitializeComponent();
            this.Order1 = order;
            var a = Helper.db.Users.Include(z => z.Orders).FirstOrDefault(u => u.Id == order.Waiter);
            titleTBox.Text = order.DateTimes.ToString();
            titleTBox1.Text = order.Tables.ToString();
            titleTBox11.Text = a.Fname + " " + a.Sname + " " + a.Mname;
            titleTBox2.Text = order.CookingStatus.ToString();
            titleTBox22.Text = order.PaymentMethod.ToString();
            DisheslDGrid.ItemsSource = Helper.db.OrderedDishes.Include(q => q.Dishes).Include(q => q.Orders).Where(q => q.OrdersId == order.Id).ToList();

        }

        private void SavebTN_Click(object sender, RoutedEventArgs e)
        {
            new PaymentWindow(Order1).Show();
            this.Close();
        }

        private void BackbTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
