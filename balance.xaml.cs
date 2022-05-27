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
    /// Логика взаимодействия для balance.xaml
    /// </summary>
    public partial class balance : Window
    {
        public balance()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = "defaultName", email = "defaultEmail", numClientBalance = "defaultNum";
            DateTime begin = default, end = default;
            name = txtNameBalance.Text;
            email = txtMailBalance.Text;
            numClientBalance = numClient.Text;
            try
            {
                begin = beginCalendarBalance.SelectedDate.Value;
                end = endCalendarBalance.SelectedDate.Value;
                balanceReport br = new balanceReport(name, email,begin,end,numClientBalance);
                SendOrPrint window = new SendOrPrint(br);
                window.Show();
            }
            catch
            {
                MessageBox.Show("Даты и номер клиента должны быть заполнены", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
