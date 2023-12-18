using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kafe_Bulochka.DataBases;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using Word = Microsoft.Office.Interop.Word;
using Window = System.Windows.Window;

namespace Kafe_Bulochka
{
    /// <summary>
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        DataBases.Order order1;
        public PaymentWindow(DataBases.Order order)
        {
            this.order1 = order;
            InitializeComponent();
        }

        private void BackBat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bat1_Click(object sender, RoutedEventArgs e)
        {
            order1.PaymentMethod = "наличная";
            order1.OrderStatus = "оплачен";
            Helper.db.SaveChanges();
            this.Close();
            ChekPDF(order1);

        }

        private void bat2_Click(object sender, RoutedEventArgs e)
        {
            order1.PaymentMethod = "безналичная";
            order1.OrderStatus = "оплачен";
            Helper.db.SaveChanges();
            this.Close();
            ChekPDF(order1);
        }

        private void ChekPDF(DataBases.Order order1)
        {

            var a = Helper.db.Users.FirstOrDefault(q => q.Id == order1.Waiter);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            var war = Helper.usersession;
            var t = DateTime.Today;
            var t1 = DateTime.Now;

            var pdf = "pdf documents |*.pdf";

            saveFileDialog.FileName = "Чек за " + DateTime.Now.ToString("dd.MM.yyyy hh.mm.ss") + " Официант " + war.Fname;
            saveFileDialog.Filter = $"{pdf}";

            if ((bool)saveFileDialog.ShowDialog())
            {

                int num = 0;
                Word.Application application = new Word.Application();
                Word.Document document = application.Documents.Add();
                Word.Paragraph paragraph = document.Paragraphs.Add();

                foreach (var q in Helper.db.OrderedDishes.Include(q => q.Dishes).Include(q => q.Orders).Where(q => q.OrdersId == order1.Id).ToList())
                {
                    num++;
                }



                paragraph.Range.Text = ($"Кафе Булочка\nДата и время: {DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss")}\nОфициант: {war.Fname} {war.Sname} {war.Mname}\nБлюда:\n");
                var tableRange = paragraph.Range;
                Word.Table table_ = document.Tables.Add(tableRange, num + 1, 4);
                table_.Borders.Enable = 1;

                table_.Rows[1].Cells[1].Range.Text = "Блюдо";
                table_.Rows[1].Cells[2].Range.Text = "Количество";
                table_.Rows[1].Cells[3].Range.Text = "Цена";
                table_.Rows[1].Cells[4].Range.Text = "Всего";


                int c = 2;
                decimal sum = 0;
                foreach (var q in Helper.db.OrderedDishes.Include(q => q.Dishes).Include(q => q.Orders).Where(q => q.OrdersId == order1.Id).ToList())
                {
                    table_.Rows[c].Cells[1].Range.Text = q.Dishes.Tittle.ToString();
                    table_.Rows[c].Cells[2].Range.Text = q.Quantity.ToString().ToString();
                    table_.Rows[c].Cells[3].Range.Text = q.Dishes.Prise.ToString();
                    table_.Rows[c].Cells[4].Range.Text = (q.Quantity * q.Dishes.Prise).ToString();
                    sum += q.Quantity * q.Dishes.Prise;
                    c++;
                }
                paragraph.Range.Text = ($"Итого: {sum}\nОфициант: {a.Fname} {a.Sname} {a.Mname}\nСпособ оплаты: {order1.PaymentMethod}");
                document.SaveAs(saveFileDialog.FileName, Word.WdSaveFormat.wdFormatPDF);
                application.Quit();
            }
        }
    }
}
