using Kafe_Bulochka.DataBases;
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
    /// Логика взаимодействия для CounteDishWindow.xaml
    /// </summary>
    public partial class CounteDishWindow : Window
    {
        public DataBases.Dish dish1;
        public CounteDishWindow(DataBases.Dish dish)
        {
            InitializeComponent();
            this.dish1 = dish;
        }

        private void BackBat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DishAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LoginTBox.Text))
            {
                if (Regex.IsMatch(LoginTBox.Text, @"[0-9]$"))
                {
                    DataBases.OrderedDish dish = new DataBases.OrderedDish()
                    {
                        OrdersId = Helper.order.Id,
                        DishesId = dish1.Id,
                        Quantity = int.Parse(LoginTBox.Text)
                    };

                    Helper.db.Add(dish);
                    Helper.db.SaveChanges();

                    this.Close();
                }
                else
                    MessageBox.Show("Необходимо ввести число", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Заполните поле", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);





        }
    }
}
