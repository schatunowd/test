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
    /// Логика взаимодействия для currency.xaml
    /// </summary>
    public partial class currency : Window
    {
        public currency()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = "", email ="", curr = "";
            DateTime begin = default, end = default;
                 name = txtName.Text;
                 email = txtMail.Text;
            curr = currencyBox.Text;
            try
                {
                    begin = beginCalendar.SelectedDate.Value;
                end = endCalendar.SelectedDate.Value;
                
                currencyReport cr = new currencyReport(name, email, curr, begin, end);
                SendOrPrint window = new SendOrPrint(cr);
                window.Show();
            }
            catch
                {
                    MessageBox.Show("Даты должны быть заполнены", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
    }
}
