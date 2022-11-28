
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ChatApi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HttpClient httpClient = new HttpClient();
        public static Employee employee;

        public MainWindow()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Login) &&
            !string.IsNullOrEmpty(Properties.Settings.Default.Password))
            {
                Enter();
            }
        }
        private async void Enter()
        {
            logTb.Text = Properties.Settings.Default.Login;
            PassTb.Text = Properties.Settings.Default.Password;
        }

        private async void Signin(object sender, RoutedEventArgs e)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var content = new userData { password = PassTb.Text, username = logTb.Text };
            HttpContent httpContent =
            new StringContent(JsonConvert.
            SerializeObject(content), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:50203/api/Auth", httpContent);
            if (message.IsSuccessStatusCode)
            {
                var curConnect = await message.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(curConnect);

                if ((bool)SaveCheck.IsChecked)
                {
                    Properties.Settings.Default.Login = logTb.Text;
                    Properties.Settings.Default.Password = PassTb.Text;
                    Properties.Settings.Default.Save();

                }
                else
                {
                    Properties.Settings.Default.Login = string.Empty;
                    Properties.Settings.Default.Password = string.Empty;
                    Properties.Settings.Default.Save();
                }
                ChatSelection n = new ChatSelection();
                n.Show();
                Close();
            }
            else
            {
                MessageBox.Show("пользователь не найден");
            }
        }

        public class userData
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}