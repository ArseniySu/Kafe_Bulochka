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
    /// Логика взаимодействия для CookWindow.xaml
    /// </summary>
    public partial class CookWindow : Window
    {
        public CookWindow()
        {
            InitializeComponent();
            InitData();
        }
        public void InitData()
        {
            OrdersDGrid.ItemsSource = null;
            var t = DateTime.Today;
            var t1 = DateTime.Now;
            OrdersDGrid.ItemsSource = Helper.db.Orders.Where(q=>q.DateTimes > t && q.DateTimes < t1 && q.CookingStatus == "готовится").ToList();
            OrdersDGridHist.ItemsSource = Helper.db.Orders.ToList();

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if ((OrdersDGrid.SelectedItem as DataBases.Order) == null)
            {
                MessageBox.Show("пусто");
            }
            else
            {
                new DishesWindow(OrdersDGrid.SelectedItem as DataBases.Order).Show();

            }
        }
        private void Open_ClickHis(object sender, RoutedEventArgs e)
        {
            if ((OrdersDGridHist.SelectedItem as DataBases.Order) == null)
            {
                MessageBox.Show("пусто");
            }
            else
            {
                new DishesWindow(OrdersDGridHist.SelectedItem as DataBases.Order).Show();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void update_ClickHis(object sender, RoutedEventArgs e)
        {
            InitData();
        }
    }
}
