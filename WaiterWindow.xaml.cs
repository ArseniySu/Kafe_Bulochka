
using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;



namespace Kafe_Bulochka
{
    /// <summary>
    /// Логика взаимодействия для WaiterWindow.xaml
    /// </summary>
    public partial class WaiterWindow : Window
    {
        public WaiterWindow()
        {
            InitializeComponent();
            InitData();
        }
        public void InitData()
        {
            OrdersDGrid.ItemsSource = null;
            OrdersDGridHist.ItemsSource = null;
            OrdersDGridHist1.ItemsSource = null;
            var t = DateTime.Today;
            var t1 = DateTime.Now;
            OrdersDGrid.ItemsSource = Helper.db.Orders.Where(q => q.DateTimes >= t && q.DateTimes <= t1 && q.Waiter == Helper.usersession.Id && q.OrderStatus == "принят").ToList();
            OrdersDGridHist.ItemsSource = Helper.db.Orders.Where(z=>z.Waiter == Helper.usersession.Id).ToList();
            OrdersDGridHist1.ItemsSource = Helper.db.Orders.Where(q => q.DateTimes >= t && q.DateTimes <= t1 && q.Waiter == Helper.usersession.Id).ToList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if ((OrdersDGrid.SelectedItem as DataBases.Order) == null)
            {
                MessageBox.Show("пусто");
            }
            else
            {
                new WaiterOrdWindow(OrdersDGrid.SelectedItem as DataBases.Order).Show();

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
                new WaiterOrdWindow(OrdersDGridHist.SelectedItem as DataBases.Order).Show();

            }
        }

        private void update_ClickHis(object sender, RoutedEventArgs e)
        {
            InitData();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if ((OrdersDGridHist1.SelectedItem as DataBases.Order) == null)
            {
                MessageBox.Show("пусто");
            }
            else
            {
                new WaiterOrdWindow(OrdersDGridHist1.SelectedItem as DataBases.Order).Show();

            }
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddOrderWindow().Show();
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            var war = Helper.usersession;
            var t = DateTime.Today;
            var t1 = DateTime.Now;

            var pdf = "pdf documents |*.pdf";
            var xlx = "excel documents |*.xlsx";

            saveFileDialog.FileName = "Отчет за " + DateTime.Now.ToString("dd.MM.yyyy") + " Официант " + war.Fname;
            saveFileDialog.Filter = $"{pdf} | {xlx}";

            if ((bool)saveFileDialog.ShowDialog())
            {

                if (saveFileDialog.FilterIndex == 1)
                {
                    int sum = 0;
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Add();
                    Word.Paragraph paragraph = document.Paragraphs.Add();

                    foreach (var q in Helper.db.Orders.Where(q => q.DateTimes >= t && q.DateTimes <= t1 && q.Waiter == war.Id))
                    {
                        sum++;
                    }
                    paragraph.Range.Text = ($"Всего заказов за смену: {sum}");



                    paragraph.Range.Text = ($"Кафе Булочка\nОфициант: {war.Fname} {war.Sname} {war.Mname}\n");
                    var tableRange = paragraph.Range;
                    Word.Table table_ = document.Tables.Add(tableRange, sum + 1, 6);
                    table_.Borders.Enable = 1;

                    table_.Rows[1].Cells[1].Range.Text = "Стол";
                    table_.Rows[1].Cells[2].Range.Text = "Количество персон";
                    table_.Rows[1].Cells[3].Range.Text = "Статус заказа";
                    table_.Rows[1].Cells[4].Range.Text = "Статус готовки";
                    table_.Rows[1].Cells[5].Range.Text = "Способ оплаты";
                    table_.Rows[1].Cells[6].Range.Text = "Вата и время";

                    int a = 2;
                    foreach (var q in Helper.db.Orders.Where(q => q.DateTimes >= t && q.DateTimes <= t1 && q.Waiter == war.Id))
                    {
                        table_.Rows[a].Cells[1].Range.Text = q.Tables.ToString();
                        table_.Rows[a].Cells[2].Range.Text = q.NumberPersons.ToString();
                        table_.Rows[a].Cells[3].Range.Text = q.OrderStatus;
                        table_.Rows[a].Cells[4].Range.Text = q.CookingStatus;
                        table_.Rows[a].Cells[5].Range.Text = q.PaymentMethod;
                        table_.Rows[a].Cells[6].Range.Text = q.DateTimes.ToString("dd.MM.yyyy hh:mm:ss");
                        a++;

                    }
                    paragraph.Range.Text = ($"Всего заказов за смену: {sum}");


                    document.SaveAs(saveFileDialog.FileName, Word.WdSaveFormat.wdFormatPDF);
                    application.Quit();

                }
                else
                {
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Add();
                    Excel.Worksheet worksheet = application.Worksheets.Add();

                    worksheet.Name = "Кафе Булочка!";
                    worksheet.UsedRange.Cells[1, 1] = "Кафе Булочка!";
                    worksheet.UsedRange.Cells[2, 1] = "Официант:";
                    worksheet.UsedRange.Cells[2, 2] = war.Fname;
                    worksheet.UsedRange.Cells[2, 3] = war.Sname;
                    worksheet.UsedRange.Cells[2, 4] = war.Mname;

                    worksheet.UsedRange.Cells[4, 1] = "Стол";
                    worksheet.UsedRange.Cells[4, 2] = "Количество персон";
                    worksheet.UsedRange.Cells[4, 3] = "Статус заказа";
                    worksheet.UsedRange.Cells[4, 4] = "Статус готовки";
                    worksheet.UsedRange.Cells[4, 5] = "Способ оплаты";
                    worksheet.UsedRange.Cells[4, 6] = "дата и время";

                    int a = 5;
                    foreach (var q in Helper.db.Orders.Where(q => q.DateTimes >= t && q.DateTimes <= t1 && q.Waiter == war.Id))
                    {

                        worksheet.UsedRange.Cells[a, 1] = q.Tables;
                        worksheet.UsedRange.Cells[a, 2] = q.NumberPersons;
                        worksheet.UsedRange.Cells[a, 3] = q.OrderStatus;
                        worksheet.UsedRange.Cells[a, 4] = q.CookingStatus;
                        worksheet.UsedRange.Cells[a, 5] = q.PaymentMethod;
                        worksheet.UsedRange.Cells[a, 6] = q.DateTimes.ToString("dd.MM.yyyy hh:mm:ss");
                        a++;
                    }
                    workbook.SaveAs(saveFileDialog.FileName);
                    application.Quit();
                }

            }
        }



    }
}

