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
    /// Логика взаимодействия для DishAddOrdersWindow.xaml
    /// </summary>
    public partial class DishAddOrdersWindow : Window
    {
        public DishAddOrdersWindow()
        {
            InitializeComponent();
            DishjuiceDGrid.ItemsSource = Helper.db.Dishes.Where(q => q.CategoryDishesId == 1).ToList();
            DishgarnishDGrid.ItemsSource = Helper.db.Dishes.Where(q => q.CategoryDishesId == 2).ToList();
            DishAlkDGrid.ItemsSource = Helper.db.Dishes.Where(q => q.CategoryDishesId == 3).ToList();
            DishhotdrinksDGrid.ItemsSource = Helper.db.Dishes.Where(q => q.CategoryDishesId == 4).ToList();
            DishdessertsDGrid.ItemsSource = Helper.db.Dishes.Where(q => q.CategoryDishesId == 5).ToList();
            DishhotdishesDGrid.ItemsSource = Helper.db.Dishes.Where(q => q.CategoryDishesId == 6).ToList();
            DishSallatsDGrid.ItemsSource = Helper.db.Dishes.Where(q => q.CategoryDishesId == 7).ToList();
            DishsnacksDGrid.ItemsSource = Helper.db.Dishes.Where(q => q.CategoryDishesId == 8).ToList();
            DishsoupsDGrid.ItemsSource = Helper.db.Dishes.Where(q => q.CategoryDishesId == 9).ToList();



        }

        private void DishjuiceDGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                new CounteDishWindow(DishjuiceDGrid.SelectedValue as DataBases.Dish).Show();
        }

        private void DishgarnishDGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CounteDishWindow(DishgarnishDGrid.SelectedValue as DataBases.Dish).Show();

        }

        private void DishAlkDGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CounteDishWindow(DishAlkDGrid.SelectedValue as DataBases.Dish).Show();
        }

        private void DishhotdrinksDGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CounteDishWindow(DishhotdrinksDGrid.SelectedValue as DataBases.Dish).Show();
        }

        private void DishdessertsDGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CounteDishWindow(DishdessertsDGrid.SelectedValue as DataBases.Dish).Show();
        }

        private void DishhotdishesDGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CounteDishWindow(DishhotdishesDGrid.SelectedValue as DataBases.Dish).Show();
        }

        private void DishSallatsDGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CounteDishWindow(DishSallatsDGrid.SelectedValue as DataBases.Dish).Show();
        }

        private void DishsnacksDGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CounteDishWindow(DishsnacksDGrid.SelectedValue as DataBases.Dish).Show();
        }

        private void DishsoupsDGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CounteDishWindow(DishsoupsDGrid.SelectedValue as DataBases.Dish).Show();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
