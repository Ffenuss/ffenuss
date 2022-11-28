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
using System.Windows.Shapes;

namespace ChatApi
{
    /// <summary>
    /// Логика взаимодействия для ChatSelection.xaml
    /// </summary>
    public partial class ChatSelection : Window
    {
        public List<ChatRoom> chatRooms = new List<ChatRoom>();
        public List<ChatRoomEmployee> chatRoomEmployees = new List<ChatRoomEmployee>();
        public static ChatRoom chatRoom;
        public ChatSelection()
        {
            InitializeComponent();
            ChatSelect.DataContext = MainWindow.employee;
            GetRooms();
        }

        public async void GetRooms()
        {
            //Берем инфу о чатах(комнатах)
            HttpResponseMessage httpResponseMessage = await MainWindow.httpClient.GetAsync("http://localhost:50203/api/Chatrooms");
            var rooms = await httpResponseMessage.Content.ReadAsStringAsync();
            //Берем инфу о пользоваталей в чатах(комнатах)
            HttpResponseMessage responseMessage = await MainWindow.httpClient.GetAsync("http://localhost:50203/api/ChatroomEmploees");
            var emp = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ChatRoomEmployee>>(emp)
            .Where(i => i.IdEmployee == MainWindow.employee.id).ToList();
            if (result == null)
            {

            }
            else
            {
                var temp = JsonConvert.DeserializeObject<List<ChatRoom>>(rooms).ToList();
                ChatRoomList.ItemsSource = from t in temp
                                           join r in result on t.id equals r.IdChatRoom
                                           select t;
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            chatRoom = ChatRoomList.SelectedItem as ChatRoom;
            ChatRoomWindow chat = new ChatRoomWindow();
            chat.Show();
            this.Close();

            
        }
        //chatRoom = ChatRoomList.SelectedItem as ChatRoom;
        //    ChatRoomWindow w = new ChatRoomWindow();
        //w.Show();
        //Close();
    }
}