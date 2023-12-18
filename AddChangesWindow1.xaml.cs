using Aspose.Cells.Charts;
using Kafe_Bulochka.DataBases;
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
    /// Логика взаимодействия для AddChangesWindow1.xaml
    /// </summary>
    public partial class AddChangesWindow1 : Window
    {
        DataBases.User user;
        public AddChangesWindow1(DataBases.User user)
        {
            InitializeComponent();
            this.user = user;
            ChangesCBox.ItemsSource = new Chanhesbox[] 
            {
                new Chanhesbox{ title = "1"},
                new Chanhesbox{ title = "2"},
            };

        }

        private void AddChangesBtn_Click(object sender, RoutedEventArgs e)
        {
                if (!string.IsNullOrWhiteSpace(ChangesCBox.Text) &&
                    !string.IsNullOrWhiteSpace(datechan.ToString()))
                {
                    DataBases.Change change = new DataBases.Change()
                    {
                        DateChanges = DateTime.Parse(datechan.ToString()),
                        ChangesTitle = ChangesCBox.SelectedValue.ToString(),
                        UsersId = user.Id
                    };
                    Helper.db.Changes.Add(change);
                    Helper.db.SaveChanges();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Заполните все поля", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }


        }
    }

    public class Chanhesbox
    {
        public string title { get; set; } = "";
        public override string ToString() => $"{title}";
    }
}
