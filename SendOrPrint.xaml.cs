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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для SendOrPrint.xaml
    /// </summary>
    public partial class SendOrPrint : Window
    {
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=DatabaseName;Trusted_Connection=True;";
        private currencyReport cr;
        private balanceReport br;

        public SendOrPrint(currencyReport cr)
        {
            this.cr = cr;
            InitializeComponent();
        }
        public SendOrPrint(balanceReport br)
        {
            this.br = br;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cr != null)
            {
                cr.sendEmail(cr.Email);
                cr = null;
                //cr.logAsync(connectionString, 1);
                Close();
            }
            if (br != null)
            {
                br.sendEmail(br.Email);
                br = null;
                //br.logAsync(connectionString, 2);
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cr != null)
            {
                cr.Print(cr.Name);
                cr = null;
                //cr.logAsync(connectionString, 1);
                Close();
            }
            if (br != null)
            {
                br.Print(br.Name);
                br = null;
                //br.logAsync(connectionString, 2);
                Close();
            }
        }
    }
}
