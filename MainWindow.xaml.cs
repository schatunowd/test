using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class Report
    {
        public string Name { get; set; } = "Undefined";
        public string Email { get; set; } = "Undefined";
        public Report(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public void Print(string name)
        {
            MessageBox.Show("Отчет печатается с параметрами: " + name, "Ура", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void sendEmail(string email)
        {
            MessageBox.Show("Отчет отправлен по email: " + email, "Ура", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public async Task logAsync (string connectionString, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = query;
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
            }
            MessageBox.Show("Запрос выполнен " + query, "Ура", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class currencyReport : Report
    {
        public string Currency { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public currencyReport(string name, string email, string currency, DateTime beginDate, DateTime endDate)
            : base(name, email)
        {
            if (email == null)
            {
                throw new ArgumentException($"\"{nameof(email)}\" не может быть неопределенным или пустым.", nameof(email));
            }

            if (currency == null)
            {
                throw new ArgumentException($"\"{nameof(currency)}\" не может быть неопределенным или пустым.", nameof(currency));
            }

            Currency = currency;
            BeginDate = beginDate;
            EndDate = endDate;
        }

        public async Task logAsync(string connectionString, int type)
        {
            string query = "INSERT INTO logs (name,email,datebegin,dateend,currency) VALUES (@name,@email,@datebegin, @dateend, @currency)";
            if (type == 1)
            {
               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = query;
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                    command.Parameters.AddWithValue("@name", Name);
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@datebegin", BeginDate);
                    command.Parameters.AddWithValue("@dateend", EndDate);
                    command.Parameters.AddWithValue("@currency", Currency);
                }

            }
            MessageBox.Show("Запрос выполнен " + query, "Ура", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }

    public class balanceReport : Report
    {
        public string NumClient { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public balanceReport(string name, string email, DateTime beginDate, DateTime endDate, string numClient)
            : base(name, email)
        {
            if (email == null)
            {
                throw new ArgumentException($"\"{nameof(email)}\" не может быть неопределенным или пустым.", nameof(email));
            }

            if (numClient == null)
            {
                throw new ArgumentException($"\"{nameof(numClient)}\" не может быть неопределенным или пустым.", nameof(currency));
            }
            BeginDate = beginDate;
            EndDate = endDate;
            NumClient = numClient;
        }

        public void Print(string name, string numClient)
        {
            MessageBox.Show("Отчет печатается с параметрами: " + name + ", " + numClient, "Ура", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void sendEmail(string email, string numClient)
        {
            //
        }

        public async Task logAsync(string connectionString, int type)
        {
            string query = "INSERT INTO logs (name,email,datebegin,dateend,numclient) VALUES (@name,@email,@datebegin, @dateend, @numclient)";
            if (type == 2)
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = query;
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                    command.Parameters.AddWithValue("@name", Name);
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@datebegin", BeginDate);
                    command.Parameters.AddWithValue("@dateend", EndDate);
                    command.Parameters.AddWithValue("@numclient", NumClient);
                }

            }
            MessageBox.Show("Запрос выполнен " + query, "Ура", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string templatePath = "templates";
        public MainWindow()
        {
        string[] templates = Directory.GetFiles(templatePath);
        InitializeComponent();
        }


        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
            }
            if (openFileDialog.FileName.Contains("валютный отчет.txt"))
            {
                currency currencyReport = new currency();
                currencyReport.Show();
            }

            if (openFileDialog.FileName.Contains("остатки по резервам.txt"))
            {
                balance balanceReport = new balance();
                balanceReport.Show();
            }

        }
    }
}
