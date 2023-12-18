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
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace Kafe_Bulochka
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            UsersDGrid.ItemsSource = Helper.db.Users.Include(q => q.Role).ToList();
            OrderDGrid.ItemsSource = Helper.db.Orders.Include(q => q.WaiterNavigation).ToList();
            ChanesDGrid.ItemsSource = Helper.db.Changes.Include(q => q.Users).ToList();

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddUsersWindow().Show();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if ((UsersDGrid.SelectedItem as DataBases.User) == null)
            {
                MessageBox.Show("пусто");
            }
            else
            {
                new CardUsersWindow(UsersDGrid.SelectedItem as DataBases.User).Show();

            }
        }

        private void update_ClickHis(object sender, RoutedEventArgs e)
        {
            InitData();
        }

        private void Chengas_ClickHis(object sender, RoutedEventArgs e)
        {
            if ((UsersDGrid.SelectedItem as DataBases.User) == null)
            {
                MessageBox.Show("пусто");
            }
            else
            {
                DataBases.User user = UsersDGrid.SelectedItem as DataBases.User;
                if (user.Jobstatus != "уволен")
                {
                    new AddChangesWindow1(UsersDGrid.SelectedItem as DataBases.User).Show();

                }
                else
                    MessageBox.Show("Сотрудник уволен");
            }
        }

        private void reportorderBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            var t = DateTime.Today;
            var t1 = DateTime.Now;

            var pdf = "pdf documents |*.pdf";
            var xlx = "excel documents |*.xlsx";

            saveFileDialog.FileName = "Отчет за " + DateTime.Now.ToString("dd.MM.yyyy");
            saveFileDialog.Filter = $"{pdf} | {xlx}";

            if ((bool)saveFileDialog.ShowDialog())
            {

                if (saveFileDialog.FilterIndex == 1)
                {
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Add();
                    Word.Paragraph paragraph = document.Paragraphs.Add();

                    paragraph.Range.Text = $"Отчет за {DateTime.Now.ToString("dd.MM.yyyy")}\nАдминистратор: {Helper.usersession.Fname} {Helper.usersession.Sname} {Helper.usersession.Mname}\n";

                    foreach (var q in Helper.db.Orders.Where(q => q.DateTimes >= t && q.DateTimes <= t1).Include(z => z.WaiterNavigation))
                    {

                        paragraph.Range.Text = $"Официант: {q.WaiterNavigation.Fname} {q.WaiterNavigation.Sname} {q.WaiterNavigation.Mname}\n" +
                            $"Столик: {q.Tables}, Количество персон: {q.NumberPersons}, Статус заказа: {q.OrderStatus}, Статус готовки: {q.CookingStatus}, Способ оплаты: {q.PaymentMethod}, " +
                            $"Время: {q.DateTimes.ToString("hh:MM.ss")}\n\n";
                    }
                    document.SaveAs(saveFileDialog.FileName, Word.WdSaveFormat.wdFormatPDF);
                    application.Quit();

                }
                else
                {
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Add();
                    Excel.Worksheet worksheet = application.Worksheets.Add();

                    worksheet.Name = $"Отчет за {DateTime.Now.ToString("dd.MM.yyyy")}";
                    worksheet.UsedRange.Cells[1, 1] = $"Отчет за {DateTime.Now.ToString("dd.MM.yyyy")}";
                    worksheet.UsedRange.Cells[2, 1] = "Администратор:";
                    worksheet.UsedRange.Cells[2, 2] = Helper.usersession.Fname;
                    worksheet.UsedRange.Cells[2, 3] = Helper.usersession.Sname;
                    worksheet.UsedRange.Cells[2, 4] = Helper.usersession.Mname;

                    worksheet.UsedRange.Cells[4, 1] = "Официант";
                    worksheet.UsedRange.Cells[4, 2] = "Стол";
                    worksheet.UsedRange.Cells[4, 3] = "Количество персон";
                    worksheet.UsedRange.Cells[4, 4] = "Статус заказа";
                    worksheet.UsedRange.Cells[4, 5] = "Статус готовки";
                    worksheet.UsedRange.Cells[4, 6] = "Способ оплаты";
                    worksheet.UsedRange.Cells[4, 7] = "дата и время";


                    int a = 5;
                    foreach (var q in Helper.db.Orders.Where(q => q.DateTimes >= t && q.DateTimes <= t1).Include(z => z.WaiterNavigation))
                    {


                        worksheet.UsedRange.Cells[a, 1] = $"{q.WaiterNavigation.Fname} {q.WaiterNavigation.Sname} {q.WaiterNavigation.Mname}";
                        worksheet.UsedRange.Cells[a, 2] = q.Tables;
                        worksheet.UsedRange.Cells[a, 3] = q.NumberPersons;
                        worksheet.UsedRange.Cells[a, 4] = q.OrderStatus;
                        worksheet.UsedRange.Cells[a, 5] = q.CookingStatus;
                        worksheet.UsedRange.Cells[a, 6] = q.PaymentMethod;
                        worksheet.UsedRange.Cells[a, 7] = q.DateTimes.ToString("dd.MM.yyyy hh:mm:ss");
                        a++;
                    }
                    workbook.SaveAs(saveFileDialog.FileName);
                    application.Quit();
                }

            }
        }

        private void Open_ClickHis(object sender, RoutedEventArgs e)
        {
            if ((OrderDGrid.SelectedItem as DataBases.Order) == null)
            {
                MessageBox.Show("пусто");
            }
            else
            {
                new AdminOrderWindow(OrderDGrid.SelectedItem as DataBases.Order).Show();

            }

        }

        private void edit_ClickHis(object sender, RoutedEventArgs e)
        {

        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}

