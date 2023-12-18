using Kafe_Bulochka.DataBases;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
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

namespace Kafe_Bulochka
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public AddOrderWindow()
        {
            

            InitializeComponent();

            var a = Helper.usersession;
            WaiterTbox.Text = a.Fname + " " + a.Sname + " " + a.Mname;
            DanetimeTbox.Text = DateTime.Now.ToString();



            NumberTablesCBox.ItemsSource = new Tablenum[] {
                new Tablenum{ title = 1},
                new Tablenum{ title = 2},
                new Tablenum{ title = 3},
                new Tablenum{ title = 4},
                new Tablenum{ title = 5},
                new Tablenum{ title = 6},
                new Tablenum{ title = 7},
                new Tablenum{ title = 8},
                new Tablenum{ title = 9},
                new Tablenum{ title = 10},
                
            };

            NumberTablesCBox.SelectedIndex = 0;

            InitData();

        }
        public void InitData()
        {
            if (Helper.order != null)
            {
                DishDgrid.ItemsSource = Helper.db.OrderedDishes.Include(q => q.Dishes).Include(q => q.Orders).Where(q => q.OrdersId == Helper.order.Id).ToList();

            }

        }


        private void qqq_Click(object sender, RoutedEventArgs e)
        {
            www.IsEnabled = true;
            if (int.Parse(qwe.Text) != 1)
            {
               var a = int.Parse(qwe.Text);
                a -= 1;
                qwe.Text = a.ToString();
            }
            else
            {
                qqq.IsEnabled = false;
            }
            
        }

        private void www_Click(object sender, RoutedEventArgs e)
        {
            qqq.IsEnabled = true;
            if (int.Parse(qwe.Text) != 6)
            {
                var a = int.Parse(qwe.Text);
                a += 1;
                qwe.Text = a.ToString();
            }
            else
            {
                www.IsEnabled = false;
            }
        }

        private void AddDishBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Helper.order == null)
            {
                DataBases.Order order = new DataBases.Order()
                {
                    Tables = int.Parse(NumberTablesCBox.SelectedValue.ToString()),
                    NumberPersons = int.Parse(qwe.Text),
                    OrderStatus = "принят",
                    CookingStatus = "готовится",
                    PaymentMethod = "не оплачен",
                    DateTimes = DateTime.Now,
                    Waiter = Helper.usersession.Id
                };
                Helper.db.Add(order);
                Helper.db.SaveChanges();
                Helper.order = Helper.db.Orders.OrderByDescending(r => r.Id).First();
                new DishAddOrdersWindow().Show();
                qqq.IsEnabled = false;
                www.IsEnabled = false;
                NumberTablesCBox.IsEnabled = false;
            }
            else
                new DishAddOrdersWindow().Show();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            foreach (var c in Helper.db.OrderedDishes) 
            {
                if(c.OrdersId == Helper.order.Id)
                {
                    Helper.db.OrderedDishes.Remove(c);
                }
            }

            Helper.db.Orders.Remove(Helper.order);
            Helper.db.SaveChanges();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Helper.order = null;
        }

        private void update_ClickHis(object sender, RoutedEventArgs e)
        {
            InitData();
        }
    }
    public class Tablenum
    {
        public int title { get; set; }
        public override string ToString() => $"{title}";
    }
}
