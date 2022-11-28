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
using System.Windows.Shapes;

namespace ChatApi
{
    /// <summary>
    /// Логика взаимодействия для WindowMessage.xaml
    /// </summary>
    public partial class WindowMessage : Window
    {
        public List<ChatRoom> chatRooms = new List<ChatRoom>();

        public List<ChatRoomEmployee> chatRoomEmployees = new List<ChatRoomEmployee>();

        public WindowMessage()
        {
            InitializeComponent();
            helloGrid.DataContext = MainWindow.employee;
        }

        public async void GetRooms()
        {
            HttpResponseMessage httpResponseMessage = await MainWindow.httpClient.GetAsync("http://localhost:50203/api/Chatrooms");
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
