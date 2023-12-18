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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Kafe_Bulochka
{
    /// <summary>
    /// Логика взаимодействия для DishesWindow.xaml
    /// </summary>
    public partial class DishesWindow : Window
    {
        private DataBases.Order Order1;
        public DishesWindow(DataBases.Order order)
        {
            InitializeComponent();
            this.Order1 = order;
            var a = Helper.db.Users.Include(z=>z.Orders).FirstOrDefault(u => u.Id == order.Waiter);
            titleTBox.Text = order.DateTimes.ToString();
            titleTBox1.Text = order.Tables.ToString();
            titleTBox11.Text = a.Fname + " " + a.Sname + " " + a.Mname;
            DishesDGrid.ItemsSource = Helper.db.OrderedDishes.Include(q => q.Dishes).Include(q => q.Orders).Where(q => q.OrdersId == order.Id).ToList();

            qweCBox.ItemsSource = new StatusCook[] { 
                new StatusCook{ title = "готов"}, 
                new StatusCook{ title = "готовится"}, 
            };
            
            qweCBox.SelectedIndex = 1;



        }

        private void BackbTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SavebTN_Click(object sender, RoutedEventArgs e)
        {
            if (qweCBox.SelectedItem is StatusCook status) 
            {
                Order1.CookingStatus = status.title;
            }
            Helper.db.SaveChanges();
            this.Close();
        }

    }

    public class StatusCook
    {
        public string title { get; set; } = "";
        public override string ToString() => $"{title}";
    } 
}

